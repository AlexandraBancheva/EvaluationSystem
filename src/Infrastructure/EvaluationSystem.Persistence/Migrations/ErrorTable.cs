//using FluentMigrator;

//namespace EvaluationSystem.Persistence.Migrations
//{
//    [Migration(202111181417)]
//    public class ErrorTable : Migration
//    {
//        public override void Down()
//        {
//            Delete.Table("ErrorTable");
//        }

//        public override void Up()
//        {
//            Create.Table("ErrorTable")
//                .WithColumn("Id").AsInt32().Identity()
//                .WithColumn("Date").AsDateTime2()
//                .WithColumn("Level").AsString()
//                .WithColumn("Message").AsString(300).NotNullable();
//        }
//    }
//}
