using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112211318)]
    public class AttestationInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("Attestation");
        }

        public override void Up()
        {
            Create.Table("Attestation")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdForm").AsInt32().ForeignKey("AttestationForm", "Id").NotNullable()
                .WithColumn("IdUserToEvaluate").AsInt32().ForeignKey("User", "IdUser").NotNullable()
                .WithColumn("CreateDate").AsDateTime2().NotNullable();
        }
    }
}