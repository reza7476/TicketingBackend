using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;


[Migration(202610220916)]
public class _202610220916_CreateTicketAssignmentTable : Migration
{

    public override void Up()
    {
        Create.Table("TicketAssignments")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("TicketId").AsString().NotNullable()
            .WithColumn("AssignedToUserId").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime2();

        Create.ForeignKey("FK_Ticket_TicketAssignment")
            .FromTable("TicketAssignments")
            .ForeignColumn("TicketId")
            .ToTable("Tickets")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.None);

    }
    public override void Down()
    {
        Delete.Table("TicketAssignments");
    }
}
