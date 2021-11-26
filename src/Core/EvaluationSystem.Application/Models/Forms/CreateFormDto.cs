using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Models.Forms
{
    public class CreateFormDto
    {
        public string FormName { get; set; }

        public ICollection<CreateModuleDto> Module { get; set; }

        public ICollection<CreateQuestionDto> Question { get; set; }

        public ICollection<AddNewAnswerDto> Answer { get; set; }
    }
}