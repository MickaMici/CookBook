namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1f3993cf-c688-4281-a906-7aa4cad9008e', N'gost@gmail.com', 0, N'ADPIzZU7qQSEou8c8RW1Ag1ruWLDd4698bpSTgUmIfPainQT1UbZO7JfsnzFJeWwjA==', N'cc88eb3a-8132-476f-a081-0bb1d349adf3', NULL, 0, 0, NULL, 1, 0, N'gost@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'547d6605-d564-4361-b826-6f508a1a2578', N'milicaz994@gmail.com', 0, N'ACD8DzAdxA0ZsgFQqU5FQQ9Heu9//EAjUk3Ur+PnlGo5B8Nw3M6qX74YmDlACqBYyA==', N'a7fb5e97-14aa-4254-ba28-40f4fb579503', NULL, 0, 0, NULL, 1, 0, N'milicaz994@gmail.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b15c3024-b03c-43cb-a7da-3f5ead29e336', N'Admin')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'547d6605-d564-4361-b826-6f508a1a2578', N'b15c3024-b03c-43cb-a7da-3f5ead29e336')

");
        }
        
        public override void Down()
        {

        }
    }
}
