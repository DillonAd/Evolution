﻿using Evolution.Data;
using Evolution.Data.Entity;
using System;
using System.Linq;

namespace Evolution.Repo
{
    public class EvolutionRepo : IEvolutionRepo
    {
        private readonly IEvolutionContext _Context;

        public EvolutionRepo(IEvolutionContext context)
        {
            _Context = context;
        }

        public string[] GetExecutedEvolutionFileNames() =>
             _Context.GetEvolutions()
                     .Select(e => e.FileName)
                     .ToArray();

        public void AddEvolution(Model.Evolution evolution, string content)
        {
            var newEvolution = new Data.Entity.Evolution()
            {
                Id = Guid.NewGuid(),
                Name = evolution.Name,
                FileName = evolution.FileName,
                Content = content
            };

            _Context.AddEvolution(newEvolution);
        }

        public void ExecuteEvolution(string content)
        {
            _Context.ExecuteEvolution(content);
        }
    }
}
