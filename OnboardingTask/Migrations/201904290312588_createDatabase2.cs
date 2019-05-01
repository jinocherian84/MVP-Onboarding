namespace OnboardingTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatabase2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductSolds", "DateSold", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductSolds", "DateSold", c => c.Int(nullable: false));
        }
    }
}
