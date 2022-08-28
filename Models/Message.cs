using System;

namespace FootballProgrammes.Models
{
    public class Message : Entity
    {
        public string MessageText { get; set; }

        public string ToUserId { get; set; }

        public string FromUserId { get; set; }

        public bool Read { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ReadDate { get; set; }
    }
}
