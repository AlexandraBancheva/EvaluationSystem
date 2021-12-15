using System.Collections.Generic;
using System.Linq;
using Dapper;
using EvaluationSystem.Application.Models.FormModules;
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

        public ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId)
        {
            var query = @"SELECT IdModule FROM FormModule
                            WHERE IdForm = @FormId";

            var formWithModules = _connection.Query<FormModuleGettingOnlyModulesDto>(query, new { FormId = formId });

            return (ICollection<FormModuleGettingOnlyModulesDto>)formWithModules;
        }

        public ICollection<FormModelDto> GetModulesByFormId(int formId)
        {
            var query = @"SELECT * FROM FormModule AS fm
                                JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule
                                WHERE fm.IdForm = @IdForm";

            var queryParameter = new { IdForm = formId };

            var formModuleDictionary = new Dictionary<int, FormModelDto>();
            var modules = _connection.Query<FormModelDto, ModuleTemplate, FormModelDto>(query, (form, module) =>
            {
                if (!formModuleDictionary.TryGetValue(form.IdForm, out var currentForm))
                {
                    currentForm = form;
                    formModuleDictionary.Add(currentForm.IdForm, currentForm);
                }

                currentForm.Modules.Add(module);
                return currentForm;
            }, queryParameter, _transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return modules;
        }
    }
}