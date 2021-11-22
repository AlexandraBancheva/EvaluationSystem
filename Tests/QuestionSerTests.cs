using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Profiles.QuestionProfile;
using EvaluationSystem.Application.Services;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;

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
                Id = QuestionId,
            };

            var answerRepoMock = new Mock<IAnswerRepository>();
            var questionRepoMock = new Mock<IQuestionRepository>();

            var config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();

            _questionService = new QuestionsServices(new QuestionRepository(config), new MapperConfiguration((mc) =>
            {
                mc.AddMaps(typeof(QuestionProfile).Assembly);
            }).CreateMapper());
        }

        [Test]
        public void Verify_QuestionServiceGetById_ReturnsSameDto()
        {
            //var questionId = 1;
            //var questionName = "How are you";
            //var questionDto = new QuestionTemplate
            //{
            //    Id = questionId,
            //    Name = questionName,
            //};
            //_questionRepoMock.Setup(x => x.GetById(questionId)).Returns(questionDto);

            //var question = _sut.GetQuestionById(questionId);

            //Assert.That(question.Id == questionId);
        }
    }
}