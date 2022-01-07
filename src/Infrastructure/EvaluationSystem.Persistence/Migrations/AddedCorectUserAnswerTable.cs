using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202201070951)]
    public class AddedCorectUserAnswerTable : Migration
    {
        public override void Down()
        {
            throw new System.NotImplementedException();
        }

        public override void Up()
        {
            Delete.Table("UserAnswer");

            Create.Table("UserAnswer")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt32().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt32().ForeignKey("User", "Id").NotNullable()
                .WithColumn("IdAttestationModule").AsInt32().ForeignKey("AttestationModule", "Id").NotNullable()
                .WithColumn("IdAttestationQuestion").AsInt32().ForeignKey("AttestationQuestion", "Id").NotNullable()
                .WithColumn("IdAttestationAnswer").AsInt32().ForeignKey("AttestationAnswer", "Id").Nullable()
                .WithColumn("TextAnswer").AsString().Nullable();
        }
    }
}
