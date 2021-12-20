using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112201706)]
    public class AttestationQuestion : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationQuestion");
        }

        public override void Up()
        {
            Create.Table("AttestationQuestion")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("DateOfCreation").AsDateTime2().NotNullable()
                .WithColumn("Type").AsString(20).NotNullable()
                .WithColumn("IsReusable").AsBoolean();
        }
    }
}
