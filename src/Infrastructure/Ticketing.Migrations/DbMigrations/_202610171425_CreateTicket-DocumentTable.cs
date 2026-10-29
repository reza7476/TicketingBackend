using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;



[Migration(202610171425)]
public class _202610171425_CreateTicket_DocumentTable : Migration
{

    public override void Up()
    {
        Create.Table("TicketDocuments")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("DocumentId").AsString().NotNullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("TicketId").AsString().NotNullable()
            .WithColumn("Extension").AsString().NotNullable();
        Create.ForeignKey("FK_TicketDocuments_Tickets")
            .FromTable("TicketDocuments")
            .ForeignColumn("TicketId")
            .ToTable("Tickets")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);
    } 

    public override void Down()
    {
        Delete.Table("TicketDocuments");
    }
}
