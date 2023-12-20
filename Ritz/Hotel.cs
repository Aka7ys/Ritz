// Hotel.cs

using System;
using System.Collections.Generic;
using System.Threading;

class Hotel
{
    private List<Room> rooms;
    private List<Reservation> reservations;
    private int reservationIdCounter = 1;

    public Hotel()
    {
        InitializeRooms();
        reservations = new List<Reservation>();
    }

    private void InitializeRooms()
    {
        rooms = new List<Room>
        {
            new Room { RoomNumber = 101, RoomClass = "Luxury", Capacity = 3, IsAvailable = true },
            new Room { RoomNumber = 201, RoomClass = "Premium", Capacity = 2, IsAvailable = true },
            new Room { RoomNumber = 301, RoomClass = "Standard", Capacity = 1, IsAvailable = true },
            new Room { RoomNumber = 401, RoomClass = "Exclusive", Capacity = 3, IsAvailable = true },
        };
    }

    public void DisplayAvailableRooms()
    {
        Console.WriteLine("Available Rooms:");
        foreach (var room in rooms)
        {
            if (room.IsAvailable)
            {
                Console.WriteLine($"Room {room.RoomNumber} - {room.RoomClass} - Capacity: {room.Capacity}");
            }
        }
    }

    public void MakeReservation()
    {
        Console.WriteLine("\nMake a Reservation:");

        Console.Write("Enter Guest Name: ");
        string guestName = Console.ReadLine();

        Console.WriteLine("Select Room Class:");
        Console.WriteLine("1. Luxury");
        Console.WriteLine("2. Premium");
        Console.WriteLine("3. Standard");
        Console.WriteLine("4. Exclusive");

        Console.Write("Enter Room Class (1-4): ");
        int roomClassChoice;
        while (!int.TryParse(Console.ReadLine(), out roomClassChoice) || roomClassChoice < 1 || roomClassChoice > 4)
        {
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
        }

        string roomClass;
        switch (roomClassChoice)
        {
            case 1:
                roomClass = "Luxury";
                break;
            case 2:
                roomClass = "Premium";
                break;
            case 3:
                roomClass = "Standard";
                break;
            case 4:
                roomClass = "Exclusive";
                break;
            default:
                Console.WriteLine("Invalid choice. Defaulting to Luxury.");
                roomClass = "Luxury";
                break;
        }

        Console.Write("Enter Check-In Date (yyyy-MM-dd): ");
        DateTime checkInDate;
        while (!DateTime.TryParse(Console.ReadLine(), out checkInDate))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
        }

        Console.Write("Enter Check-Out Date (yyyy-MM-dd): ");
        DateTime checkOutDate;
        while (!DateTime.TryParse(Console.ReadLine(), out checkOutDate) || checkOutDate <= checkInDate)
        {
            Console.WriteLine("Invalid date. Check-Out Date must be later than Check-In Date. Please enter a valid date (yyyy-MM-dd).");
        }

        Console.Write("Enter Number of Rooms (1-3): ");
        int numberOfRooms;
        while (!int.TryParse(Console.ReadLine(), out numberOfRooms) || numberOfRooms < 1 || numberOfRooms > 3)
        {
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
        }

        Console.Write("Enter Number of People: ");
        int numberOfPeople;
        while (!int.TryParse(Console.ReadLine(), out numberOfPeople) || numberOfPeople < 1)
        {
            Console.WriteLine("Invalid choice. Please enter a number greater than 0.");
        }

        var selectedRooms = rooms.FindAll(r => r.RoomClass == roomClass && r.IsAvailable && r.Capacity >= numberOfPeople);

        if (selectedRooms.Count >= numberOfRooms)
        {
            Reservation reservation = new Reservation
            {
                ReservationId = reservationIdCounter++,
                RoomNumber = selectedRooms[0].RoomNumber,
                GuestName = guestName,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                Confirmed = false
            };

            foreach (var room in selectedRooms)
            {
                room.IsAvailable = false;
                room.Reservations.Add(reservation);
            }

            reservation.ConfirmationTimer = new Timer(CancelUnconfirmedReservation, reservation, TimeSpan.FromMinutes(2), Timeout.InfiniteTimeSpan);

            reservations.Add(reservation);

            Console.WriteLine($"Reservation successful. Reservation ID: {reservation.ReservationId}");
        }
        else
        {
            Console.WriteLine("No available rooms matching your criteria.");
        }
    }

    private void CancelUnconfirmedReservation(object state)
    {
        var reservation = (Reservation)state;
        reservation.Confirmed = false;

        var room = rooms.Find(r => r.RoomNumber == reservation.RoomNumber);
        room.IsAvailable = true;

        Console.WriteLine($"Reservation {reservation.ReservationId} canceled. Reason: Not confirmed within 2 minutes.");
    }

    public void CancelReservation()
    {
        Console.Write("Enter Reservation ID to cancel: ");
        int reservationId;
        while (!int.TryParse(Console.ReadLine(), out reservationId) || reservationId <= 0)
        {
            Console.WriteLine("Invalid choice. Please enter a positive integer.");
        }

        Console.Write("Enter Reason for Cancellation: ");
        string cancellationReason = Console.ReadLine();

        var reservation = reservations.Find(r => r.ReservationId == reservationId);

        if (reservation != null)
        {
            reservation.Confirmed = false;

            var room = rooms.Find(r => r.RoomNumber == reservation.RoomNumber);
            room.IsAvailable = true;

            Console.WriteLine($"Reservation {reservationId} canceled. Reason: {cancellationReason}");
        }
        else
        {
            Console.WriteLine($"Reservation {reservationId} not found.");
        }
    }
}
