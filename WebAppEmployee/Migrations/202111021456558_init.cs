namespace WebAppEmployee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        RegistrationNumber = c.Int(nullable: false, identity: true),
                        IsExternalEmployee = c.Boolean(nullable: false),
                        FullName = c.String(),
                        Gender = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        Position_Id = c.Guid(),
                    })
                .PrimaryKey(t => new { t.RegistrationNumber, t.IsExternalEmployee })
                .ForeignKey("dbo.Positions", t => t.Position_Id)
                .Index(t => t.Position_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        BaseSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "Position_Id" });
            DropTable("dbo.Positions");
            DropTable("dbo.Employees");
        }
    }
}
