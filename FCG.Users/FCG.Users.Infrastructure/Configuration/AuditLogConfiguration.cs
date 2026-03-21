using FCG.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Infrastructure.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("TB_AUDITORIA");

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("ISN_AUDITORIA");
            builder.Property(x => x.TableName).IsRequired().HasMaxLength(128).HasColumnName("DSC_TABELA");
            builder.Property(x => x.Action).IsRequired().HasMaxLength(50).HasColumnName("DSC_ACAO");
            builder.Property(x => x.EntityId).HasMaxLength(100).HasColumnName("ISN_ENTIDADE");
            builder.Property(x => x.UserId).HasMaxLength(100).HasColumnName("ISN_USUARIO");
            builder.Property(p => p.KeyValues).HasColumnName("CHAVES_VALORES");
            builder.Property(p => p.OldValues).HasColumnName("VALORES_ANTIGOS");
            builder.Property(p => p.NewValues).HasColumnName("VALORES_NOVOS");
            builder.Property(p => p.Timestamp).HasColumnName("DTH_ATUALIZACAO");
            builder.HasIndex(x => x.TableName);
            builder.HasIndex(x => x.EntityId);
            builder.HasIndex(x => x.Timestamp);
        }
    }
}
