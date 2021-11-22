namespace EvaluationSystem.Domain.Entities
{
    public class ModuleQuestion
    {
        public int Id { get; set; }

        public int IdModule { get; set; }

        public int IdQuestion { get; set; }

        public int Position { get; set; }
    }
}
