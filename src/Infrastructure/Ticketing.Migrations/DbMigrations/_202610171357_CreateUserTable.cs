using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;


[Migration(202610171357)]
public class _202610171357_CreateUserTable : Migration
{

    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("FirstName").AsString().NotNullable()
            .WithColumn("LastName").AsString().NotNullable()
            .WithColumn("Mobile").AsString()
            .WithColumn("HashPassword").AsString()
            .WithColumn("OrganizationId").AsString()
            .WithColumn("CreatedAt").AsDateTime2();
        Create.ForeignKey("Fk_UserOrganization_Organization")
            .FromTable("Users")
            .ForeignColumn("OrganizationId")
            .ToTable("Organizations")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.None);

    }


    public override void Down()
    {
        Delete.Table("Users");

    }
}
