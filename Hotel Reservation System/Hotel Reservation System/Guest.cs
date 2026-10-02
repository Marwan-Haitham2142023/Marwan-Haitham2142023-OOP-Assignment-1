using System;
using System.Collections.Generic;

namespace Hotel_Reservation_System
{
    public class Guest
    {
        private readonly List<Reservation> reservations = new List<Reservation>();

        public Guest(int guestId, string fullName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be null or empty.", nameof(fullName));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));

            GuestId = guestId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public int GuestId { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        public IReadOnlyList<Reservation> Reservations => reservations.AsReadOnly();

        public Reservation MakeReservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
        {
            Reservation reservation = new Reservation(reservationId, checkInDate, checkOutDate, room);
            room.AddReservation(reservation);
            reservations.Add(reservation);
            return reservation;
        }
    }
}