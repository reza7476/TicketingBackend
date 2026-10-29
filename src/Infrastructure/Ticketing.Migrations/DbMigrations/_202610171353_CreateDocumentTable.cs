using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;


[Migration(202610171353)]
public class _202610171353_CreateDocumentTable : Migration
{

    public override void Up()
    {
        Create.Table("Documents")
             .WithColumn("Id").AsString().PrimaryKey().NotNullable()
             .WithColumn("ObjectName").AsString().NotNullable()
             .WithColumn("ContentType").AsString().NotNullable()
             .WithColumn("Size").AsInt32()
             .WithColumn("OriginalFileName").AsString()
             .WithColumn("CreatedAt").AsDateTime2();
    }
    public override void Down()
    {
        Delete.Table("Documents");
    }
}
