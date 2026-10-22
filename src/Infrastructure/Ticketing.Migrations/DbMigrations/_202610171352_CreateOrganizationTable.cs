using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;

[Migration(202610171352)]
public class _202610171352_CreateOrganizationTable : Migration
{

    public override void Up()
    {
        Create.Table("Organizations")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("Name").AsString().NotNullable();
    }
    public override void Down()
    {
        Delete.Table("Organizations");
    }
}
