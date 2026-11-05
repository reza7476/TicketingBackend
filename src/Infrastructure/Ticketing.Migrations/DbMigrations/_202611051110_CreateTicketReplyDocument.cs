using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;


[Migration(202611051110)]
public class _202611051110_CreateTicketReplyDocument : Migration
{

    public override void Up()
    {
        Create.Table("ReplyDocuments")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("DocumentId").AsString().NotNullable()
            .WithColumn("Extension").AsString().NotNullable()
            .WithColumn("ReplyId").AsString().NotNullable();
        Create.ForeignKey("FK_ReplyDocuments_Reply")
            .FromTable("ReplyDocuments")
            .ForeignColumn("ReplyId")
            .ToTable("TicketReplies")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

    }


    public override void Down()
    {
        Delete.Table("ReplyDocuments");
    }
}
