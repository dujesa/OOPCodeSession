using ParkingLot.Entities;
using ParkingLot.Enums;

namespace ParkingLot.Helpers
{
    public static class CashBox
    {
        public static decimal TotalIncome = 0.0m;
        public static int TicketsSold = 0;

        private const decimal SmallPrice = 0.5m;
        private const decimal OffRoadPrice = 1.5m;
        private const decimal LuxuryPrice = 2.5m;
        private const decimal TruckPrice = 3.5m;

        public static void Charge(decimal ticketPrice)
        {
            TotalIncome += ticketPrice;
        }

        public static decimal CalculatePrice(VehicleType vehicleType, Ticket ticket)
        {
            var parkedTimeUnits = ticket.ParkedTime.Seconds;
            return parkedTimeUnits * CalculatePriceRate(vehicleType);
        }

        public static decimal CalculatePriceRate(VehicleType vehicleType)
        {
            var priceRate = vehicleType switch
            {
                VehicleType.OffRoad => OffRoadPrice,
                VehicleType.Luxury => LuxuryPrice,
                VehicleType.Truck => TruckPrice,
                _ => SmallPrice,
            };

            return priceRate;
        }

        public static Ticket GenerateTicket()
        {
            var ticketId = $"ticket-{++TicketsSold}";
            var ticket = new Ticket(ticketId);

            return ticket;
        }
    }
}
