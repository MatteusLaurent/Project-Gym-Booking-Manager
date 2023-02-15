using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Activities;
using Gym_Booking_Manager.Reservations;

namespace Gym_Booking_Manager
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // LOAD DATA METHODS RUNS BELOW:
            User.LoadUsers();
            Reservable.LoadReservables();
            Reservation.LoadReservations();
            Activity.LoadActivities();

            // MAIN SOFTWARE LOOP RUNS BELOW:
            RunGBM();
        }
        static void RunGBM()
        {
            bool shutdown = false;
            int userID;
            User? currentUser;

            do
            {
                Console.Clear();
                Console.WriteLine("<< GYM BOOKING MANAGER >>\n");
                Console.WriteLine(">> Press any key to log in, or escape to quit!");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine(">> Thank you for using \"Gym Booking Manager\"!");
                    Task.Delay(1000).Wait();
                    shutdown = true;
                }
                else
                {
                    userID = User.LogIn();
                    if (userID != -1)
                    {
                        currentUser = User.users.Find(u => u.id == userID);
                        currentUser.Menu();
                    }
                    else Console.WriteLine(">> Login failed!");
                    
                }
            } while (!shutdown);
        }
    }
}