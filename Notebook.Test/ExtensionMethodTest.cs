using Notebook.Data.Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Data.Extensions;

namespace Notebook.Test
{
    [TestFixture]
    public class ExtensionMethodTest
    {
        [Test]
        public void GetUsers_When_FromKievAgeMore18()
        {
            int countUser = 1;

            AddressBook addressBook = new AddressBook();
            addressBook.AddUser(new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now.AddYears(-30),
                FirstName = "First",
                LastName = "Last",
                Email = "somemail@gmail.com",
                City = "City"
            });
            addressBook.AddUser(new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now.AddYears(-30),
                FirstName = "First1",
                LastName = "Last1",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });
            addressBook.AddUser(new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "First2",
                LastName = "Last2",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });

            int countUs = addressBook.KievExtension().Count();

            Assert.That(countUser, Is.EqualTo(countUs));
        }

        [Test]
        public void GetUsersFromKievAgeMore18_When_HaveMoreThanOne()
        {
            int countUser = 4;

            AddressBook addressBook = new AddressBook();
            addressBook.AddUser(new User()
            {
                Birthdate = DateTime.Now.AddYears(-30),
                FirstName = "First",
                LastName = "Last",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });
            addressBook.AddUser(new User()
            {
                Birthdate = DateTime.Now.AddYears(-30),
                FirstName = "First1",
                LastName = "Last1",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });
            addressBook.AddUser(new User()
            {
                Birthdate = DateTime.Now.AddYears(-18),
                FirstName = "First11",
                LastName = "Last11",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });
            addressBook.AddUser(new User()
            {
                Birthdate = DateTime.Now.AddYears(-28),
                FirstName = "First121",
                LastName = "Last121",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });
            addressBook.AddUser(new User()
            {
                Birthdate = DateTime.Now.AddYears(-17),
                FirstName = "First11",
                LastName = "Last11",
                Email = "somemail@gmail.com",
                City = "Kiev"
            });

            int countUs = addressBook.KievExtension().Count();

            Assert.That(countUser, Is.EqualTo(countUs));
        }

        [Test]
        public void GetUsersFromKievAgeMore18_When_AddressBookIsEmpty()
        {
            AddressBook addressBook = new AddressBook();

            int countUs = addressBook.KievExtension().Count();

            Assert.That(countUs, Is.EqualTo(0));
        }
    }
}
