using FluentMigrator;

namespace Users.Dal.Migrations;

[Migration(20230518, TransactionBehavior.None)]
public class InitSchema : Migration {
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("guid").AsGuid().WithDefault(SystemMethods.NewGuid).PrimaryKey("users_pk")
            .WithColumn("login").AsString().Unique().NotNullable()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("gender").AsInt32().NotNullable()
            .WithColumn("birthday").AsDateTimeOffset().Nullable()
            .WithColumn("admin").AsBoolean().NotNullable()
            .WithColumn("created_on").AsDateTimeOffset().NotNullable()
            .WithColumn("created_by").AsString().Nullable()
            .WithColumn("modified_on").AsDateTimeOffset().Nullable()
            .WithColumn("modified_by").AsString().Nullable()
            .WithColumn("revoked_on").AsDateTimeOffset().Nullable()
            .WithColumn("revoked_by").AsString().Nullable();

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