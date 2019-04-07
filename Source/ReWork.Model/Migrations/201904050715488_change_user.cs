namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 70),
                        Description = c.String(nullable: false, maxLength: 250),
                        Price = c.Int(),
                        DateAdded = c.DateTime(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerProfiles", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeProfiles", t => t.EmployeeId)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Age = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 250),
                        AddedDate = c.DateTime(nullable: false),
                        ImplementationTime = c.Time(nullable: false, precision: 7),
                        OfferPayment = c.Time(nullable: false, precision: 7),
                        JobId = c.Int(nullable: false),
                        EpmployeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeProfiles", t => t.EpmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.EpmployeeId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        RegistrationdDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 250),
                        AddedDate = c.DateTime(nullable: false),
                        QualityOfWork = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SkillEmployeeProfiles",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        EmployeeProfile_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.EmployeeProfile_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeProfiles", t => t.EmployeeProfile_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.EmployeeProfile_Id);
            
            CreateTable(
                "dbo.SkillJobs",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Job_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Job_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Job_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CustomerProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedBacks", "Id", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "EmployeeId", "dbo.EmployeeProfiles");
            DropForeignKey("dbo.EmployeeProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Skills", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.SkillJobs", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.SkillJobs", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillEmployeeProfiles", "EmployeeProfile_Id", "dbo.EmployeeProfiles");
            DropForeignKey("dbo.SkillEmployeeProfiles", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.Offers", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Offers", "EpmployeeId", "dbo.EmployeeProfiles");
            DropForeignKey("dbo.Jobs", "CustomerId", "dbo.CustomerProfiles");
            DropIndex("dbo.SkillJobs", new[] { "Job_Id" });
            DropIndex("dbo.SkillJobs", new[] { "Skill_Id" });
            DropIndex("dbo.SkillEmployeeProfiles", new[] { "EmployeeProfile_Id" });
            DropIndex("dbo.SkillEmployeeProfiles", new[] { "Skill_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FeedBacks", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Skills", new[] { "SectionId" });
            DropIndex("dbo.Offers", new[] { "EpmployeeId" });
            DropIndex("dbo.Offers", new[] { "JobId" });
            DropIndex("dbo.EmployeeProfiles", new[] { "Id" });
            DropIndex("dbo.Jobs", new[] { "EmployeeId" });
            DropIndex("dbo.Jobs", new[] { "CustomerId" });
            DropIndex("dbo.CustomerProfiles", new[] { "Id" });
            DropTable("dbo.SkillJobs");
            DropTable("dbo.SkillEmployeeProfiles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sections");
            DropTable("dbo.Skills");
            DropTable("dbo.Offers");
            DropTable("dbo.EmployeeProfiles");
            DropTable("dbo.Jobs");
            DropTable("dbo.CustomerProfiles");
        }
    }
}
