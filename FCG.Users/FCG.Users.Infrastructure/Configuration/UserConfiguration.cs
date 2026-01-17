using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FCG.Users.Domain.Entities;


namespace FCG.Users.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)         
        {
            builder.ToTable("TB_USUARIO");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").HasColumnName("ISN_USUARIO").UseIdentityColumn();
            builder.Property(p => p.Name).HasColumnType("VARCHAR(1000)").HasColumnName("DSC_NOME").IsRequired();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(1000)").HasColumnName("DSC_EMAIL").IsRequired();
            builder.Property(p => p.Passoword).HasColumnType("VARCHAR(1000)").HasColumnName("DSC_SENHA").IsRequired();
            builder.Property(p => p.DateCreation).HasColumnType("DATETIME").HasColumnName("DTH_CRIACAO").IsRequired();
            builder.Property(p => p.DateUpdate).HasColumnType("DATETIME").HasColumnName("DTH_ATUALIZACAO").IsRequired();
            builder.Property(p => p.UserGroupId).HasColumnType("INT").HasColumnName("ISN_GRUPO_USUARIO");

            builder.HasOne(p => p.UserGroup)
                   .WithMany(p => p.Users)
                   .HasPrincipalKey(p => p.Id).IsRequired(true);
        }
    }
}
