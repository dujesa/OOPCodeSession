using ParkingLot.Entities;
using ParkingLot.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLot
{
    internal class Program
    {
        #region Setup
        public static Random NumberGenerator = new Random();
        public static int RandomNumber => NumberGenerator.Next(999);
        #endregion

        public static bool IsWorkShift = false;
        public static decimal TotalIncome = 0.0m;
        public static int TicketsSold = 0;
        public static IDictionary<Ticket, Car> ParkingCars = new Dictionary<Ticket, Car>();


        public static void Main()
        {
            do
            {
                var menuInput = ProvideMenuInput();
                ExecuteActionFromMenu(menuInput);
            } while (IsWorkShift);
        }

        public static void ExecuteActionFromMenu(int menuInput)
        {
            switch (menuInput)
            {
                case 0:
                    IsWorkShift = !IsWorkShift;
                    break;
                case 1:
                    var newTicket = GenerateTicket();
                    LetNewCarIn(newTicket);
                    break;
                case 2:
                    var ticket = GetTicket();
                    var car = GetCarFromTicket(ticket);
                    if (car is null) return;

                    ChargeTicket(ticket);
                    LetCarOut(car, ticket);
                    break;
                case 3:
                    DisplayStats();
                    break;
                default:
                    break;
            }
        }

        private static void DisplayCars()
        {
            foreach (var parkingCar in ParkingCars)
            {
                var (ticket, car) = parkingCar;
                Console.WriteLine($"\t{ticket} - {car}");
            }
        }

        private static void LetCarOut(Car car, Ticket ticket)
        {
            ParkingCars.Remove(ticket);

            Console.WriteLine($"Car [{car}] left parking lot.");
            Pause();
        }

        private static void ChargeTicket(Ticket ticket)
        {
            var pricePerSecond = 0.99m;
            var parkingTime = DateTime.Now - ticket.CreatedAt;

            var ticketPrice = parkingTime.Seconds * pricePerSecond;
            TotalIncome += ticketPrice;
            Console.WriteLine($"Paid {ticketPrice} HRK.");
        }

        private static Car GetCarFromTicket(Ticket ticket)
        {
            ParkingCars.TryGetValue(ticket, out var car);
            return car;
        }

        private static void LetNewCarIn(Ticket newTicket)
        {
            var newCar = new Car
            {
                Id = $"ZG-{RandomNumber}-AJ",
            };


            Console.Clear();
            Console.WriteLine("" +
                "======================================\n" +
                "Please input car type:\n" +
                $"\t{(int)VehicleType.Truck} - {VehicleType.Truck}\n" +
                $"\t{(int)VehicleType.Luxury} - {VehicleType.Luxury}\n" +
                $"\t{(int)VehicleType.OffRoad} - {VehicleType.OffRoad}\n" +
                $"\t{(int)VehicleType.Small}/anything else - {VehicleType.Small}\n" +
                "======================================\n");

            int.TryParse(Console.ReadLine(), out int typeInput);
            switch (typeInput)
            {
                case (int)VehicleType.Truck:
                    newCar.Type = VehicleType.Truck;
                    break;

                case (int)VehicleType.OffRoad:
                    newCar.Type = VehicleType.OffRoad;
                    break;

                case (int)VehicleType.Luxury:
                    newCar.Type = VehicleType.Luxury;
                    break;

                case (int)VehicleType.Small:
                    newCar.Type = VehicleType.Small;
                    break;

                default:
                    newCar.Type = VehicleType.Small;
                    break;
            }

            ParkingCars.Add(newTicket, newCar);

            Console.WriteLine($"Car [{newCar}] entered parking lot.");
            Pause();
        }

        private static Ticket GenerateTicket()
        {
            var ticketId = $"ticket-{++TicketsSold}";
            var ticket = new Ticket(ticketId);

            return ticket;
        }

        public static Ticket GetTicket()
        {
            var ticketInput = string.Empty;
            var ticket = new Ticket();

            do
            {
                Console.Clear();
                Console.WriteLine("" +
                    "======================================\n" +
                    "Please input ticket no.:\n" +
                    "======================================\n");

                ticketInput = Console.ReadLine();
                ticket = ValidateTicket(ticketInput);

                if (string.IsNullOrEmpty(ticketInput) && ticket.IsValid())
                {
                    Console.WriteLine("Invalid ticket number, please try again!");
                    Pause();
                    continue;
                }

                Pause();
            }
            while (!ticket.IsValid());

            return ticket;
        }

        public static Ticket ValidateTicket(string ticketId)
        {
            Ticket validTicket = new Ticket();

            foreach (var carTicket in ParkingCars.Keys)
            {
                if (ticketId != carTicket.Id)
                    continue;

                validTicket = carTicket;
            }

            return validTicket;
        }

        public static int ProvideMenuInput()
        {
            var isInputed = false;
            var menuInput = 0;


            while (!isInputed)
            {
                Console.Clear();
                Console.WriteLine("" +
                    "======================================\n" +
                    MenuContent() +
                    "======================================\n");

                isInputed = int.TryParse(Console.ReadLine(), out menuInput);
                if (isInputed)
                {
                    break;
                }

                Console.WriteLine("Invalid menu input, please try again!");
                Pause();
            }

            return menuInput;
        }

        private static void DisplayStats()
        {
            Console.WriteLine("" +
                "====================\n" +
                "Stats:\n" +
                $"No. of cars in lot: {ParkingCars.Count}\n");
            DisplayCars();
            Console.WriteLine("\n" +
                $"Total income: {TotalIncome} HRK\n" +
                "====================\n"
                );

            Pause();
        }

        private static string MenuContent()
        {
            if (IsWorkShift)
            {
                return
                    "Menu:\n" +
                    "0 - End work shift\n" +
                    "1 - Generate ticket and let new car in\n" +
                    "2 - Bill and let car out\n" +
                    "3 - Display stats\n";
            }

            return
                "Menu:\n" +
                "0 - Start work shift\n";
        }

        private static void Pause()
        {
            Task.Delay(2000).Wait();
        }
    }
}
