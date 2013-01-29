namespace Reprografia.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Solicitacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ano = c.Int(nullable: false),
                        Seq = c.Int(nullable: false),
                        FornecedorId = c.Int(nullable: false),
                        Formato = c.String(),
                        DataSolicitacao = c.DateTime(nullable: false),
                        DataEntrega = c.DateTime(nullable: false),
                        Suporte = c.String(),
                        UserName = c.String(maxLength: 128),
                        AreaId = c.Int(nullable: false),
                        CodificacaoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Fornecedor", t => t.FornecedorId, cascadeDelete: true)
                .ForeignKey("User", t => t.UserName)
                .ForeignKey("Area", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("Codificacao", t => t.CodificacaoId, cascadeDelete: true)
                .Index(t => t.FornecedorId)
                .Index(t => t.UserName)
                .Index(t => t.AreaId)
                .Index(t => t.CodificacaoId);
            
            CreateTable(
                "Fornecedor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "User",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "Role",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "Avaliacao",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Avaliado = c.Boolean(nullable: false),
                        DataLimite = c.DateTime(nullable: false),
                        DataAvaliado = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Solicitacao", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ItemAvaliacao",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AvaliacaoId = c.Int(nullable: false),
                        Prazo = c.String(),
                        Nitidez = c.String(),
                        Paginacao = c.String(),
                        Quantidade = c.String(),
                        Matriz = c.String(),
                        Acabamento = c.String(),
                        Satisfacao = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Item", t => t.Id)
                .ForeignKey("Avaliacao", t => t.AvaliacaoId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.AvaliacaoId);
            
            CreateTable(
                "Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100),
                        Paginas = c.Int(nullable: false),
                        Copias = c.Int(nullable: false),
                        GramposACavalo = c.Boolean(nullable: false),
                        GramposLaterais = c.Boolean(nullable: false),
                        Espiral = c.Boolean(nullable: false),
                        CapaEmPVC = c.Boolean(nullable: false),
                        CapaEmPapel = c.Boolean(nullable: false),
                        Transparencia = c.Boolean(nullable: false),
                        Reduzido = c.Boolean(nullable: false),
                        Ampliado = c.Boolean(nullable: false),
                        Digitacao = c.Boolean(nullable: false),
                        SemAcabamento = c.Boolean(nullable: false),
                        Grampear = c.Boolean(nullable: false),
                        PretoBranco = c.Boolean(nullable: false),
                        FrenteVerso = c.Boolean(nullable: false),
                        SoFrente = c.Boolean(nullable: false),
                        CortarAoMeio = c.Boolean(nullable: false),
                        SolicitacaoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Solicitacao", t => t.SolicitacaoId, cascadeDelete: true)
                .Index(t => t.SolicitacaoId);
            
            CreateTable(
                "Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        NomeExcel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Codificacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CentroDeCusto = c.Int(nullable: false),
                        DescricaoCentroDeCusto = c.String(),
                        ContaMemo = c.Int(nullable: false),
                        DescricaoContaMemo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EmailCobrancaAvaliacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeradoEm = c.DateTime(nullable: false),
                        EnviadoEm = c.DateTime(nullable: false),
                        Enviado = c.Boolean(nullable: false),
                        Avaliacao_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Avaliacao", t => t.Avaliacao_Id)
                .Index(t => t.Avaliacao_Id);
            
            CreateTable(
                "RoleUser",
                c => new
                    {
                        Role_Name = c.String(nullable: false, maxLength: 128),
                        User_UserName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Role_Name, t.User_UserName })
                .ForeignKey("Role", t => t.Role_Name, cascadeDelete: true)
                .ForeignKey("User", t => t.User_UserName, cascadeDelete: true)
                .Index(t => t.Role_Name)
                .Index(t => t.User_UserName);
            
        }
        
        public override void Down()
        {
            DropIndex("RoleUser", new[] { "User_UserName" });
            DropIndex("RoleUser", new[] { "Role_Name" });
            DropIndex("EmailCobrancaAvaliacao", new[] { "Avaliacao_Id" });
            DropIndex("Item", new[] { "SolicitacaoId" });
            DropIndex("ItemAvaliacao", new[] { "AvaliacaoId" });
            DropIndex("ItemAvaliacao", new[] { "Id" });
            DropIndex("Avaliacao", new[] { "Id" });
            DropIndex("Solicitacao", new[] { "CodificacaoId" });
            DropIndex("Solicitacao", new[] { "AreaId" });
            DropIndex("Solicitacao", new[] { "UserName" });
            DropIndex("Solicitacao", new[] { "FornecedorId" });
            DropForeignKey("RoleUser", "User_UserName", "User");
            DropForeignKey("RoleUser", "Role_Name", "Role");
            DropForeignKey("EmailCobrancaAvaliacao", "Avaliacao_Id", "Avaliacao");
            DropForeignKey("Item", "SolicitacaoId", "Solicitacao");
            DropForeignKey("ItemAvaliacao", "AvaliacaoId", "Avaliacao");
            DropForeignKey("ItemAvaliacao", "Id", "Item");
            DropForeignKey("Avaliacao", "Id", "Solicitacao");
            DropForeignKey("Solicitacao", "CodificacaoId", "Codificacao");
            DropForeignKey("Solicitacao", "AreaId", "Area");
            DropForeignKey("Solicitacao", "UserName", "User");
            DropForeignKey("Solicitacao", "FornecedorId", "Fornecedor");
            DropTable("RoleUser");
            DropTable("EmailCobrancaAvaliacao");
            DropTable("Codificacao");
            DropTable("Area");
            DropTable("Item");
            DropTable("ItemAvaliacao");
            DropTable("Avaliacao");
            DropTable("Role");
            DropTable("User");
            DropTable("Fornecedor");
            DropTable("Solicitacao");
        }
    }
}
