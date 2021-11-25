﻿using System.Linq;
using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class FormRepository : BaseRepository<FormTemplate>, IFormRepository
    {
        public FormRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteForm(int id)
        {
            var query = @"DELETE FROM FormModule
                            WHERE IdForm = @FormId
                            DELETE FROM FormTemplate
                            WHERE Id = @FormId";
            _connection.Execute(query, new { FormId = id }, _transaction);
        }

        public IEnumerable<FormTemplateDto> FormsWithModules()
        {
            var query = @"SELECT * FROM FormTemplate AS ft
                            JOIN FormModule AS fm ON fm.IdForm = ft.Id
                            JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule";

            var formDictionary = new Dictionary<int, FormTemplateDto>();
            var forms = _connection.Query<FormTemplateDto, ModuleTemplateDto, FormTemplateDto>(query, (form, module) =>
            {
                if (!formDictionary.TryGetValue(form.Id, out var currentForm))
                {
                    currentForm = form;
                    formDictionary.Add(currentForm.Id, currentForm);
                }

                currentForm.Modules.Add(module);
                return currentForm;
            }, _transaction, splitOn: "Id")
                .Distinct()
                .ToList();

            return forms;
        }
    }
}
