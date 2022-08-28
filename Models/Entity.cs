using CsvHelper.Configuration.Attributes;
using System;

namespace FootballProgrammes.Models
{
    public class Entity
    {
        [Optional]
        public int Id { get; set; }

        [Index(0)]
        public string Name { get; set; }

        [Optional]
        public string Description { get; set; }

        [Optional]
        public bool isDeleted { get; set; }
    }
}
