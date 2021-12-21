using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112201726)]
    public class AttestationModuleQuestionInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationModuleQuestion");
        }

        public override void Up()
        {
            Create.Table("AttestationModuleQuestion")
                .WithColumn("IdAttestationModuleQuestion").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdAttestationModule").AsInt32().ForeignKey("AttestationModule", "Id").NotNullable()
                .WithColumn("IdAttestationQuestion").AsInt32().ForeignKey("AttestationQuestion", "Id").NotNullable()
                .WithColumn("Position").AsInt32().NotNullable();
        }
    }
}