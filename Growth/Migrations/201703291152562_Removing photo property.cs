namespace Growth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removingphotoproperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Photo", c => c.String());
        }
    }
}
