﻿using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Application.Repositories;
using System.Collections.Generic;

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
    }
}
