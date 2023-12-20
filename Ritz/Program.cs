// Program.cs
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Ritz Hotel Reservation System!");

        Hotel ritzHotel = new Hotel();

        bool exit = false;

        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Display Available Rooms");
            Console.WriteLine("2. Make Reservation");
            Console.WriteLine("3. Cancel Reservation");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ritzHotel.DisplayAvailableRooms();
                    break;

                case "2":
                    ritzHotel.MakeReservation();
                    break;

                case "3":
                    ritzHotel.CancelReservation();
                    break;

                case "4":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        } while (!exit);
    }
}
