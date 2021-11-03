namespace EvaluationSystem.Domain.Entities
{
    public class Answer
    {
        public Answer()
        {

        }

        public Answer(int id, string name, int questionId, string questionName)
        {
            this.Id = id;
            this.Name = name;
            this.QuestionId = questionId;
            this.QuestionName = questionName;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        // ForeignKey from Table Question
        public int QuestionId { get; set; }

        // [ForeignKey(nameof(Question))]
        public string QuestionName { get; set; }
    }
}