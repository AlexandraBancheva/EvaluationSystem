using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101609)]
    public class ModuleQuestionInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("ModuleQuestion");
        }

        public override void Up()
        {
            Create.Table("ModuleQuestion")
                .WithColumn("IdModuleQuestion").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdModule").AsInt32().NotNullable().ForeignKey("ModuleTemplate", "IdModule")
                .WithColumn("IdQuestion").AsInt32().NotNullable().ForeignKey("QuestionTemplate", "Id")
                .WithColumn("Position").AsInt32();
        }
    }
}
