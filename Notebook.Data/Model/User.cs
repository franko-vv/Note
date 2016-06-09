using System;

namespace Notebook.Data.Model
{
    public enum Gender
    {
        Undefined,
        Male,
        Female
    }

    public class User
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        private DateTime _birthdate; 
        public DateTime Birthdate
        {
            get { return _birthdate; }

            set
            {
                bool check = value > DateTime.Now;
                if (check)
                    _birthdate = DateTime.Now;
                else
                    _birthdate = value;
            }
        }

        public DateTime TimeAdded { get; set; } = DateTime.Now;
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; } 
        
        public override string ToString()
        {
            return String.Format($"User {FirstName} {LastName} Info: \ngender - {Gender}; \nbirthdate - {Birthdate:d}; \ncity - {City}; \naddress - {Address}; \nphone - {PhoneNumber}; \nE-mail - {Email}; \ntime added - {TimeAdded:d}.");
        }
    }
}
