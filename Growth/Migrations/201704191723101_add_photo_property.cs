namespace Growth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_photo_property : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Photo", c => c.String());
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String());
            AlterColumn("dbo.User", "Email", c => c.String());
            DropColumn("dbo.User", "Photo");
        }
    }
}
