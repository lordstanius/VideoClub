namespace Model.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        NumOfCopies = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfRental = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 30),
                        LastName = c.String(maxLength: 30),
                        Street = c.String(maxLength: 50),
                        City = c.String(maxLength: 20),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "UserId", "dbo.Users");
            DropForeignKey("dbo.Rentals", "MovieId", "dbo.Movies");
            DropIndex("dbo.Rentals", new[] { "UserId" });
            DropIndex("dbo.Rentals", new[] { "MovieId" });
            DropTable("dbo.Users");
            DropTable("dbo.Rentals");
            DropTable("dbo.Movies");
        }
    }
}
