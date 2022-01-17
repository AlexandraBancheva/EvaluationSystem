using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.FormModules;

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
            Connection.Execute(query, new { IdForm = formId, IdModule = moduleId, Position = position }, transaction: Transaction);
        }

        public void DeleteModuleFromForm(int formId, int moduleId)
        {
            var query = @"DELETE FROM FormModule 
                            WHERE IdForm = @FormId AND IdModule = @ModuleId";

            Connection.Execute(query, new { FormId = formId, ModuleId = moduleId}, transaction: Transaction);
        }

        public ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId)
        {
            var query = @"SELECT IdModule FROM FormModule
                            WHERE IdForm = @FormId";

            var queryParameter = new { FormId = formId };
            var formWithModules = Connection.Query<FormModuleGettingOnlyModulesDto>(query, queryParameter, transaction: Transaction);

            return (ICollection<FormModuleGettingOnlyModulesDto>)formWithModules;
        }

        public ICollection<FormModelDto> GetModulesByFormId(int formId)
        {
            var query = @"SELECT * FROM FormModule AS fm
                                JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule
                                WHERE fm.IdForm = @IdForm";

            var queryParameter = new { IdForm = formId };
            var formModuleDictionary = new Dictionary<int, FormModelDto>();
            var modules = Connection.Query<FormModelDto, ModuleTemplate, FormModelDto>(query, (form, module) =>
            {
                if (!formModuleDictionary.TryGetValue(form.IdForm, out var currentForm))
                {
                    currentForm = form;
                    formModuleDictionary.Add(currentForm.IdForm, currentForm);
                }

                currentForm.Modules.Add(module);
                return currentForm;
            }, queryParameter, transaction: Transaction,
               splitOn: "Id")
            .Distinct()
            .ToList();

            return modules;
        }
    }
}