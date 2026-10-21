using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations
{

    [Migration(202610171421)]
    public class _202610171421_CreateTicketsTable : Migration
    {

        public override void Up()
        {
            Create.Table("Tickets")
                .WithColumn("Id").AsString().PrimaryKey().NotNullable()
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Content").AsString().NotNullable()
                .WithColumn("Priority").AsByte()
                .WithColumn("Status").AsByte()
                .WithColumn("Number").AsInt64().Identity()
                .WithColumn("CreatedAt").AsDateTime2();
        }


        public override void Down()
        {
            Delete.Table("Tickets");
        }
    }
}
