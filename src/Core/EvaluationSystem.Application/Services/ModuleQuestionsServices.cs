using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.ModuleQuestions;
using System;

namespace EvaluationSystem.Application.Services
{
    public class ModuleQuestionsServices : IModuleQuestionsServices
    {
        private readonly IModuleQuestionRepository _moduleQuestionRepository;
        private readonly IMapper _mapper;

        public ModuleQuestionsServices(IModuleQuestionRepository moduleQuestionRepository, IMapper mapper)
        {
            _moduleQuestionRepository = moduleQuestionRepository;
            _mapper = mapper;
        }

        public void AddQuestionToModule(int moduleId, int questionId, int position)
        {
            var isExistQuestionId = CheckIfQuestionIdExists(moduleId, questionId, _moduleQuestionRepository);
            if (isExistQuestionId == false)
            {
                throw new InvalidOperationException("The question is already added.");
            }
            _moduleQuestionRepository.AddNewQuestionToModule(moduleId, questionId, position);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            _moduleQuestionRepository.DeleteQuestionFromModule(moduleId, questionId);
        }
         
        public IEnumerable<ListModulesQuestionsDto> GetAllModulesWithAllQuestions()
        {
            var modules = _moduleQuestionRepository.GetModuleWithAllQuestions();
            var allModules = _mapper.Map<IEnumerable<ListModulesQuestionsDto>>(modules);

            return allModules;
        }

        public IEnumerable<ListModulesQuestionsDto> GetModuleWithAllQuestions(int moduleId)
        {
            var moduleQuestions = _moduleQuestionRepository.GetAllQuestionsByModuleId(moduleId);
            var res = _mapper.Map<IEnumerable<ListModulesQuestionsDto>>(moduleQuestions);

            return res;
        }

        public static bool CheckIfQuestionIdExists(int moduleId, int questionId, IModuleQuestionRepository moduleQuestionRepository)
        {
            var allQuestionIds = moduleQuestionRepository.GetAllQuestionIdsByModuleId(moduleId);

            foreach (var id in allQuestionIds)
            {
                if (id.IdQuestion == questionId)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
