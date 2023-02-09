using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager.Users
{
    public abstract class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int ssn { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string loginName { get; set; }
        public string loginPass { get; set; }

        protected User(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
        {
            this.id = id;
            this.name = name;
            this.ssn = ssn;
            this.phone = phone;
            this.email = email;
            this.loginName = loginName;
            this.loginPass = loginPass;
        }
        public void UpdateInfo()
        {

        }
        public void UpdateLogin()
        {

        }
        public static int LogIn(List<User> users)
        {
            int result=-1;
            int tries = 5;
            while(result==-1)
            {
                Console.WriteLine("Skriv in username");
                string username = Console.ReadLine();
                foreach (User user in users)
                {
                    if (username == user.loginName) result = user.id; break;
                }
                if(result==-1) Console.WriteLine("Felaktigt username");
            }
            while (true)
            {
                Console.WriteLine("Skriv in password");
                string input = Console.ReadLine();
                if (input == users[result].loginPass)
                { 
                    Console.WriteLine("Välkommen " + users[result].name);
                    break;
                }
                Console.WriteLine("Felaktigt password "+tries+" försök kvar");                
                tries--;
                if (tries == 0)
                { 
                    Console.WriteLine("Maximalt antal försök nått!");
                    return -1;
                }
            }
            return result;
        }
        public static void LoadUsers(List<User> users)
        {
            string[] lines = File.ReadAllLines("Users/Users.txt");
            foreach (string line in lines)
            {
                string[] strings = line.Split(";");
                if (strings[0] == "Staff") users.Add(new Staff(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7]));
                if (strings[0] == "Admin") users.Add(new Admin(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7]));
                if (strings[0] == "Customer") users.Add(new Customer(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7], DateTime.Parse(strings[8]), DateTime.Parse(strings[9]), bool.Parse(strings[10])));
            }
        }
    }
    internal class Staff : User
    {
        public Staff(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass) { }
        private void RegisterUser() { }
        private void UnregisterUser() { }
    }

    public class Customer : User
    {
        public bool isMember { get; set; }
        public DateTime subStart { get; set; }
        public DateTime subEnd { get; set; }
        public Customer(int id, string name, int ssn, string phone, string email, string loginName, string loginPass,
                        DateTime subStart, DateTime subEnd, bool isMember)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        {
            this.subStart = subStart;
            this.subEnd = subEnd;
            this.isMember = isMember;
        }
        public void DaySubscription(DateTime addDay)
        {

        }
        public void MonthSubscription(DateTime addMonth)
        {

        }
        public void YearSubscription(DateTime addYear)
        {

        }
    }
    public class Admin : User
    {
        public Admin(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass) { }
    }
}
