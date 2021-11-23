using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Profiles.QuestionProfile;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Services;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Persistence.QuestionDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private const int QuestionId = 1;
        private IQuestionsServices _questionService;
        private QuestionDetailDto questionDetailDto;


        [SetUp]
        public void Setup()
        {
            questionDetailDto = new QuestionDetailDto()
            {
                Id = QuestionId
            };

            var answerRepoMock = new Mock<IAnswerRepository>();
            var questionRepoMock = new Mock<IQuestionRepository>();

            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _questionService = new QuestionsServices(new QuestionRepository(config), new MapperConfiguration((mc) =>
            {
                mc.AddMaps(typeof(QuestionProfile).Assembly);
            }).CreateMapper());
        }

        [Test]
        public void Verify_QuestionsServicesGetQuestionById_ReturnsSameIdDto()
        {
            var result = _questionService.GetQuestionById(QuestionId);
            Assert.That(result.Id == QuestionId);
        }

        [Test]
        public void Verify_QuestionsServicesCreateQuestion_ReturnsSameDto()
        {
            var insertable = new CreateQuestionDto()
            {
                QuestionName = "Test",
                Type = QuestionType.CheckBoxes,
                IsReusable = true,
            };

            var insert = _questionService.CreateNewQuestion(new CreateQuestionDto()
            { 
                QuestionName = "Test",
                Type = QuestionType.CheckBoxes,
                IsReusable = true,
            });

           // var result = _questionService.GetQuestionById(insert.Id);
            Assert.That(insertable.QuestionName == insert.QuestionName); // ???
        }

        //[Test]
        //public void Verify_QuestionsServicesUpdateQuestion_ReturnsSameDto()
        //{
        //    var entity = _questionService.GetQuestionById(QuestionId);
        //}

        //[Test]
        //public void Verify_QuestionsServicesDeleteQuestion_ReturnsNull()
        //{
        //    var entity = _questionService.GetQuestionById(10);
        //    _questionService.DeleteQuestion(entity.Id);
        //    Assert.IsTrue(false , "No Content");
        //}
    }
}