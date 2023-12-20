// Reservation.cs
using System;
using System.Threading;

class Reservation
{
    public int ReservationId { get; set; }
    public int RoomNumber { get; set; }
    public string GuestName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public bool Confirmed { get; set; }
    public Timer ConfirmationTimer { get; set; }
}
