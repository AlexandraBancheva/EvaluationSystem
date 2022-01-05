using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112161736)]
    public class UserInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("Name").AsString(200).NotNullable()
                 .WithColumn("Email").AsString(200).NotNullable();
        }
    }
}