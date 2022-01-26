using System.ComponentModel;
using System.Collections.Generic;
using EvaluationSystem.Domain.Enums;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Models.Questions.QuestionsDtos
{
    public class CreateFormModuleQuestionDto
    {
        public CreateFormModuleQuestionDto()
        {
            this.Answers = new HashSet<CreateFormModuleQuestionAnswerDto>();
        }

        public string QuestionName { get; set; }

        public QuestionType Type { get; set; }

        public int QuestionPosition { get; set; }

        [DefaultValue("false")]
        public bool IsTemplate { get; set; }

        public virtual ICollection<CreateFormModuleQuestionAnswerDto> Answers { get; set; }

    }
}