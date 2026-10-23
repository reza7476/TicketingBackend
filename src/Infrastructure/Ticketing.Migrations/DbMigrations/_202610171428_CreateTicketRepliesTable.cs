using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;


[Migration(202610171428)]
public class _202610171428_CreateTicketRepliesTable : Migration
{

    public override void Up()
    {
        Create.Table("TicketReplies")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("TicketId").AsString().NotNullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("ApprovedByAdmin").AsBoolean()
            .WithColumn("IssuedBy").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime2()
            .WithColumn("Content").AsString().NotNullable();

        Create.ForeignKey("FK_TicketReplies_Tickets")
            .FromTable("TicketReplies")
            .ForeignColumn("TicketId")
            .ToTable("Tickets")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);
    }
    public override void Down()
    {
        Delete.Table("TicketReplies");
    }
}
