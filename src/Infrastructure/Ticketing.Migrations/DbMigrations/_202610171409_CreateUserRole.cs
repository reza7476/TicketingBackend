using FluentMigrator;

namespace Ticketing.Migrations.DbMigrations;


[Migration(202610171409)]
public class _202610171409_CreateUserRole : Migration
{

    public override void Up()
    {
        Create.Table("UserRoles")
        .WithColumn("Id").AsString().PrimaryKey().NotNullable()
        .WithColumn("UserId").AsString().NotNullable()
        .WithColumn("RoleId").AsString().NotNullable();

        Create.ForeignKey("FK_UserRoles_Users")
            .FromTable("UserRoles")
            .ForeignColumn("UserId")
            .ToTable("Users")
            .PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);


        Create.ForeignKey("FK_UserRoles_Roles")
             .FromTable("UserRoles")
             .ForeignColumn("RoleId")
             .ToTable("Roles")
             .PrimaryColumn("Id")
             .OnDelete(System.Data.Rule.None);
    }


    public override void Down()
    {

        Delete.Table("UserRoles");
    }
}
