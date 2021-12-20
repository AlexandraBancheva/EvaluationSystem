using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112201720)]
    public class AttestationModule : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationModule");
        }

        public override void Up()
        {
            Create.Table("AttestationModule")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable();
        }
    }
}
