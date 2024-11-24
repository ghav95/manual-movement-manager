using Manual.Movement.Manager.Domain.Product;
using System.Data.Entity.ModelConfiguration;

namespace Manual.Movement.Manager.Infrastructure.SqlServer.Configuration
{
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("PRODUTO");

            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("COD_PRODUTO")
                .HasMaxLength(4)
                .IsRequired();

            Property(p => p.Description)
                .HasColumnName("DES_PRODUTO")
                .HasMaxLength(30);

            Property(p => p.Status)
                .HasColumnName("STA_STATUS")
                .HasMaxLength(1); 
        }
    }
}
