using System;

namespace FootballProgrammes.Models
{
    public class MediaFile : Entity
    {
        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        public DateTime DateAdded { get; set; }

        public int? FootballProgrammeId { get; set; }

        public FootballProgramme FootballProgramme { get; set; }

        public int? TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public int? BookId { get; set; }

        public Book Book { get; set; }
    }
}
