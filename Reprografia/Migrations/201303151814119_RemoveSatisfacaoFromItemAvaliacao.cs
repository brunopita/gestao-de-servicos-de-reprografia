namespace Reprografia.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSatisfacaoFromItemAvaliacao : DbMigration
    {
        public override void Up()
        {
            DropColumn("ItemAvaliacao", "Satisfacao");
        }
        
        public override void Down()
        {
            DropColumn("Avaliacao", "Acao");
        }
    }
}
