namespace Reprografia.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddSolicitacaoComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("Solicitacao", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Solicitacao", "Comment");
        }
    }
}
