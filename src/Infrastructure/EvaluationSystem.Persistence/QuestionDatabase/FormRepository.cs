using System.Linq;
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

        public void DeleteForm(int formId)
        {
            var query = @"DELETE FROM FormModule
                            WHERE IdForm = @FormId
                            DELETE FROM FormTemplate
                            WHERE Id = @FormId";
            _connection.Execute(query, new { FormId = formId }, _transaction);
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

        // Repository in repository ???
        public IEnumerable<FormWithAllDto> GetAllWithFormId(int formId)
        {
           // var query = @"SELECT * FROM FormTemplate AS ft
           //                 JOIN FormModule AS fm ON fm.IdForm = ft.Id
           //                 JOIN ModuleTemplate AS mt ON mt.Id = fm.IdModule
           //                 JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
           //                 JOIN QuestionTemplate AS qt ON qt.Id = mq.IdQuestion
           //                 JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id
           //                 WHERE ft.Id = 29";

           // var formDictionary = new Dictionary<int, FormWithAllDto>();
           //// var moduleDictionary = new Dictionary<int, ModuleTemplate>();
           // var forms = _connection.Query<FormWithAllDto, ModuleInFormDto, FormWithAllDto>(query, (form, module) =>
           // { 
           //     if (!formDictionary.TryGetValue(form.FormId, out var currentForm))
           //     {
           //         currentForm = form;
           //         formDictionary.Add(currentForm.FormId, currentForm);
           //     }

           // });
            /*
             var lookup = new Dictionary<int, OrderDetail>();
            var lookup2 = new Dictionary<int, OrderLine>();
            connection.Query<OrderDetail, OrderLine, OrderLineSize, OrderDetail>(@"
                    SELECT o.*, ol.*, ols.*
                    FROM orders_mstr o
                    INNER JOIN order_lines ol ON o.id = ol.order_id
                    INNER JOIN order_line_size_relations ols ON ol.id = ols.order_line_id           
                    ", (o, ol, ols) =>
            {
                OrderDetail orderDetail;
                if (!lookup.TryGetValue(o.id, out orderDetail))
                {
                    lookup.Add(o.id, orderDetail = o);
                }
                OrderLine orderLine;
                if (!lookup2.TryGetValue(ol.id, out orderLine))
                {
                    lookup2.Add(ol.id, orderLine = ol);
                    orderDetail.OrderLines.Add(orderLine);
                }
                orderLine.OrderLineSizes.Add(ols);
                return orderDetail;
            }).AsQueryable();

            var resultList = lookup.Values.ToList();
             */
            return null;
        }
    }
}
