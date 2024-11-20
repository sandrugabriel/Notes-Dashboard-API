
using FluentMigrator;
using System.ComponentModel.DataAnnotations;

namespace Notes_Dashboard_API.Data.Migrations
{
    [Migration(2)]
    public class CreateTabels : Migration
    {
        public override void Down()
        {
            Delete.Table("Notes");
            Delete.Table("Categories");
            Delete.Table("Customers");
        }

        private void createTabels()
        {
            Create.Table("Notes")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("Title").AsString().NotNullable()
                 .WithColumn("CreateDate").AsString().NotNullable()
                 .WithColumn("Description").AsString().NotNullable()
                 .WithColumn("Category").AsString().NotNullable();

            Create.Table("NotesServices")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("CustomerId").AsInt32().NotNullable()
             .WithColumn("NoteId").AsInt32().NotNullable();

        }

        public override void Up()
        {


            Create.Table("Customers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString(256).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("NormalizedEmail").AsString(256).Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
            .WithColumn("PasswordHash").AsString().Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("PhoneNumber").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
            .WithColumn("LockoutEnd").AsDateTime().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
            .WithColumn("AccessFailedCount").AsInt32().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Discriminator").AsString().NotNullable();

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).Nullable()
                .WithColumn("NormalizedName").AsString().Nullable()
                .WithColumn("ConcurrencyStamp").AsInt32().Nullable();

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .ForeignKey("FK_AspNetUserRoles_Customers", "Customers", "Id")
                .ForeignKey("FK_AspNetUserRoles_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetRolesClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("RoleId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString(256).Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetUserClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("UserId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString().Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_Customers", "Customers", "Id");

            Create.Table("AspNetUserLogins")
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("ProviderKey").AsString(256).PrimaryKey()
           .WithColumn("ProviderDisplayName").AsString().Nullable()
           .WithColumn("UserId").AsInt32().NotNullable()
           .ForeignKey("FK_AspNetUserLogins_Customers", "Customers", "Id");


            Create.Table("AspNetUserTokens")
           .WithColumn("UserId").AsInt32().PrimaryKey()
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("Name").AsString(256).PrimaryKey()
           .WithColumn("Value").AsInt32().Nullable()
           .ForeignKey("FK_AspNetUserTokens_Customers", "Customers", "Id");


            createTabels();

            //  CreateCustomers();
            CreateNotes();
            CreateNoteServices();

            CreateRoles();
            AddPermissionsToRoles();
        }

        private void CreateRoles()
        {
            Insert.IntoTable("AspNetRoles").Row(new { Name = "Admin", NormalizedName = "ADMIN" });
            Insert.IntoTable("AspNetRoles").Row(new { Name = "Editor", NormalizedName = "EDITOR" });
        }

        private void CreateNoteServices()
        {
            Insert.IntoTable("NotesServices").Row(new { CustomerId = 1, NoteId = 2 });
            Insert.IntoTable("NotesServices").Row(new { CustomerId = 2, NoteId = 1});
            Insert.IntoTable("NotesServices").Row(new { CustomerId = 3, NoteId = 3 });

        }

        /*  private void CreateCustomers()
          {
              Insert.IntoTable("Customers").Row(new { Name = "gabi", Email = "gabi@gmail.com", Password = "gabi1234", PhoneNumber = "07737777" });
              Insert.IntoTable("Customers").Row(new { Name = "filip", Email = "fil@gmail.com", Password = "filip1234", PhoneNumber = "07757777" });
              Insert.IntoTable("Customers").Row(new { Name = "ana", Email = "ana@gmail.com", Password = "ana1234", PhoneNumber = "07767777" });
              Insert.IntoTable("Customers").Row(new { Name = "david", Email = "d@gmail.com", Password = "davi1234", PhoneNumber = "07797777" });

          }
  */

        private void CreateNotes()

        {

            //.WithColumn("Title").AsString().NotNullable()
            //.WithColumn("CreateDate").AsString().NotNullable()
            //.WithColumn("Description").AsString().NotNullable()

            Insert.IntoTable("Notes").Row(new { Id = 1, Title = "test1", CreateDate = "10 Nov 2024", Description = "asdasd...", Category = "social" });
            Insert.IntoTable("Notes").Row(new { Id = 2, Title = "test2", CreateDate = "10 Nov 2024", Description = "asdasd...", Category = "important" });
            Insert.IntoTable("Notes").Row(new { Id = 3, Title = "test3", CreateDate = "10 Nov 2024", Description = "asdasd...", Category = "social" });
        }

        private void AddPermissionsToRoles()
        {
            Insert.IntoTable("AspNetRolesClaims").Row(new
            {
                RoleId = "1",
                ClaimType = "Permission",
                ClaimValue = "1"
            });

            Insert.IntoTable("AspNetRolesClaims").Row(new
            {
                RoleId = "2",
                ClaimType = "Permission",
                ClaimValue = "1"
            });
        }
    }
}
