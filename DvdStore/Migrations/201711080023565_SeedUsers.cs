namespace DvdStore.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'21bfa7fe-9e61-4825-9a5a-c3bfe95e9942', N'admin@DvdStore.com', 0, N'AKkUrxqaqaE8o6PU+kS1IoC8FaL2q5SVEVy0l4emEW2ZCmj4+o1x9/bSBdk8n5UFxw==', N'164dfbfc-9f97-4aa2-b262-20bfb7b00874', NULL, 0, 0, NULL, 1, 0, N'admin@DvdStore.com')");
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'b1088777-70d4-4a9e-93d6-4042c8d11a59', N'guest@DvdStore.com', 0, N'AGFUA2THboPpm5mhkeZgbr9Y+atI8xIWpLtQOkQDrUVsPDseuzsrjddU8NnIW2KI0A==', N'f55f7f27-b7c7-492c-aa06-a1f98520ca75', NULL, 0, 0, NULL, 1, 0, N'guest@DvdStore.com')");

            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7857942b-ab37-4e9c-b4af-449d1efe2904', N'MovieManager')");

            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'21bfa7fe-9e61-4825-9a5a-c3bfe95e9942', N'7857942b-ab37-4e9c-b4af-449d1efe2904')");

        }
        public override void Down()
        {
        }
    }
}
