using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;

[Migration(202610171351)]
public class _202610171351_CreateRolesTable : Migration
{

    public override void Up()
    {
        Create.Table("Roles")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Description").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Roles");
    }
}
