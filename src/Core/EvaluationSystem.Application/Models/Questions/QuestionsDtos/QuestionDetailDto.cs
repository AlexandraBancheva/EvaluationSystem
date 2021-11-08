using System;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class QuestionDetailDto
    {
        public string QuestionName { get; set; }

        public string Type { get; set; }

        public bool  IsReusable { get; set; }
    }
}
