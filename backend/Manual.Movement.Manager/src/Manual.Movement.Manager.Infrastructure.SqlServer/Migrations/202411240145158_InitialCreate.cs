namespace Manual.Movement.Manager.Infrastructure.SqlServer.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MOVIMENTO_MANUAL",
                c => new
                    {
                        DAT_MES = c.Int(nullable: false),
                        DAT_ANO = c.Int(nullable: false),
                        NUM_LANCAMENTO = c.Long(nullable: false),
                        COD_PRODUTO = c.String(nullable: false, maxLength: 4),
                        COD_COSIF = c.String(nullable: false, maxLength: 11),
                        DES_DESCRICAO = c.String(nullable: false, maxLength: 50),
                        DAT_MOVIMENTO = c.DateTime(nullable: false),
                        COD_USUARIO = c.String(nullable: false, maxLength: 15),
                        VAL_VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.DAT_MES, t.DAT_ANO, t.NUM_LANCAMENTO, t.COD_PRODUTO, t.COD_COSIF });
            
            CreateTable(
                "dbo.PRODUTO_COSIF",
                c => new
                    {
                        COD_PRODUTO = c.String(nullable: false, maxLength: 4),
                        COD_COSIF = c.String(nullable: false, maxLength: 11),
                        CO_CLASSIFICACAO = c.String(maxLength: 6),
                        STA_STATUS = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => new { t.COD_PRODUTO, t.COD_COSIF });
            
            CreateTable(
                "dbo.PRODUTO",
                c => new
                    {
                        COD_PRODUTO = c.String(nullable: false, maxLength: 4),
                        DES_PRODUTO = c.String(maxLength: 30),
                        STA_STATUS = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.COD_PRODUTO);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PRODUTO");
            DropTable("dbo.PRODUTO_COSIF");
            DropTable("dbo.MOVIMENTO_MANUAL");
        }
    }
}
