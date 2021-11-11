using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111101531)]
    public class QuestionsInitialCreate : Migration
    {
        public override void Down()
        {
            Delete.Table("QuestionTemplate");
        }

        public override void Up()
        {
            Create.Table("QuestionTemplate")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("DateOfCreation").AsDateTime2().NotNullable()
                .WithColumn("Type").AsString(20).NotNullable()
                .WithColumn("IsReusable").AsBoolean();
        }
    }
}
