using FCG.Users.Application.Interface.Service;
using FCG.Users.Application.UseCases.Service;
using FCG.Users.Domain.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace FCG.Users.Application.UseCases.Interceptor
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly MongoAuditService _auditService;
        private readonly IUserService _userService;

        private readonly List<(EntityEntry Entry, AuditDocument Audit)> _pendingAudits = [];

        public AuditInterceptor(
            MongoAuditService auditService,
            IUserService userService)
        {
            _auditService = auditService;
            _userService = userService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = context.ChangeTracker.Entries()
                .Where(x =>
                    x.State == EntityState.Modified ||
                    x.State == EntityState.Added ||
                    x.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                var audit = CreateAudit(entry);

                _pendingAudits.Add((entry, audit));
            }

            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            foreach (var (entry, audit) in _pendingAudits)
            {
                // agora o ID já existe
                audit.RecordId = entry.Properties
                    .FirstOrDefault(p => p.Metadata.IsPrimaryKey())
                    ?.CurrentValue
                    ?.ToString() ?? string.Empty;

                await _auditService.SaveAsync(audit);
            }

            _pendingAudits.Clear();

            return await base.SavedChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        private AuditDocument CreateAudit(EntityEntry entry)
        {
            var changes = new Dictionary<string, AuditChange>();

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                    continue;

                changes[property.Metadata.Name] = new AuditChange
                {
                    Old = entry.State == EntityState.Added
                        ? null
                        : property.OriginalValue,

                    New = entry.State == EntityState.Deleted
                        ? null
                        : property.CurrentValue
                };
            }

            return new AuditDocument
            {
                Table = entry.Metadata.GetTableName() ?? string.Empty,

                Action = entry.State.ToString(),

                TimestampUtc = DateTime.UtcNow,

                Changes = changes,

                User = new AuditUser
                {
                    Id = _userService.GetUserId(),
                }
            };
        }
    }
}
