using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal abstract class User
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
    }

    internal class Staff : User
    {
        private Staff(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        { }

        private void RegisterUser() { }
        private void DeleteUser() { }
    }

    internal class Customer : User
    {
        private bool IsPaying { get; set; }
        private bool IsMember { get; set; }
        private DateTime SubStart { get; set; }
        private DateTime SubEnd { get; set; }

        private Customer(int id, string name, int ssn, string phone, string email, string loginName, string loginPass,
                        bool isPaying, bool isMember, DateTime subStart, DateTime subEnd)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        {
            IsPaying = isPaying;
            IsMember = isMember;
            SubStart = subStart;
            SubEnd = subEnd;
        }
    }

    internal class Admin : User
    {
        private Admin(int id, string name, int ssn, string phone, string email, string loginName, string loginPass)
            : base(id, name, ssn, phone, email, loginName, loginPass)
        { }

        private void ViewLog() { }
    }
}
