﻿using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class FormModuleRepository : BaseRepository<FormModule>, IFormModuleRepository
    {
        public FormModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork) 
        {

        }

        public void AddNewModuleInForm(int formId, int moduleId, int position)
        {
            var query = @"INSERT INTO FormModule
                            VALUES (@IdForm, @IdModule, @Position)";
            _connection.Execute(query, new { IdForm = formId, IdModule = moduleId, Position = position }, _transaction);
        }

        public void DeleteModuleFromForm(int formId, int moduleId)
        {
            var query = @"DELETE FROM FormModule 
                            WHERE IdForm = @FormId AND IdModule = @ModuleId";

            _connection.Execute(query, new { FormId = formId, ModuleId = moduleId}, _transaction);
        }
    }
}