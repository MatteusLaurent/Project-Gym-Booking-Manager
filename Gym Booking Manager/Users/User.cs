namespace Gym_Booking_Manager.Users
{
    public abstract class User
    {
        public static List<User> users = new List<User>();

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

            users.Add(this);
        }
        public void UpdateInfo()
        {

        }
        public void UpdateLogin()
        {

        }
        public int GetNextID()
        {
            int maxID = 0;
            using (StreamReader reader = new StreamReader("Users/Users.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    int currentID = int.Parse(parts[1]);
                    if (currentID > maxID)
                    {
                        maxID = currentID;
                    }
                }
            }
            return maxID + 1;
        }
        public static int LogIn()
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
        public static void LoadUsers()
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
    public class Staff : User
    {
        public Staff(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass) { }
        public void RegisterUser() 
        {
            Console.WriteLine("Enter user name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter user SSN: ");
            int ssn = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter user phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter user email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter user login name: ");
            string loginName = Console.ReadLine();
            Console.WriteLine("Enter user login password: ");
            string loginPass = Console.ReadLine();
            int id = GetNextID();

            Console.WriteLine("Enter 1 for One Day membership, 2 for One Month membership, 3 for One Year membership:");
            int membershipChoice = int.Parse(Console.ReadLine());
            DateTime membershipDuration;
            switch (membershipChoice)
            {
                case 1:
                    membershipDuration = DateTime.Now.AddDays(1);
                    break;
                case 2:
                    membershipDuration = DateTime.Now.AddMonths(1);
                    break;
                case 3:
                    membershipDuration = DateTime.Now.AddYears(1);
                    break;
                default:
                    Console.WriteLine("Invalid membership choice. Defaulting to Daily pass membership.");
                    membershipDuration = DateTime.Now.AddDays(1);
                    break;
            }

            using (StreamWriter writer = new StreamWriter("Users/Users.txt", true))
            {
                writer.WriteLine($"Customer;{id};{name};{ssn};{phone};{email};{loginName};{loginPass};{DateTime.Now};{membershipDuration};True");
            }
        }
        public void UnregisterUser() { }
        public void ManageAccounts() { }
        public void ManageOptions()
        {
            bool go = true;
            while (go == true)
            {
                Console.WriteLine("Skriv 1 för Register user, 2 för Unregister User, 3 för Manage account, 4 för Avboka aktiviteter, 5 Registerara artiklar, 6 Exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        UnregisterUser();
                        break;
                    case "3":
                        ManageAccounts();
                        break;
                    case "4":
                        //
                        break;
                    case "5":
                        //
                        break;
                    case "6":
                        Console.WriteLine("Hej då!");
                        go = false;
                        break;
                }
            }
        }
    }

    public class Customer : User
    {
        public DateTime subStart { get; set; }
        public DateTime subEnd { get; set; }
        public bool isMember { get; set; }
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
