using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112211337)]
    public class UserAnswersInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("UserAnswer");
        }

        public override void Up()
        {
            Create.Table("UserAnswer")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt32().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt32().ForeignKey("AttestationParticipant", "Id").NotNullable()
                .WithColumn("IdAttestationModule").AsInt32().ForeignKey("AttestationModule", "Id").NotNullable()
                .WithColumn("IdAttestationQuestion").AsInt32().ForeignKey("AttestationQuestion", "Id").NotNullable()
                .WithColumn("IdAttestationAnswer").AsInt32().ForeignKey("AttestationAnswer", "Id").Nullable()
                .WithColumn("TextAnswer").AsString().Nullable();
        }
    }
}