namespace CanteenFoodApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        personname = c.String(),
                        orderitems = c.String(),
                        comments = c.String(),
                        totalammount = c.Int(nullable: false),
                        ordertime = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
