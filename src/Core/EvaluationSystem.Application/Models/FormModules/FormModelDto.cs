﻿using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Models.FormModules
{
    public class FormModelDto
    {
        public FormModelDto()
        {
            this.Modules = new HashSet<ModuleTemplate>();
        }

        public int IdForm { get; set; }

        public virtual ICollection<ModuleTemplate> Modules { get; set; }
    }
}
