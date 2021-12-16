﻿using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202112161736)]
    public class User : Migration
    {
        public override void Down()
        {
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                 .WithColumn("IdUser").AsInt32().PrimaryKey().Identity()
                 .WithColumn("Email").AsString(200).NotNullable()
                 .WithColumn("Name").AsString(200).NotNullable();
        }
    }
}