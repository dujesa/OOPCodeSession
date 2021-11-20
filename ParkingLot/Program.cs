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


        public static void Main()
        {
            var parkingCars = new Dictionary<string, string>();

            do
            {
                var menuInput = ProvideMenuInput();
                ExecuteActionFromMenu(menuInput, parkingCars);
            } while (IsWorkShift);
        }

        public static void ExecuteActionFromMenu(int menuInput, IDictionary<string, string> parkingCars)
        {
            switch (menuInput)
            {
                case 0:
                    IsWorkShift = !IsWorkShift;
                    break;
                case 1:
                    var newTicket = GenerateTicket(parkingCars);
                    LetNewCarIn(newTicket, parkingCars);
                    break;
                case 2:
                    var ticket = GetTicket();
                    var car = ValidateTicket(ticket, parkingCars);
                    if (car is null) return;
                    ChargeTicket(ticket);
                    LetCarOut(car, parkingCars);
                    break;
                case 3:
                    DisplayStats(parkingCars);
                    break;
                default:
                    break;
            }
        }

        private static void DisplayCars(IDictionary<string, string> parkingCars)
        {
            foreach (var car in parkingCars)
            {
                var (ticket, registration) = car;
                Console.WriteLine($"\t[{ticket}] - {registration}");
            }
        }

        private static void LetCarOut(string car, IDictionary<string, string> parkingCars)
        {
            parkingCars.Remove(car);
        }

        private static void ChargeTicket(string ticket)
        {
            var ticketPrice = 9.99m;
            TotalIncome += ticketPrice;
        }

        private static string? ValidateTicket(string ticket, IDictionary<string, string> parkingCars)
        {
            var isValid = parkingCars.TryGetValue(ticket, out var car);
            if (isValid)
            {
                parkingCars.Remove(ticket);
            }

            return car;
        }

        private static void LetNewCarIn(string newTicket, IDictionary<string, string> parkingCars)
        {
            var newCar = $"ZG-{RandomNumber}-AJ";
            parkingCars.Add(newTicket, newCar);

            Console.WriteLine($"Car [{newCar}] entered parking lot.");
            Pause();
        }

        private static string GenerateTicket(IDictionary<string, string> parkingCars)
        {
            return $"ticket-{++TicketsSold}";
        }

        public static string GetTicket()
        {
            var ticketInput = string.Empty;

            do
            {
                Console.Clear();
                Console.WriteLine("" +
                    "======================================\n" +
                    "Please input ticket no.:\n" +
                    "======================================\n");

                ticketInput = Console.ReadLine();
                if (string.IsNullOrEmpty(ticketInput))
                {
                    Console.WriteLine("Invalid ticket number, please try again!");
                }

                Pause();
            }
            while (string.IsNullOrEmpty(ticketInput));

            return ticketInput;
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

        private static void DisplayStats(IDictionary<string, string> parkingCars)
        {
            Console.WriteLine("" +
                "====================\n" +
                "Stats:\n" +
                $"No. of cars in lot: {parkingCars.Count}\n");
            DisplayCars(parkingCars);
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
