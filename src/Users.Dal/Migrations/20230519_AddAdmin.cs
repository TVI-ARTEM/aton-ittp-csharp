using FluentMigrator;

namespace Users.Dal.Migrations;

[Migration(20230519, TransactionBehavior.None)]
public class AddAdmin : Migration {
    public override void Up()
    {
        Insert
            .IntoTable("users")
            .Row(new
                {
                    guid = Guid.NewGuid(),
                    login = "admin",
                    password = "admin",
                    name = "Admin",
                    gender = 2,
                    admin = true,
                    created_on = DateTime.UtcNow,
                }
            );

    }

    public override void Down()
    {
        Delete.FromTable("users")
            .Row(new { Login = "admin" });
    }
}