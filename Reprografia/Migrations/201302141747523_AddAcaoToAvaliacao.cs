namespace Reprografia.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAcaoToAvaliacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("Avaliacao", "Acao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Avaliacao", "Acao");
        }
    }
}
