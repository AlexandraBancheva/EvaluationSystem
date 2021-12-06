﻿using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IFormRepository : IRepository<FormTemplate>
    {
        void DeleteForm(int formId);

        IEnumerable<FormWithAllDto> AllForms();

        IEnumerable<FormWithAllDto> GetAllWithFormId(int formId);
    }
}
