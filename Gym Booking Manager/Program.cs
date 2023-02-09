using Gym_Booking_Manager;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Program
    {

        static void Main(string[] args)
        {

            int option = 0;
            do
            {
                Console.WriteLine("Option 1");
                Console.WriteLine("Option 2");
                Console.WriteLine("Option 3");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Option 1");
                        break;
                    case 2:
                        Console.WriteLine("Option 2");
                        break;
                    case 3:
                        Console.WriteLine("Option 3");
                        break;
                    case 4:
                        Console.WriteLine("Exiting the menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid option, no workie.");
                        break;
                }
                Console.WriteLine();
            } while (option != 4);
        }

        // Static methods for the program
    }
}