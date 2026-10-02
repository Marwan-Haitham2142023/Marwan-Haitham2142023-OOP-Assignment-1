using System;
using System.Collections.Generic;

namespace Hotel_Reservation_System
{
    public class Program
    {
        public static void Main()
        {
            List<Guest> guests = new List<Guest>();
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room(101, RoomType.Single, 300m));
            rooms.Add(new Room(102, RoomType.Double, 500m));
            rooms.Add(new Room(201, RoomType.Suite, 1200m));

            guests.Add(new Guest(1, "Mona Ali", "0100000000"));
            guests.Add(new Guest(2, "Omar Hassan", "0111111111"));
            guests.Add(new Guest(3, "Sara Nabil", "0122222222"));

            Reservation r1 = guests[0].MakeReservation(1, new DateTime(2026, 10, 1), new DateTime(2026, 10, 4), rooms[1]);
            r1.Confirm();
            r1.CheckIn();

            Reservation r2 = guests[1].MakeReservation(2, new DateTime(2026, 10, 10), new DateTime(2026, 10, 12), rooms[0]);
            r2.Confirm();

            guests[2].MakeReservation(3, new DateTime(2026, 11, 1), new DateTime(2026, 11, 5), rooms[2]);


            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1) Add guest");
                Console.WriteLine("2) Add room");
                Console.WriteLine("3) Make reservation");
                Console.WriteLine("4) Change reservation status");
                Console.WriteLine("5) Show guest history");
                Console.WriteLine("6) Change room rate");
                Console.WriteLine("7) Room maintenance on/off");
                Console.WriteLine("0) Exit");
                Console.Write("Choice: ");

                try
                {
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            {
                                int id = ReadInt("Guest id: ");
                                if (FindGuest(guests, id) != null)
                                    throw new InvalidOperationException("Guest id already exists.");
                                Console.Write("Full name: ");
                                string name = Console.ReadLine();
                                Console.Write("Phone: ");
                                string phone = Console.ReadLine();
                                guests.Add(new Guest(id, name, phone));
                                Console.WriteLine("Guest added.");
                                break;
                            }

                        case "2":
                            {
                                int number = ReadInt("Room number: ");
                                if (FindRoom(rooms, number) != null)
                                    throw new InvalidOperationException("Room already exists.");
                                int type = ReadInt("Type (0=Single, 1=Double, 2=Suite): ");
                                if (type < 0 || type > 2)
                                    throw new ArgumentException("Type must be 0, 1 or 2.");
                                decimal rate = ReadDecimal("Nightly rate: ");
                                rooms.Add(new Room(number, (RoomType)type, rate));
                                Console.WriteLine("Room added.");
                                break;
                            }

                        case "3":
                            {
                                Guest guest = FindGuest(guests, ReadInt("Guest id: "));
                                Room room = FindRoom(rooms, ReadInt("Room number: "));
                                if (guest == null || room == null)
                                    throw new InvalidOperationException("Guest or room not found.");
                                int reservationId = ReadInt("Reservation id: ");
                                DateTime checkIn = ReadDate("Check-in (YYYY-MM-DD): ");
                                DateTime checkOut = ReadDate("Check-out (YYYY-MM-DD): ");
                                Reservation r = guest.MakeReservation(reservationId, checkIn, checkOut, room);
                                Console.WriteLine("Created. Total cost: " + r.TotalCost);
                                break;
                            }

                        case "4":
                            {
                                Guest guest = FindGuest(guests, ReadInt("Guest id: "));
                                if (guest == null)
                                    throw new InvalidOperationException("Guest not found.");
                                int reservationId = ReadInt("Reservation id: ");
                                Reservation r = null;
                                foreach (Reservation item in guest.Reservations)
                                {
                                    if (item.ReservationId == reservationId)
                                        r = item;
                                }
                                if (r == null)
                                    throw new InvalidOperationException("Reservation not found.");

                                Console.Write("1=Confirm 2=Check in 3=Check out 4=Cancel: ");
                                string action = Console.ReadLine();
                                switch (action)
                                {
                                    case "1": r.Confirm(); break;
                                    case "2": r.CheckIn(); break;
                                    case "3": r.CheckOut(); break;
                                    case "4": r.Cancel(); break;
                                    default: throw new ArgumentException("Unknown action.");
                                }
                                Console.WriteLine("Status: " + r.Status);
                                break;
                            }

                        case "5":
                            {
                                Guest guest = FindGuest(guests, ReadInt("Guest id: "));
                                if (guest == null)
                                    throw new InvalidOperationException("Guest not found.");
                                Console.WriteLine(guest.FullName + " has " + guest.Reservations.Count + " reservation(s):");
                                foreach (Reservation r in guest.Reservations)
                                {
                                    Console.WriteLine("#" + r.ReservationId + "  room " + r.Room.RoomNumber + "  "
                                        + r.CheckInDate.ToString("yyyy-MM-dd") + " to "
                                        + r.CheckOutDate.ToString("yyyy-MM-dd") + "  total "
                                        + r.TotalCost + "  " + r.Status);
                                }
                                break;
                            }

                        case "6":
                            {
                                Room room = FindRoom(rooms, ReadInt("Room number: "));
                                if (room == null)
                                    throw new InvalidOperationException("Room not found.");
                                room.SetNightlyRate(ReadDecimal("New rate: "));
                                Console.WriteLine("Rate is now " + room.NightlyRate);
                                break;
                            }

                        case "7":
                            {
                                Room room = FindRoom(rooms, ReadInt("Room number: "));
                                if (room == null)
                                    throw new InvalidOperationException("Room not found.");
                                Console.Write("1=Start maintenance 2=End maintenance: ");
                                string action = Console.ReadLine();
                                switch (action)
                                {
                                    case "1": room.StartMaintenance(); break;
                                    case "2": room.EndMaintenance(); break;
                                    default: throw new ArgumentException("Unknown action.");
                                }
                                Console.WriteLine("Under maintenance: " + room.IsUnderMaintenance);
                                break;
                            }

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Unknown choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        private static Guest FindGuest(List<Guest> guests, int id)
        {
            foreach (Guest g in guests)
            {
                if (g.GuestId == id)
                    return g;
            }
            return null;
        }

        private static Room FindRoom(List<Room> rooms, int number)
        {
            foreach (Room r in rooms)
            {
                if (r.RoomNumber == number)
                    return r;
            }
            return null;
        }

        private static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine());
        }

        private static decimal ReadDecimal(string prompt)
        {
            Console.Write(prompt);
            return decimal.Parse(Console.ReadLine());
        }

        private static DateTime ReadDate(string prompt)
        {
            Console.Write(prompt);
            return DateTime.Parse(Console.ReadLine());
        }
    }
}