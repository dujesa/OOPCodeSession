using System;

namespace ParkingLot.Entities
{
    public struct Ticket
    {
        public string Id { get; init; }
        public decimal PriceRate { get; set; }
        public decimal Price
        {
            get
            {
                var parkedTimeUnits = ParkedTime().Seconds;
                return parkedTimeUnits * PriceRate;
            }
        }

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

        public override string ToString() => $"#{Id} [Currently: {Price} HRK])";

        public bool IsValid() => !string.IsNullOrEmpty(Id);


        private TimeSpan ParkedTime()
        {
            return DateTime.Now - _createdAt;
        }
    }
}
