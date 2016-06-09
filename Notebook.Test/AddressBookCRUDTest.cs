using Notebook.Data.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Test
{
    [TestFixture]
    public class AddressBookCRUDTest
    {
        private AddressBook addressBook;

        [SetUp]
        public void CreateAddressBook()
        {
            addressBook = new AddressBook();
        }

        [Test]
        public void AddUser_When_WrongFirstName_Then_GetException()
        {
            User newUser = new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "",
                LastName = "Last",
                Email = "somemail@example.com",
                City = "City"
            };

            Assert.Throws<ArgumentException>(() => addressBook.AddUser(newUser));
        }

        [Test]
        public void AddUser_When_UserExists_Then_GetException()
        {
            User newUser = new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "First Name",
                LastName = "Last",
                Email = "somemail@example.com",
                City = "City"
            };
            User newUser1 = new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "First Name",
                LastName = "Last",
                Email = "somemail@example.com",
                City = "City"
            };

            addressBook.AddUser(newUser);

            Assert.Throws<ArgumentException>(() => addressBook.AddUser(newUser1));
        }

        [Test]
        public void AddUser_When_UserNull_Then_GetException()
        {
            Assert.Throws<ArgumentNullException>(() => addressBook.AddUser(null));
        }

        [Test]
        public void RemoveUser_When_UserExists()
        {
            User newUser = new User()
            {
                Birthdate = DateTime.Now,
                FirstName = "First Name",
                LastName = "Last",
                Email = "somemail@example.com",
            };

            addressBook.AddUser(newUser);

            Assert.DoesNotThrow(() => addressBook.RemoveUser(newUser));
        }

        [Test]
        public void RemoveUser_When_UserNotExists_Then_GetException()
        {
            User newUser = new User()
            {
                Birthdate = DateTime.Now,
                FirstName = "First Name",
                LastName = "Last",
                Email = "somemail@example.com",
            };

            Assert.Throws<ArgumentNullException>(() => addressBook.RemoveUser(newUser));
        }

        [Test]
        public void RemoveUser_When_UserNull_Then_GetException()
        {
            Assert.Throws<ArgumentNullException>(() => addressBook.RemoveUser(null));
        }
    }
}
