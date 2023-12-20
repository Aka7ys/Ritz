// Room.cs
using System.Collections.Generic;

class Room
{
    public int RoomNumber { get; set; }
    public string RoomClass { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}
