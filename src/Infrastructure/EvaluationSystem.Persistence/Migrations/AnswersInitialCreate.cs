using FluentMigrator;


namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101604)]
    public class AnswersInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("AnswerTemplate");
        }

        public override void Up()
        {
            Create.Table("AnswerTemplate")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IsDefault").AsBoolean()
                .WithColumn("Position").AsInt32()
                .WithColumn("AnswerText").AsString(100).NotNullable()
                .WithColumn("IdQuestion").AsInt32().NotNullable().ForeignKey("QuestionTemplate", "Id");
        }
    }
}
