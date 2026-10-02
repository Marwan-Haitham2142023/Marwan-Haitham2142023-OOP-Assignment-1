using System;
using System.Collections.Generic;

namespace Hotel_Reservation_System
{
    public class Room
    {
        private readonly List<Reservation> reservations = new List<Reservation>();

        public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            SetNightlyRate(nightlyRate);
        }

        public int RoomNumber { get; }
        public RoomType RoomType { get; }
        public decimal NightlyRate { get; private set; }
        public bool IsUnderMaintenance { get; private set; }

        public void SetNightlyRate(decimal newRate)
        {
            if (newRate <= 0)
                throw new ArgumentException("Nightly rate must be positive.", nameof(newRate));
            NightlyRate = newRate;
        }

        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }

        public void AddReservation(Reservation reservation)
        {
            foreach (Reservation existing in reservations)
            {
                if (existing.IsActive &&
                    reservation.CheckInDate < existing.CheckOutDate &&
                    reservation.CheckOutDate > existing.CheckInDate)
                {
                    throw new InvalidOperationException(
                        $"Room {RoomNumber} is already booked for an overlapping period.");
                }
            }

            reservations.Add(reservation);
        }
    }
}