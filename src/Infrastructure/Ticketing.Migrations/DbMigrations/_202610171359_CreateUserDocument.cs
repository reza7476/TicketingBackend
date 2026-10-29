using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;

[Migration(202610171359)]
public class _202610171359_CreateUserDocument : Migration
{

    public override void Up()
    {
        Create.Table("UserDocuments")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("DocumentId").AsString().NotNullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("Extension").AsString().NotNullable();
        Create.ForeignKey("FK_UserDocuments_Users")
            .FromTable("UserDocuments")
            .ForeignColumn("UserId")
            .ToTable("Users")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

    }
    public override void Down()
    {
        Delete.Table("UserDocuments");
    }
}
