using FluentMigrator;


namespace Users.Dal.Migrations;

[Migration(20230517, TransactionBehavior.None)]
public class EnableUuidOsspExtension : Migration {
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
    }

    public override void Down()
    {
        Execute.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";");
    }
}