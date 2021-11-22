using EvaluationSystem.Application.Interfaces;
using Moq;
using NUnit.Framework;

namespace EvaluationSystem.Tests
{
    [TestFixture]
    public class QuestionServiceTests
    {
        private const int QuestionId = 1;
        private IQuestionsServices _questionService;

        public void SetUp()
        {
            var questionRepository = new Mock<IQuestionRepository>();
            var answerRepository = new Mock<IAnswerRepository>();
        }

        [Test]
        public void Verify_QuestionServiceGetById_ReturnsSameDto()
        {
            var res = _questionService.GetQuestionById(QuestionId);
            Assert.AreEqual(res.Id, QuestionId);
        }
    }
}
