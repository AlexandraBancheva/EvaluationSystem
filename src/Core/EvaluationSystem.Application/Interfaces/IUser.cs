﻿namespace EvaluationSystem.Application.Interfaces
{
    public interface IUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
