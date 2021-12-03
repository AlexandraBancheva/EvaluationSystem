using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112031626)]
    public class NlogTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Nlogs");
        }

        public override void Up()
        {
            Create.Table("Nlogs")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Date").AsDateTime2().NotNullable()
                .WithColumn("[Level]").AsString().NotNullable()
                .WithColumn("Message").AsString(int.MaxValue);
        }
    }
}
