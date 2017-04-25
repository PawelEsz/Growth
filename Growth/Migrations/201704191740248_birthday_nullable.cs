namespace Growth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class birthday_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
