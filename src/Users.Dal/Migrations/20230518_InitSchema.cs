using FluentMigrator;

namespace Users.Dal.Migrations;

[Migration(20230518, TransactionBehavior.None)]
public class InitSchema : Migration {
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("guid").AsGuid().PrimaryKey("users_pk").Identity()
            .WithColumn("login").AsString().Unique().NotNullable()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("gender").AsInt32().NotNullable()
            .WithColumn("birthday").AsDateTimeOffset()
            .WithColumn("admin").AsBoolean().NotNullable()
            .WithColumn("created_on").AsDateTimeOffset().NotNullable()
            .WithColumn("created_by").AsString().NotNullable()
            .WithColumn("modified_on").AsDateTimeOffset()
            .WithColumn("modified_by").AsString()
            .WithColumn("revoked_on").AsDateTimeOffset()
            .WithColumn("revoked_by").AsString();

        Create.ForeignKey()
            .FromTable("users").ForeignColumn("created_by")
            .ToTable("users").PrimaryColumn("login");
        
        Create.ForeignKey()
            .FromTable("users").ForeignColumn("modified_by")
            .ToTable("users").PrimaryColumn("login");
        
        Create.ForeignKey()
            .FromTable("users").ForeignColumn("revoked_by")
            .ToTable("users").PrimaryColumn("login");
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}