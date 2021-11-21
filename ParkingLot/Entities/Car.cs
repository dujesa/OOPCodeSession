using ParkingLot.Enums;

namespace ParkingLot.Entities
{
    public class Car
    {
        public string Id { get; set; }
        public VehicleType Type { get; set; }

        public static int Counter = 0;

        public Car()
        {
            Counter++;
        }

        public override string ToString() => $"{Type} car: {Id}";
        public decimal CalculatePriceRate()
        {
            if (Type is VehicleType.Small)
                return 0.5m;

            if (Type is VehicleType.OffRoad)
                return 1.5m;

            if (Type is VehicleType.Luxury)
                return 2.5m;

            if (Type is VehicleType.Truck)
                return 3.5m;

            return 0.5m;
        }
    }
}
