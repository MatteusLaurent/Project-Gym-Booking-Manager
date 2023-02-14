using Gym_Booking_Manager.Reservations;
using System.Runtime.CompilerServices;

namespace Gym_Booking_Manager.Users
{
    public abstract class User
    {
        public static List<User> users = new List<User>();

        public int id { get; set; } // TBD: USE GUID?
        public string name { get; set; }
        public string ssn { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string loginName { get; set; }
        public string loginPass { get; set; }
        protected User(int id, string name, string ssn, string phone, string email, string loginName, string loginPass)
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
        public static int GetNextID()
        {
            int maxID = 0;
            using (StreamReader reader = new StreamReader("Users/Users.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int currentID = 0;
                    string[] parts = line.Split(';');
                    if (parts.Length > 1) currentID = int.Parse(parts[1]);
                    if (currentID > maxID)
                    {
                        maxID = currentID;
                    }
                }
            }
            return maxID + 1;
        }
        public static int LogIn() // NYI: ESCAPE PATH!
        {
            int id = -1;
            int tries = 5;
            string loginName = string.Empty;

            Console.Clear();
            Console.WriteLine("<< LOG-IN >>\n");
            while (id == -1)
            {
                Console.Write(">> Enter username: ");
                loginName = Console.ReadLine();

                foreach (User user in users)
                {
                    if (loginName == user.loginName)
                    {
                        id = user.id;
                    }
                }

                if (id == -1) Console.WriteLine(">> Username does not exist!");
                else Console.Clear();
            }

            Console.WriteLine("<< LOG-IN >>\n");
            Console.WriteLine($">> Username: {loginName}");
            while (true)
            {
                Console.Write(">> Enter password: ");
                string loginPass = PasswordInput();

                if (loginPass == users[id].loginPass)
                {
                    Console.WriteLine("\n>> Welcome " + users[id].name);
                    Task.Delay(1000).Wait();
                    break;
                }
                else
                {
                    Console.WriteLine("\n>> Incorrect password, " + tries + " tries left.");
                    tries--;
                }
                if (tries == 0)
                {
                    Console.WriteLine(">> Maximum tries reached, contact staff for support.");
                    return -1;
                }
            }
            return id;
        }
        public static string PasswordInput()
        {
            ConsoleKeyInfo keyInfo;
            string pass = string.Empty;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
                else if (keyInfo.Key == ConsoleKey.Escape) break;

            } while (keyInfo.Key != ConsoleKey.Enter);

            return pass;
        }
        public static void LoadUsers()
        {
            string[] lines = File.ReadAllLines("Users/Users.txt");
            foreach (string line in lines)
            {
                string[] strings = line.Split(";");
                if (strings[0] == "Staff") users.Add(new Staff(int.Parse(strings[1]), strings[2], (strings[3]), strings[4], strings[5], strings[6], strings[7]));
                if (strings[0] == "Admin") users.Add(new Admin(int.Parse(strings[1]), strings[2], (strings[3]), strings[4], strings[5], strings[6], strings[7]));
                if (strings[0] == "Customer") users.Add(new Customer(int.Parse(strings[1]), strings[2], (strings[3]), strings[4], strings[5], strings[6], strings[7], DateTime.Parse(strings[8]), DateTime.Parse(strings[9]), bool.Parse(strings[10])));
            }
        }
        public static void SaveUsers() // TBD: NEEDS TESTING!
        {
            using (StreamWriter writer = new StreamWriter("Users/Users.txt", false))
            {
                foreach (User user in users)
                {
                    if (user.GetType() == typeof(Staff))
                    {
                        writer.WriteLine($"Staff;{user.id};{user.name};{user.ssn};{user.phone};{user.email};{user.loginName};{user.loginPass}");
                    }
                    if (user.GetType() == typeof(Admin))
                    {
                        writer.WriteLine($"Admin;{user.id};{user.name};{user.ssn};{user.phone};{user.email};{user.loginName};{user.loginPass}");
                    }
                    if (user.GetType() == typeof(Customer))
                    {
                        Customer saveUser = (Customer) user;
                        writer.WriteLine($"Customer;{saveUser.id};{saveUser.name};{saveUser.ssn};{saveUser.phone};{saveUser.email};{saveUser.loginName};{saveUser.loginPass};{saveUser.subStart};{saveUser.subEnd};{saveUser.isMember}");
                    }
                }
            } 
        }
        public abstract void SaveUser();
        public abstract void Menu();
    }
    public class Staff : User
    {
        public Staff(int id, string name, string ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass) { }
        public static void RegisterUser()
        {
            Console.WriteLine("<< NEW ACCOUNT REGISTRATION >>\n");
            int id = GetNextID();
            Console.Write(">> Enter full name: ");
            string name = Console.ReadLine();
            Console.Write(">> Enter social security number: ");
            string ssn = Console.ReadLine();
            Console.Write(">> Enter phone number: ");
            string phone = Console.ReadLine();
            Console.Write(">> Enter email address: ");
            string email = Console.ReadLine();
            Console.Write(">> Enter login name: ");
            string loginName = Console.ReadLine();
            string loginPass1, loginPass2;
            do
            {
                Console.Write(">> Enter login password: ");
                loginPass1 = PasswordInput();
                Console.Write("\n>> Confirm password: ");
                loginPass2 = PasswordInput();
                Console.WriteLine();
            } while (loginPass1 != loginPass2);

            Customer customer = new Customer(id, name, ssn, phone, email, loginName, loginPass1);

            Console.WriteLine("\n>> New account registered, do you want to add a subscription plan?");
            Console.WriteLine($"{"- [Y]",-8}Yes.\n{"- [N]",-8}No.");

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);

            if (keyPressed.Key == ConsoleKey.Y) customer.AddSubscription();
            else if (keyPressed.Key == ConsoleKey.N) Console.WriteLine("\n>> No subscription plan added.");
            else Console.WriteLine($"\n>> Invalid key option [{keyPressed.Key}], no subscription plan added.");

            Console.WriteLine($"\n>> New account ({loginName}) successfully created!");
            Console.WriteLine($"{"- NAME:",-14}{name}");
            Console.WriteLine($"{"- SSN:",-14}{ssn}");
            Console.WriteLine($"{"- PHONENR.:",-14}{phone}");
            Console.WriteLine($"{"- EMAIL:",-14}{email}\n");

            users.Add(customer);
            customer.SaveUser();
        }
        public void UnregisterUser() { }
        public void ManageAccounts() { }
        public void CancelActivity() { }
        public override void SaveUser()
        {
            using (StreamWriter writer = new StreamWriter("Users/Users.txt", true))
            {
                writer.WriteLine($"Staff;{this.id};{this.name};{this.ssn};{this.phone};{this.email};{this.loginName};{this.loginPass}");
            }
        }
        public override void Menu()
        {
            bool go = true;
            while (go == true)
            {
                Console.WriteLine("Skriv 1 för Registrera användare, 2 för Avregisterera användare, 3 för Kontohantering, 4 för Avboka aktiviteter, 5 Registerara artiklar, 6 Avsluta");
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
                        CancelActivity();
                        break;
                    case "5":
                        Reservable.NewReservable();
                        break;
                    case "6":
                        Console.WriteLine("Hej då!");
                        go = false;
                        break;
                    default:
                        Console.WriteLine("Felaktigt val!");
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
        public Customer(int id, string name, string ssn, string phone, string email, string loginName, string loginPass,
                        DateTime subStart = default(DateTime), DateTime subEnd = default(DateTime), bool isMember = false)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        {
            this.subStart = subStart;
            this.subEnd = subEnd;
            this.isMember = isMember;
        }
        public void AddSubscription()
        {
            Console.WriteLine($"\n>> Select membership type:\n{"- [1]",-8}One Day membership.\n{"- [2]",-8}One Month membership.\n{"- [3]",-8}One Year membership.\n{"- [ESC]",-8}Cancel, no membership.");
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);

            if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
            {
                if (this.subEnd < DateTime.Now) this.subStart = DateTime.Now.Date;
                this.subEnd = DateTime.Now.AddDays(1).Date;
                this.isMember = true;

                Console.WriteLine($"\n>> Successfully added a \"One-Day\" subscription plan to {this.name}.");
                Console.WriteLine($">> New subscription: {this.subStart.Date} - {this.subEnd.Date}");
            }
            if (keyPressed.Key == ConsoleKey.D2 || keyPressed.Key == ConsoleKey.NumPad2)
            {
                if (this.subEnd < DateTime.Now) this.subStart = DateTime.Now.Date;
                this.subEnd = DateTime.Now.AddMonths(1).Date;
                this.isMember = true;

                Console.WriteLine($"\n>> Successfully added a \"One-Month\" subscription plan to {this.name}.");
                Console.WriteLine($">> New subscription: {this.subStart.Date} - {this.subEnd.Date}");
            }
            if (keyPressed.Key == ConsoleKey.D3 || keyPressed.Key == ConsoleKey.NumPad3)
            {
                if (this.subEnd < DateTime.Now) this.subStart = DateTime.Now.Date;
                this.subEnd = DateTime.Now.AddYears(1).Date;
                this.isMember = true;

                Console.WriteLine($"\n>> Successfully added a \"One-Year\" subscription plan to {this.name}.");
                Console.WriteLine($">> New subscription: {this.subStart.Date} - {this.subEnd.Date}");
            }
            if (keyPressed.Key == ConsoleKey.Escape) Console.WriteLine(">> New subscription cancelled!");
        }
        public void BookActivity()
        {

        }
        public void ListActivity()
        {

        }
        public void CancelActivity()
        {

        }
        public override void SaveUser()
        {
            using (StreamWriter writer = new StreamWriter("Users/Users.txt", true))
            {
                writer.WriteLine($"Customer;{this.id};{this.name};{this.ssn};{this.phone};{this.email};{this.loginName};{this.loginPass};{this.subStart.Date};{this.subEnd.Date};{this.isMember}");
            }
        }
        public override void Menu()
        {
            bool cancel = false;

            Console.Clear();
            Console.WriteLine("<< CUSTOMER MENU >>\n");
            Console.WriteLine($">> LOGGED IN: {this.name}");
            while (!cancel)
            {
                Console.WriteLine("\n>> Select an option!");
                Console.WriteLine($"{"- [1]",-8}List available activities.\n{"- [2]",-8}Register for an activity.\n{"- [3]",-8}Deregister for an activity.\n{"- [4]",-8}View your registered activities.\n{"- [ESC]",-8}Log out.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1)
                {
                    Calendars.Calendar.ViewCalendarMenu();
                }
                else if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
                {
                    BookActivity();
                }
                else if (keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3)
                {
                    CancelActivity();
                }
                else if (keyInfo.Key == ConsoleKey.D4 || keyInfo.Key == ConsoleKey.NumPad4)
                {
                    ListActivity();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"\n>> LOGGED OUT: {this.name}");
                    Task.Delay(1000).Wait();
                    cancel = true;
                }
                else
                {
                    Console.WriteLine($">> Invalid key option (KEY.{keyInfo.Key})");
                }
            }
        }
    }
    public class Admin : User
    {
        public Admin(int id, string name, string ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass) { }
        public void ListLog() { }
        public override void SaveUser()
        {
            using (StreamWriter writer = new StreamWriter("Users/Users.txt", true))
            {
                writer.WriteLine($"Admin;{this.id};{this.name};{this.ssn};{this.phone};{this.email};{this.loginName};{this.loginPass}");
            }
        }
        public override void Menu()
        {
            bool cancel = false;

            Console.Clear();
            Console.WriteLine("<< ADMIN MENU >>\n");
            Console.WriteLine($">> LOGGED IN: {this.name}");
            while (!cancel)
            {
                Console.WriteLine("\n>> Select an option!");
                Console.WriteLine($"{"- [1]",-8}Add new staff account.\n{"- [2]",-8}Remove existing staff account.\n{"- [3]",-8}View log activities.\n{"- [ESC]",-8}Log out.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1)
                {
                    // ADD STAFF METHOD HERE.
                }
                else if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
                {
                    // DELETE STAFF METHOD HERE.
                }
                else if (keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3)
                {
                    ListLog();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"\n>> LOGGED OUT: {this.name}");
                    Task.Delay(1000).Wait();
                    cancel = true;
                }
                else
                {
                    Console.WriteLine($">> Invalid key option (KEY.{keyInfo.Key})");
                }
            } 
        }
    }
}