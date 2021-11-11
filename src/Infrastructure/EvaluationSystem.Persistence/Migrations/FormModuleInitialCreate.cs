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
                .WithColumn("IdFormModule").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdForm").AsInt32().NotNullable().ForeignKey("FormTemplate", "IdForm")
                .WithColumn("IdModule").AsInt32().NotNullable().ForeignKey("ModuleTemplate", "IdModule")
                .WithColumn("Position").AsInt32();
        }
    }
}
