using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101612)]
    public class FormTemplateInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("FormTemplate");
        }

        public override void Up()
        {
            Create.Table("FormTemplate")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(200).NotNullable();
        }
    }
}
