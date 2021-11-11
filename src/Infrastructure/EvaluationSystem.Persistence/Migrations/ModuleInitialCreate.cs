using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101606)]
    public class ModuleInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("ModuleTemplate");
        }

        public override void Up()
        {
            Create.Table("ModuleTemplate")
                .WithColumn("IdModule").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable();
        }
    }
}
