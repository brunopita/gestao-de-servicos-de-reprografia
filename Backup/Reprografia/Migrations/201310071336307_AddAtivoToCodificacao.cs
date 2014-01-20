namespace Reprografia.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddAtivoToCodificacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("Codificacao", "Ativo", c => c.Boolean(nullable: false, defaultValue: true));
            string desativarCodificacoes = "UPDATE Codificacao SET Ativo = 0 " +
                "WHERE DescricaoContaMemo LIKE 'Qualificacao%' " +
                "   OR DescricaoContaMemo LIKE 'Aperfeicoamento%' " +
                "   OR DescricaoContaMemo LIKE 'Especializacao%' " +
                "   OR DescricaoContaMemo LIKE 'Administracao%' ";
            this.Sql(desativarCodificacoes);
        }

        public override void Down()
        {
            DropColumn("Codificacao", "Ativo");
        }
    }
}
