using System;
using Notebook.Data.Model;
using Notebook.Logger;
using Notebook.Data.Service;
using System.Timers;
using System.Linq;
using System.Collections.Generic;

namespace Notebook
{
    class Program
    {
        static LoggerAbstract log = Log.ConsoleInstance;
        static AddressBook addressBook;
        const double interval24Hours = 24 * 60 * 60 * 1000;

        static void Main(string[] args)
        {
            log.Debug();

            addressBook = new AddressBook();
            addressBook.UserAdded += AddressBook_UserAdded;
            addressBook.UserRemoved += AddressBook_UserRemoved;

            InitAddressBook();
            DeferredExecutionExample();

            // Run service to send birthday congratulation letters
            ReminderSheduler.Start(addressBook);                        
            //SendEmailWithSystemTimer();

            Console.ReadKey();
        }

        /// <summary>
        /// Send birthday congratulation letters with System timer
        /// </summary>
        private static void SendEmailWithSystemTimer()
        {
            Timer checkForTime = new Timer();
            checkForTime.Elapsed += CheckForTime_Elapsed;
            checkForTime.Interval = 1000;
            checkForTime.Start();
        }

        /// <summary>
        /// Add list of users to address book
        /// </summary>
        private static void InitAddressBook()
        {
            User newUser = new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "First",
                LastName = "Last",
                Gender = Gender.Male,
                Email = "somemail@example.com",
                City = "City"
            };

            User newUser1 = new User()
            {
                Address = "Address",
                Birthdate = Convert.ToDateTime("31/05/2014"),
                FirstName = "Second",
                LastName = "LastName",
                Gender = Gender.Female,
                Email = "somemdaasdail@example.com",
                City = "City"
            };

            User newUser2 = new User()
            {
                Address = "Address",
                Birthdate = Convert.ToDateTime("30/05/2014"),
                FirstName = "Third",
                LastName = "LastName",
                Email = "somemdaasdail@example.com",
                City = "City"
            };
            addressBook.AddUser(newUser);
            addressBook.AddUser(newUser1);
            addressBook.AddUser(newUser2);
        }

        private static void DeferredExecutionExample()
        {
            User someUser = new User()
            {
                Address = "Address",
                Birthdate = Convert.ToDateTime("31/05/2014"),
                FirstName = "Some Name",
                LastName = "LastName",
                Email = "somemail@gmail.com",
                City = "City"
            };
            addressBook.AddUser(someUser);

            var usersWithGmail = addressBook.GetUsersWithGmail();

            User somenewUser = new User()
            {
                FirstName = "Deferred",
                LastName = "Execution",
                Email = "deferredExecution@gmail.com",
                City = "City"
            };
            addressBook.AddUser(somenewUser);

            foreach (var item in usersWithGmail)
            {
                Console.WriteLine("\n" + item.ToString() + "\n");
            }            
        }

        /// <summary>
        /// Send birthday congratulation letters when timer is elapsed
        /// </summary>
        private static void CheckForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            CongratulationBirthday cb = new CongratulationBirthday();
            var users = addressBook.TodayBirthday().ToList();

            foreach (var item in users)
            {
                cb.SendEmail(item);
            }    
        }

        private static void AddressBook_UserRemoved(object sender, EventArgs e)
        {
            log.Warning("User has been removed.");
        }

        private static void AddressBook_UserAdded(object sender, EventArgs e)
        {
            log.Info("User has been added.");
        }
    }
}
