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
    }
}
