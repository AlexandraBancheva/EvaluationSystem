namespace EvaluationSystem.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }

        public string Name { get; set; }


        // ForeignKey from Table Question
        public int QuestionId { get; set; }

        // [ForeignKey(nameof(Question))]
       // public Question Question { get; set; }
    }
}
