using ParkingLot.Helpers;
using System;

namespace ParkingLot.Entities
{
    public struct Ticket
    {
        public string Id { get; init; }
        public decimal PriceRate { get; set; }

        private DateTime _createdAt;

        public Ticket(string id = "", decimal priceRate = 0.0m)
        {
            Id = id;
            PriceRate = priceRate;
            _createdAt = DateTime.Now;
        }

        public override bool Equals(object equtable)
        {
            if (equtable is Ticket ticket)
                return ticket.Id == Id;

            return false;
        }

        public override string ToString() => $"#{Id}";

        public bool IsValid() => !string.IsNullOrEmpty(Id);

        public TimeSpan ParkedTime => DateTime.Now - _createdAt;
    }
}
