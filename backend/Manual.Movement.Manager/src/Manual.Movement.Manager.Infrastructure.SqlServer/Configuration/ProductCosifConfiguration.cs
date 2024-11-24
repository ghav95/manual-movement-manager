using Manual.Movement.Manager.Domain.ProductCosif;
using System.Data.Entity.ModelConfiguration;

namespace Manual.Movement.Manager.Infrastructure.SqlServer.Configuration
{
    internal class ProductCosifConfiguration : EntityTypeConfiguration<ProductCosif>
    {
        public ProductCosifConfiguration()
        {
            ToTable("PRODUTO_COSIF");
                        
            HasKey(pc => new { pc.ProductId, pc.CosifId});

            Property(pc => pc.ProductId)
                .HasColumnName("COD_PRODUTO")
                .HasColumnType("char")
                .HasMaxLength(4)
                .IsRequired();

            Property(pc => pc.CosifId)
                .HasColumnName("COD_COSIF")
                .HasColumnType("char")
                .HasMaxLength(11)
                .IsRequired();

            Property(pc => pc.ClassificationId)
                .HasColumnName("CO_CLASSIFICACAO")
                .HasColumnType("char")
                .HasMaxLength(6);

            Property(pc => pc.Status)
                .HasColumnName("STA_STATUS")
                .HasColumnType("char")
                .HasMaxLength(1);
        }
    }
}
