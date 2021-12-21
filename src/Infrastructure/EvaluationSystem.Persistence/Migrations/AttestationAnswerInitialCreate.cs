using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112201718)]
    public class AttestationAnswerInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("AttestationAnswer");
        }

        public override void Up()
        {
            Create.Table("AttestationAnswer")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IsDefault").AsBoolean()
                .WithColumn("Position").AsInt32().NotNullable()
                .WithColumn("AnswerText").AsString(100).NotNullable()
                .WithColumn("IdQuestion").AsInt32().NotNullable().ForeignKey("AttestationQuestion", "Id").NotNullable();
        }
    }
}
