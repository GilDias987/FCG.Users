using Azure.Messaging.ServiceBus.Administration;
using FCG.Users.Application.Interface.Service;
using FCG.Users.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace FCG.Users.Application.UseCases.Interceptor
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly IUserService _userService;

        public AuditInterceptor(IUserService userService)
        {
            _userService = userService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            AddAuditLogs(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddAuditLogs(DbContext context)
        {
            if (context == null) return;

            var auditLogs = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var audit = new AuditLog
                {
                    TableName = entry.Metadata.GetTableName(),
                    Action = entry.State.ToString(),
                    Timestamp = DateTime.UtcNow,
                    UserId = _userService.GetUserId()
                };

                var keyValues = new Dictionary<string, object>();
                var oldValues = new Dictionary<string, object>();
                var newValues = new Dictionary<string, object>();

                foreach (var prop in entry.Properties)
                {
                    var name = prop.Metadata.Name;

                    if (prop.Metadata.IsPrimaryKey())
                    {
                        keyValues[name] = prop.CurrentValue;
                        audit.EntityId = prop.CurrentValue?.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            newValues[name] = prop.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            oldValues[name] = prop.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (prop.IsModified)
                            {
                                oldValues[name] = prop.OriginalValue;
                                newValues[name] = prop.CurrentValue;
                            }
                            break;
                    }
                }

                audit.KeyValues = JsonSerializer.Serialize(keyValues);
                audit.OldValues = JsonSerializer.Serialize(oldValues);
                audit.NewValues = JsonSerializer.Serialize(newValues);

                auditLogs.Add(audit);
            }

            context.Set<AuditLog>().AddRange(auditLogs);
        }
    }
}
