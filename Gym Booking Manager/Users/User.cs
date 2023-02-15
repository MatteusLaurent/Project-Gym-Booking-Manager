using Gym_Booking_Manager.Activities;
using Gym_Booking_Manager.Reservations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gym_Booking_Manager.Users
{
    public abstract class User
    {
        // NYI: Add system admin rights!
        //private const string adminUsername = "admin";
        //private const string adminPassword = "admin123";

        public static int nextUserID;
        public static List<User> users = new List<User>();

        public int id { get; set; } // TBD: USE GUID?
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? ssn { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? loginName { get; set; }
        public string? loginPass { get; set; }
        public User(int id, string firstName, string lastName, string ssn, string phone, string email, string loginName, string loginPass)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.ssn = ssn;
            this.phone = phone;
            this.email = email;
            this.loginName = loginName;
            this.loginPass = loginPass;
        }
        public User() { }
        public void UpdateInfo()
        {

        }
        public void UpdateLogin()
        {

        }
        public static int GetID()
        {
            int id = nextUserID;
            nextUserID++;
            return id;
        }
        public static int LogIn()
        {
            int id = -1;
            int tries = 3;
            string? loginName = string.Empty;

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
                User? user = users.Find(u => u.id == id);
                Console.Write(">> Enter password: ");
                string loginPass = PasswordInput();

                if (loginPass == user.loginPass)
                {
                    Console.WriteLine($"\n>> Welcome {user.firstName} {user.lastName}!");
                    Task.Delay(1000).Wait();
                    break;
                }
                else
                {
                    tries--;
                    Console.WriteLine("\n>> Incorrect password, " + tries + " tries left.");
                }
                if (tries == 0)
                {
                    Console.WriteLine("\n>> Maximum tries reached, contact staff for support.");
                    Task.Delay(1000).Wait();
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
            nextUserID = int.Parse(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] strings = lines[i].Split(";");
                if (strings[0] == "Admin") users.Add(new Admin(int.Parse(strings[1]), strings[2], strings[3], (strings[4]), strings[5], strings[6], strings[7], strings[8]));
                if (strings[0] == "Staff") users.Add(new Staff(int.Parse(strings[1]), strings[2], strings[3], (strings[4]), strings[5], strings[6], strings[7], strings[8]));
                if (strings[0] == "Customer") users.Add(new Customer(int.Parse(strings[1]), strings[2], strings[3], (strings[4]), strings[5], strings[6], strings[7], strings[8], DateTime.Parse(strings[9]), DateTime.Parse(strings[10]), bool.Parse(strings[11])));
            }
        }
        public static void SaveUsers()
        {
            using (StreamWriter writer = new StreamWriter("Users/Users.txt", false))
            {
                writer.WriteLine(nextUserID);
                foreach (User user in users)
                {
                    if (user is Admin)
                    {
                        writer.WriteLine($"Admin;{user.id};{user.firstName};{user.lastName};{user.ssn};{user.phone};{user.email};{user.loginName};{user.loginPass}");
                    }
                    if (user is Staff)
                    {
                        writer.WriteLine($"Staff;{user.id};{user.firstName};{user.lastName};{user.ssn};{user.phone};{user.email};{user.loginName};{user.loginPass}");
                    }
                    if (user is Customer)
                    {
                        Customer saveUser = (Customer)user;
                        writer.WriteLine($"Customer;{saveUser.id};{user.firstName};{saveUser.lastName};{saveUser.ssn};{saveUser.phone};{saveUser.email};{saveUser.loginName};{saveUser.loginPass};{saveUser.subStart};{saveUser.subEnd};{saveUser.isMember}");
                    }
                }
            }
        }
        public void RegisterUser()
        {
            User user = new Customer();
            Console.Clear();
            Console.WriteLine("<< NEW ACCOUNT REGISTRATION >>\n");

            if (this is Admin)
            {
                Console.WriteLine($">> Select account type: \n{"- [1]",-8}New admin account.\n{"- [2]",-8}New staff account.\n{"- [3]",-8}New customer account.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1) { user = new Admin(); }
                else if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2) { user = new Staff(); }
                else if (keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3) { user = new Customer(); }
                else
                {
                    Console.WriteLine($"Invalid key option: ({keyInfo.Key})");
                    return;
                }
            }
            user.id = GetID();
            Console.Write(">> Enter first name: ");
            user.firstName = Console.ReadLine();
            Console.Write(">> Enter last name: ");
            user.lastName = Console.ReadLine();
            Console.Write(">> Enter social security number: ");
            user.ssn = Console.ReadLine();
            Console.Write(">> Enter phone number: ");
            user.phone = Console.ReadLine();
            Console.Write(">> Enter email address: ");
            user.email = Console.ReadLine();
            Console.Write(">> Enter login name: ");
            user.loginName = Console.ReadLine();
            string loginPassA, loginPassB;
            do
            {
                Console.Write(">> Enter login password: ");
                loginPassA = PasswordInput();
                Console.Write("\n>> Confirm password: ");
                loginPassB = PasswordInput();
                Console.WriteLine();
            } while (loginPassA != loginPassB);
            user.loginPass = loginPassA;

            if (user is Staff)
            {
                Console.WriteLine(">> Add new staff as a personal trainer?");
                Console.WriteLine($"{"- [Y]",-8}Yes.\n{"- [N]",-8}No.");

                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                if (keyPressed.Key == ConsoleKey.Y)
                {
                    Reservable.NewPT((Staff)user);
                    Console.WriteLine("\n>> Added new staff as a personal trainer.");
                }
                else if (keyPressed.Key == ConsoleKey.N) Console.WriteLine("\n>> New staff not added as a personal trainer.");
                else Console.WriteLine($"\n>> Invalid key option [{keyPressed.Key}], new staff not added as a personal trainer.");
            }
            if (user is Customer)
            {
                Customer customer = (Customer)user;
                Console.WriteLine("\n>> New customer account registered, add a subscription plan?");
                Console.WriteLine($"{"- [Y]",-8}Yes.\n{"- [N]",-8}No.");

                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                if (keyPressed.Key == ConsoleKey.Y) customer.AddSubscription();
                else if (keyPressed.Key == ConsoleKey.N) Console.WriteLine("\n>> No subscription plan added.");
                else Console.WriteLine($"\n>> Invalid key option [{keyPressed.Key}], no subscription plan added.");
            }

            Console.WriteLine($"\n>> New account ({user.loginName}) successfully created!");
            Console.WriteLine($"{"- TYPE:",-14}{user.GetType().Name}");
            Console.WriteLine($"{"- FIRST NAME:",-14}{user.firstName}");
            Console.WriteLine($"{"- LAST NAME:",-14}{user.lastName}");
            Console.WriteLine($"{"- SSN:",-14}{user.ssn}");
            Console.WriteLine($"{"- PHONENR.:",-14}{user.phone}");
            Console.WriteLine($"{"- EMAIL:",-14}{user.email}\n");
            Console.WriteLine("\n>> Press any key to continue.");
            Console.ReadKey(true);

            users.Add(user);
            SaveUsers();
        }
        public void DeregisterUser()
        {
            int userID = -1;
            int userIndex = -1;
            Console.Clear();
            Console.WriteLine("<< ACCOUNT DEREGISTRATION >>\n");
            Console.WriteLine($">> Select an option: \n{"- [1]",-8}View users.\n{"- [2]",-8}Search user.\n{"- [ESC]",-8}Exit");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1)
            {
                Console.WriteLine();
                this.ViewUsers(false, false);
                Console.Write("\n>> Enter id of user to deregister: ");
                try 
                { 
                    userID = int.Parse(Console.ReadLine()); 
                }
                catch 
                { 
                    Console.WriteLine(">> Invalid format, account deregistration cancelled!");
                    Task.Delay(1500).Wait();
                    return;
                }
            }
            else if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
            {
                try
                {
                    userID = this.SearchUser(false, false);
                }
                catch
                {
                    Console.WriteLine(">> Search users unsuccessful!");
                    Task.Delay(1500).Wait();
                    return;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine(">> Account deregistration cancelled!");
                Task.Delay(1500).Wait();
                return;
            }
            else
            {
                Console.WriteLine($">> Invalid key: ({keyInfo.Key})");
                Task.Delay(1500).Wait();
                return;
            }

            for (int i = 0; i < users.Count; i++) if (users[i].id == userID) userIndex = i;

            if (userIndex > 0 && userIndex < users.Count)
            {
                Console.WriteLine($">> Deregister account ({users[userIndex].loginName})?");
                Console.WriteLine($"{"- [Y]",-8}Yes");
                Console.WriteLine($"{"- [N]",-8}No");
                Console.WriteLine($"{"- [ESC]",-8}Exit");
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine($">> User ({users[userIndex].loginName}) deregistration successful!");
                    users.RemoveAt(userIndex);
                }
                else if (keyInfo.Key == ConsoleKey.N || keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($">> User ({users[userIndex].loginName}) deregistration cancelled!");
                    Task.Delay(1500).Wait();
                }
                else
                {
                    Console.WriteLine($">> Invalid key: ({keyInfo.Key}), deregistration unsuccessful!");
                    Task.Delay(1500).Wait();
                }
                SaveUsers();
            }
            else
            {
                Console.WriteLine(">> Account deregistration unsuccesful!");
                Task.Delay(1500).Wait();
            }
        }
        public int SearchUser(bool header = true, bool footer = true)
        {
            List<User> searchedUsers = new List<User>();
            int returnID = -1;

            if (header)
            {
                Console.Clear();
                Console.WriteLine("<< SEARCH USER >>\n");
            }
            Console.Write(">> Enter first name, last name or both: ");
            string[] input = Console.ReadLine().ToLower().Trim().Split(" ");

            List<User> primarySearch = new List<User>();
            foreach (User user in users)
            {
                if (input[0] == user.firstName.ToLower() || input[0] == user.lastName.ToLower())
                {
                    primarySearch.Add(user);
                }
            }

            searchedUsers = primarySearch;
            List<User> secondarySearch = new List<User>();
            if (input.Length > 1)
            {
                foreach (User user in primarySearch)
                {
                    if (input[1] == user.firstName.ToLower() || input[1] == user.lastName.ToLower())
                    {
                        secondarySearch.Add(user);
                    }
                }
                searchedUsers = secondarySearch;
            }

            Console.WriteLine(">> Search result:\n");
            if (searchedUsers.Count > 0)
            {
                int[] selectUser = new int[searchedUsers.Count];
                for (int i = 0; i < searchedUsers.Count; i++)
                {
                    ViewUser(searchedUsers[i].id, false, footer);
                    selectUser[i] = searchedUsers[i].id;
                }
                try
                {
                    Console.Write("\n>> Enter searched user id: ");
                    returnID = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\n>> Invalid input!");
                    Environment.Exit(0);
                }
            }
            else
            {
                string searchInput = string.Empty;
                foreach (string s in input) searchInput += s + " ";
                Console.WriteLine($">> No results found for: {searchInput}");
            }
            return returnID;
        }
        public void ViewUsers(bool header = true, bool footer = true)
        {
            List<User> adminUsers = users.Where(u => u.GetType() == typeof(Admin)).ToList();
            List<User> staffUsers = users.Where(u => u.GetType() == typeof(Staff)).ToList();
            List<User> customerUsers = users.Where(u => u.GetType() == typeof(Customer)).ToList();
            adminUsers.Sort((x, y) => x.lastName.CompareTo(y.lastName));
            staffUsers.Sort((x, y) => x.lastName.CompareTo(y.lastName));
            customerUsers.Sort((x, y) => x.lastName.CompareTo(y.lastName));

            if (header)
            {
                Console.Clear();
                Console.WriteLine("<< VIEW USERS >>\n");
            }
            if (this is Admin)
            {
                string typeUser = "Admin";
                int x, y;
                (x, y) = Console.GetCursorPosition();

                List<User> allUsers = new();
                allUsers.AddRange(adminUsers);
                allUsers.AddRange(staffUsers);
                allUsers.AddRange(customerUsers);

                foreach (User u in allUsers)
                {
                    if (typeUser != u.GetType().Name || x >= 120)
                    {
                        x = 0;
                        y += 5;
                    }

                    Console.SetCursorPosition(x, y);
                    Console.Write($"{"- TYPE:",-15}{u.GetType().Name}");
                    Console.SetCursorPosition(x, y + 1);
                    Console.Write($"{"- ID:",-15}{u.id}");
                    Console.SetCursorPosition(x, y + 2);
                    Console.Write($"{"- FIRST NAME:",-15}{u.firstName}");
                    Console.SetCursorPosition(x, y + 3);
                    Console.Write($"{"- LAST NAME:",-15}{u.lastName}\n");

                    x += 30;
                    typeUser = u.GetType().Name;
                }
            }
            else
            {
                string typeUser = "Staff";
                int x, y;
                (x, y) = Console.GetCursorPosition();

                List<User> allUsers = new();
                allUsers.AddRange(staffUsers);
                allUsers.AddRange(customerUsers);
                foreach (User u in allUsers)
                {
                    if (typeUser != u.GetType().Name || x >= 120)
                    {
                        x = 0;
                        y += 5;
                    }

                    Console.SetCursorPosition(x, y);
                    Console.Write($"{"- TYPE:",-15}{u.GetType().Name}");
                    Console.SetCursorPosition(x, y + 1);
                    Console.Write($"{"- ID:",-15}{u.id}");
                    Console.SetCursorPosition(x, y + 2);
                    Console.Write($"{"- FIRST NAME:",-15}{u.firstName}");
                    Console.SetCursorPosition(x, y + 3);
                    Console.Write($"{"- LAST NAME:",-15}{u.lastName}\n");

                    x += 30;
                    typeUser = u.GetType().Name;
                }
            }
            if (footer)
            {
                Console.WriteLine("\n>> Press any key to continue, or [V] to view a user.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.V)
                {
                    try
                    {
                        Console.Write(">> Enter id of user: ");
                        int id = int.Parse(Console.ReadLine());
                        ViewUser(id, true);
                    }
                    catch
                    {
                        Console.WriteLine(">> Invalid input!");
                        Task.Delay(1000).Wait();
                    }
                }
            }
        }
        public void ViewUser(int id, bool header = true, bool footer = true)
        {
            if (header)
            {
                Console.Clear();
                Console.WriteLine("<< VIEW USER >>\n");
            }
            foreach (User user in users)
            {
                if (user.id == id)
                {
                    Console.WriteLine($"{"- TYPE:",-15}{user.GetType().Name}");
                    Console.WriteLine($"{"- ID:",-15}{user.id}");
                    Console.WriteLine($"{"- FIRST NAME:",-15}{user.firstName}");
                    Console.WriteLine($"{"- LAST NAME:",-15}{user.lastName}");
                    Console.WriteLine($"{"- SSN:",-15}{user.ssn}");
                    Console.WriteLine($"{"- EMAIL:",-15}{user.email}");
                    Console.WriteLine($"{"- PHONE:",-15}{user.phone}");
                    Console.WriteLine($"{"- LOGIN:",-15}{user.loginName}");

                    if (user is Customer)
                    {
                        Customer customer = (Customer)user;
                        Console.WriteLine($"{"- MEMBER:",-15}{customer.isMember}");
                        Console.WriteLine($"{"- SUB.START:",-15}{customer.subStart}");
                        Console.WriteLine($"{"- SUB.END:",-15}{customer.subStart}");
                    }
                }
            }
            if (footer)
            {
                Console.WriteLine("\n>> Press any key to continue.");
                Console.ReadKey(true);
            }
        }
        public abstract void Menu();
    }
    public class Admin : User
    {
        public Admin(int id, string firstname, string lastname, string ssn, string phone, string email, string loginName, string loginPass)
            : base(id, firstname, lastname, ssn, phone, email, loginName, loginPass) { }
        public Admin() : base() { }
        public void ListLog() { }
        public override void Menu()
        {
            bool cancel = false;
            while (!cancel)
            {
                Console.Clear();
                Console.WriteLine("<< ADMIN MENU >>\n");
                Console.WriteLine($">> LOGGED IN: {this.firstName} {this.lastName}");
                Console.WriteLine("\n>> Select an option!");
                Console.WriteLine($"{"- [1]",-8}Register user.\n{"- [2]",-8}Deregister user.\n{"- [3]",-8}View users.\n{"- [4]",-8}View log activities.\n{"- [ESC]",-8}Log out.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1)
                {
                    this.RegisterUser();
                }
                else if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
                {
                    this.DeregisterUser();
                }
                else if (keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3)
                {
                    this.ViewUsers();
                }
                else if (keyInfo.Key == ConsoleKey.D4 || keyInfo.Key == ConsoleKey.NumPad4)
                {
                    this.ListLog();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"\n>> LOGGED OUT: {this.lastName}");
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
    public class Staff : User
    {
        public Staff(int id, string firstname, string lastname, string ssn, string phone, string email, string loginName, string loginPass)
            : base(id, firstname, lastname, ssn, phone, email, loginName, loginPass) { }
        public Staff() : base() { }
        public override void Menu()
        {
            bool cancel = false;
            while (!cancel)
            {
                Console.Clear();
                Console.WriteLine("<< STAFF MENU >>\n");
                Console.WriteLine($">> LOGGED IN: {this.firstName} {this.lastName}");
                Console.WriteLine("\n>> Select an option!");
                Console.WriteLine($"{"- [1]",-8}Register a new customer.\n{"- [2]",-8}Deregister a current customer.\n{"- [3]",-8}List all customers.\n{"- [4]",-8}Reserve equipment or PT\n{"- [5]",-8}Create activity.\n{"- [ESC]",-8}Log out.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1)
                {
                    this.RegisterUser();
                }
                else if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
                {
                    this.DeregisterUser();
                }
                else if (keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3)
                {
                    this.ViewUsers();
                }
                else if (keyInfo.Key == ConsoleKey.D4 || keyInfo.Key == ConsoleKey.NumPad4)
                {
                    Reservation.NewReservationStaff(this.id);
                }
                else if (keyInfo.Key == ConsoleKey.D5 || keyInfo.Key == ConsoleKey.NumPad5)
                {
                    Activity.NewActivity(this.id);
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"\n>> LOGGED OUT: {this.lastName}");
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
    public class Customer : User
    {
        public DateTime subStart { get; set; }
        public DateTime subEnd { get; set; }
        public bool isMember { get; set; }
        public Customer(int id, string firstname, string lastname, string ssn, string phone, string email, string loginName, string loginPass,
                        DateTime subStart = default(DateTime), DateTime subEnd = default(DateTime), bool isMember = false)
            : base(id, firstname, lastname, ssn, phone, email, loginName, loginPass)
        {
            this.subStart = subStart;
            this.subEnd = subEnd;
            this.isMember = isMember;
        }
        public Customer() : base() { }
        public void AddSubscription()
        {
            Console.WriteLine($"\n>> Select membership type:\n{"- [1]",-8}One Day membership.\n{"- [2]",-8}One Month membership.\n{"- [3]",-8}One Year membership.\n{"- [ESC]",-8}Cancel, no membership.");
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);

            if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
            {
                if (this.subEnd < DateTime.Now) this.subStart = DateTime.Now.Date;
                this.subEnd = DateTime.Now.AddDays(1).Date;
                this.isMember = true;

                Console.WriteLine($"\n>> Successfully added a \"One-Day\" subscription plan to {this.lastName}.");
                Console.WriteLine($">> New subscription: {this.subStart.Date} - {this.subEnd.Date}");
            }
            if (keyPressed.Key == ConsoleKey.D2 || keyPressed.Key == ConsoleKey.NumPad2)
            {
                if (this.subEnd < DateTime.Now) this.subStart = DateTime.Now.Date;
                this.subEnd = DateTime.Now.AddMonths(1).Date;
                this.isMember = true;

                Console.WriteLine($"\n>> Successfully added a \"One-Month\" subscription plan to {this.lastName}.");
                Console.WriteLine($">> New subscription: {this.subStart.Date} - {this.subEnd.Date}");
            }
            if (keyPressed.Key == ConsoleKey.D3 || keyPressed.Key == ConsoleKey.NumPad3)
            {
                if (this.subEnd < DateTime.Now) this.subStart = DateTime.Now.Date;
                this.subEnd = DateTime.Now.AddYears(1).Date;
                this.isMember = true;

                Console.WriteLine($"\n>> Successfully added a \"One-Year\" subscription plan to {this.lastName}.");
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
        public override void Menu()
        {
            bool cancel = false;

            Console.Clear();
            Console.WriteLine("<< CUSTOMER MENU >>\n");
            Console.WriteLine($">> LOGGED IN: {this.firstName} {this.lastName}");
            while (!cancel)
            {
                Console.WriteLine("\n>> Select an option!");
                Console.WriteLine($"{"- [1]",-8}List available activities.\n{"- [2]",-8}Register for an activity.\n{"- [3]",-8}Deregister for an activity.\n{"- [4]",-8}View your registered activities.\n{"- [5]",-8}Book equipment or PT.\n{"- [6]",-8}{"- [ESC]",-8}Log out.");
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
                else if (keyInfo.Key == ConsoleKey.D5 || keyInfo.Key == ConsoleKey.NumPad5)
                {
                    if (this.isMember == true) Reservation.NewReservationUserMember(this.id);
                    else Reservation.NewReservationUserNonMember(this.id);
                }                
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"\n>> LOGGED OUT: {this.lastName}");
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