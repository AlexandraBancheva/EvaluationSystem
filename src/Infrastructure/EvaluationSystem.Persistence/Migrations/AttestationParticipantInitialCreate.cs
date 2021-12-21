using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112211326)]
    public class AttestationParticipantInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationParticipant");
        }

        public override void Up()
        {
            Create.Table("AttestationParticipant")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdAttestation").AsInt32().ForeignKey("Attestation", "Id").NotNullable()
                .WithColumn("IdUserParticipant").AsInt32().ForeignKey("User", "IdUser").NotNullable()
                .WithColumn("Status").AsString().NotNullable()
                .WithColumn("Position").AsString().NotNullable();
        }
    }
}
