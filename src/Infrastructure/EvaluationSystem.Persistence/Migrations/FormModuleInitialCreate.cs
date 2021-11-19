using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101709)]
    public class FormModuleInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("FormModule");
        }

        public override void Up()
        {
            Create.Table("FormModule")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdForm").AsInt32().NotNullable().ForeignKey("FormTemplate", "Id")
                .WithColumn("IdModule").AsInt32().NotNullable().ForeignKey("ModuleTemplate", "Id")
                .WithColumn("Position").AsInt32();
        }
    }
}
