using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    public abstract class User
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private int Ssn { get; set; }
        private string Phone { get; set; }
        private string Email { get; set; }
        private string LoginName { get; set; }
        private string LoginPass { get; set; }

        protected User(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
        {
            Id = id;
            Name = name;
            Ssn = ssn;
            Phone = phone;
            Email = email;
            LoginName = loginName;
            LoginPass = loginPass;
        }
        public static void AddUsers(List<User> users)
        {
            string[] lines = File.ReadAllLines("Users.txt");
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
            : base(id, name, ssn, phone, email, loginName, loginPass)
        { }

        private void RegisterUser() { }
        private void DeleteUser() { }
    }

    public class Customer : User
    {
        private DateTime SubStart { get; set; }
        private DateTime SubEnd { get; set; }
        private bool IsMember;

        public Customer(int id, string name, int ssn, string phone, string email, string loginName, string loginPass,
                        DateTime subStart, DateTime subEnd, bool ismember)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        {
            SubStart = subStart;
            SubEnd = subEnd;
            IsMember= ismember;
        }
    }

    public class Admin : User
    {
        public Admin(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        { }

        private void ViewLog() { }
    }
}
