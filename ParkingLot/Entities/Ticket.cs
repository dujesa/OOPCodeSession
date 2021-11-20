using System;

namespace ParkingLot.Entities
{
    public struct Ticket
    {
        public string Id { get; }
        public DateTime CreatedAt { get; }

        public Ticket(string id = "")
        {
            Id = id;
            CreatedAt = DateTime.Now;
        }

        public override bool Equals(object equtable)
        {
            if (equtable is Ticket ticket)
                return ticket.Id == Id;

            return false;
        }

        public override string ToString() => $"#{Id} [{CreatedAt.ToLongTimeString()}]";
    
        public bool IsValid() => !string.IsNullOrEmpty(Id);
    }
}
