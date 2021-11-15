using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Services
{
    public class AnswersServices : IAnswersServices
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public AnswersServices(IAnswerRepository answerRepository, IQuestionRepository questionRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public AnswerDetailDto AddNewAnswer(int questionId, AddNewAnswerDto model)
        {
            var isExist = _questionRepository.GetById(questionId);
            if (isExist == null)
            {
                return null;
            }
            var current = _mapper.Map<AnswerTemplate>(model);
            current.IdQuestion = questionId;
            var id = _answerRepository.Insert(current);
            current.Id = id;
            
            return _mapper.Map<AnswerDetailDto>(current);
        }

        public void DeleteAnAnswer(int answerId)
        {
            var entity = _answerRepository.GetById(answerId);
            _answerRepository.Delete(entity);
        }

        public AnswerDetailDto UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model)
        {
            var entity = _mapper.Map<AnswerTemplate>(model);
            entity.Id = answerId;
            entity.IdQuestion = questionId;
            _answerRepository.Update(entity);

            return _mapper.Map<AnswerDetailDto>(entity);
        }
    }
}
