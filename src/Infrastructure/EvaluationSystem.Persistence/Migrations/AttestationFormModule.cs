using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112201751)]
    public class AttestationFormModule : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationFormModule");
        }

        public override void Up()
        {
            Create.Table("AttestationFormModule")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdAttestationForm").AsInt32().ForeignKey("AttestationForm", "Id").NotNullable()
                .WithColumn("IdAttestationModule").AsInt32().ForeignKey("AttestationModule", "Id").NotNullable()
                .WithColumn("Position").AsInt32().NotNullable();
        }
    }
}