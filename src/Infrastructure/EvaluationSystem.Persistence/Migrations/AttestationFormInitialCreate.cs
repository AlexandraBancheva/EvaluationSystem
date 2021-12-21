using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112201730)]
    public class AttestationFormInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationForm");
        }

        public override void Up()
        {
            Create.Table("AttestationForm")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(200).NotNullable();
        }
    }
}
