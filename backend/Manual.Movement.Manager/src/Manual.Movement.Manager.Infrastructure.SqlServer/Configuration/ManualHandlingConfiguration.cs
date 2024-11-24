using Manual.Movement.Manager.Domain.ManualHandling;
using Manual.Movement.Manager.Domain.ProductCosif;
using System.Data.Entity.ModelConfiguration;

namespace Manual.Movement.Manager.Infrastructure.SqlServer.Configuration
{
    internal class ManualHandlingConfiguration : EntityTypeConfiguration<ManualHandling>
    {
        public ManualHandlingConfiguration()
        {
            ToTable("MOVIMENTO_MANUAL");

            HasKey(mm => new { mm.Month, mm.Year, mm.LaunchNumber, mm.ProductId, mm.CosifId});

            Property(mm => mm.Month)
                .HasColumnName("DAT_MES")
                .IsRequired();

            Property(mm => mm.Year)
                .HasColumnName("DAT_ANO")
                .IsRequired();

            Property(mm => mm.LaunchNumber)
                .HasColumnName("NUM_LANCAMENTO")
                .IsRequired();

            Property(mm => mm.ProductId)
                .HasColumnName("COD_PRODUTO")
                .HasColumnType("char")
                .HasMaxLength(4)
                .IsRequired();

            Property(mm => mm.CosifId)
                .HasColumnName("COD_COSIF")
                .HasColumnType("char")
                .HasMaxLength(11)
                .IsRequired();

            Property(mm => mm.Description)
                .HasColumnName("DES_DESCRICAO")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(mm => mm.Date)
                .HasColumnName("DAT_MOVIMENTO")
                .IsRequired();

            Property(mm => mm.UserId)
                .HasColumnName("COD_USUARIO")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

            Property(mm => mm.Value)
                .HasColumnName("VAL_VALOR")
                .HasPrecision(18, 2)
                .IsRequired();

            HasRequired(mm => mm.ProductCosif)
                .WithMany() 
                .HasForeignKey(mm => new { mm.ProductId, mm.CosifId })
                .WillCascadeOnDelete(false);
        }
    }
}