using System;

namespace Hotel_Reservation_System
{
    public class Reservation
    {
        public  Reservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            if (checkOutDate.Date <= checkInDate.Date)
                throw new ArgumentException("Check-out date must be after check-in date.");
            if (room.IsUnderMaintenance)
                throw new InvalidOperationException($"Room {room.RoomNumber} is under maintenance.");

            ReservationId = reservationId;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;
            Room = room;
            Status = ReservationStatus.Pending;
        }

        public int ReservationId { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public Room Room { get; }
        public ReservationStatus Status { get; private set; }

        public int Nights => (CheckOutDate - CheckInDate).Days;
        public decimal TotalCost => Nights * Room.NightlyRate;

        public bool IsActive =>
            Status != ReservationStatus.Cancelled && Status != ReservationStatus.CheckedOut;

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException("Only a pending reservation can be confirmed.");
            Status = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Only a confirmed reservation can be checked in.");
            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Only a checked-in reservation can be checked out.");
            Status = ReservationStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Only a pending or confirmed reservation can be cancelled.");
            Status = ReservationStatus.Cancelled;
        }
    }
}