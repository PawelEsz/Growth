namespace Growth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_code_property : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Code");
        }
    }
}
