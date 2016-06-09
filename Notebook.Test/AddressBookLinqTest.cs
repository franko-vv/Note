using Notebook.Data.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notebook.Test
{
    [TestFixture]
    public class AddressBookLinqTest
    {
        private AddressBook addressBook;

        [SetUp]
        public void CreateAddressBook()
        {
            addressBook = new AddressBook();
        }

        [Test]
        public void GetUsersWithGmail_When_HaveGmail()
        {            
            int countUser = 1;

            addressBook.AddUser(new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "First",
                LastName = "Last",
                Email = "somemail@gmail.com",
                City = "City"
            });

            int countAddress = addressBook.GetUsersWithGmail().Count();
            
            Assert.That(countAddress, Is.EqualTo(countUser));
        }

        [Test]
        public void GetUsersWithGmail_When_HaveNotGmail()
        {
            int countUser = 0;

            addressBook.AddUser(new User()
            {
                Address = "Address",
                Birthdate = DateTime.Now,
                FirstName = "First",
                LastName = "Last",
                Email = "somegmail@mail.com",
                City = "City"
            });
            int countAddress = addressBook.GetUsersWithGmail().Count();

            Assert.That(countAddress, Is.EqualTo(countUser));
        }

        [Test]
        public void GetUsersWithGmail_When_AddressBookIsEmpty()
        {
            Assert.That(addressBook.GetUsersWithGmail().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetNewGirls_When_TimeAddedLessThan10Days()
        {
            int countUser = 1;
            
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somegmail@mail.com", Gender = Gender.Female });
            addressBook.AddUser(new User() { FirstName = "First1", LastName = "Last1", Email = "somegmail@gmail.com", Gender = Gender.Female, TimeAdded = DateTime.Now.AddDays(-50) });
            addressBook.AddUser(new User() { FirstName = "First2", LastName = "Last2", Email = "somegil@gmail.com", Gender = Gender.Male, TimeAdded = DateTime.Now.AddDays(-50) });
            addressBook.AddUser(new User() { FirstName = "First3", LastName = "Last3", Email = "somegl@gmail.com", Gender = Gender.Male });

            int countNewGirls = addressBook.GetNewGirls().Count();
            
            Assert.That(countNewGirls, Is.EqualTo(countUser));
        }

        [Test]
        public void GetNewGirls_When_AddressBookIsEmpty()
        {
            Assert.That(addressBook.GetNewGirls().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetJanuaryBirthday_When_HaveJanBirthday()
        {
            int countUser = 2;

            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somegmail@mail.com", Birthdate = Convert.ToDateTime("01/01/2010")  });
            addressBook.AddUser(new User() { FirstName = "First1", LastName = "Last1", Email = "somegmail@gmail.com", Birthdate = Convert.ToDateTime("02/01/2010") });
            addressBook.AddUser(new User() { FirstName = "First2", LastName = "Last2", Email = "somegil@gmail.com", Birthdate = Convert.ToDateTime("02/02/2010") });
            addressBook.AddUser(new User() { FirstName = "First3", LastName = "Last3", Email = "somegl@gmail.com", Birthdate = Convert.ToDateTime("05/05/2015") });

            var usersJanBirthday = addressBook.GetJanuaryBirthday();

            Assert.That(usersJanBirthday.Count, Is.EqualTo(countUser));
            Assert.That(usersJanBirthday[0].LastName, Is.GreaterThan(usersJanBirthday[usersJanBirthday.Count-1].LastName));
        }

        [Test]
        public void GetJanuaryBirthday_When_AddressBookIsEmpty()
        {
            Assert.That(addressBook.GetJanuaryBirthday().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetGenderDictionary_When_HaveUsers()
        {
            int femaleCount = 3;
            int maleCount = 2;

            // Add undefined
            addressBook.AddUser(new User() { FirstName = "First5", LastName = "Last", Email = "mail@gmail.com" });
            // Add female
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somemail@mail.com", Gender = Gender.Female });
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somegmail@mail.com", Gender = Gender.Female });
            addressBook.AddUser(new User() { FirstName = "First1", LastName = "Last1", Email = "somegmail@gmail.com", Gender = Gender.Female, TimeAdded = DateTime.Now.AddDays(-50) });
            // Add male
            addressBook.AddUser(new User() { FirstName = "First2", LastName = "Last2", Email = "somegil@gmail.com", Gender = Gender.Male, TimeAdded = DateTime.Now.AddDays(-50) });
            addressBook.AddUser(new User() { FirstName = "First3", LastName = "Last3", Email = "somegl@gmail.com", Gender = Gender.Male });

            var dict = addressBook.GetGenderDictionary();

            Assert.That(dict.Count, Is.EqualTo(2));
            Assert.That(dict[Gender.Female].Count, Is.EqualTo(femaleCount));
            Assert.That(dict[Gender.Male].Count, Is.EqualTo(maleCount));
            Assert.Throws<KeyNotFoundException>(() => dict[Gender.Undefined].Count());
        }

        [Test]
        public void GetGenderDictionary_When_AddressBookIsEmpty()
        {
            Assert.That(addressBook.GetGenderDictionary().Count, Is.EqualTo(0));
        }

        [Test]
        public void Paging_When_HaveUsers_GetFrom2()
        {
            addressBook.AddUser(new User() { FirstName = "First5", LastName = "Last", Email = "mail@gmail.com" });
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somemail@mail.com", Gender = Gender.Female });
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somegmail@mail.com", Gender = Gender.Female });
            addressBook.AddUser(new User() { FirstName = "First1", LastName = "Last1", Email = "somegmail@gmail.com", Gender = Gender.Female, TimeAdded = DateTime.Now.AddDays(-50) });
            addressBook.AddUser(new User() { FirstName = "First2", LastName = "Last2", Email = "somegil@gmail.com", Gender = Gender.Male, TimeAdded = DateTime.Now.AddDays(-50) });
            addressBook.AddUser(new User() { FirstName = "First3", LastName = "Last3", Email = "somegl@gmail.com", Gender = Gender.Male });

            var users = addressBook.Paging(x=>x.FirstName.StartsWith("First"), 2, 10);

            Assert.That(users.Count, Is.EqualTo(4));
        }

        [Test]
        public void Paging_When_AddressBookIsEmpty()
        {
            Assert.That(addressBook.Paging(x => x.FirstName.StartsWith("First"), 0, 5), Is.Empty);
        }

        [Test]
        public void Paging_When_WrongParameters()
        {
            Assert.Throws<ArgumentException>(()=> addressBook.Paging(x => x.FirstName.StartsWith("First"), -1, -2));
        }

        [Test]
        public void GetCountBirthday_When_HaveUsers()
        {
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somegmail@mail.com", Birthdate = Convert.ToDateTime("01/01/2010"), City = "City" });
            addressBook.AddUser(new User() { FirstName = "First1", LastName = "Last1", Email = "somegmail@gmail.com", Birthdate = Convert.ToDateTime("02/02/2010"), City = "City" });
            addressBook.AddUser(new User() { FirstName = "First2", LastName = "Last2", Email = "somegil@gmail.com", Birthdate = Convert.ToDateTime("02/03/2010"), City = "City" });
            addressBook.AddUser(new User() { FirstName = "First3", LastName = "Last3", Email = "somegl@gmail.com", Birthdate = DateTime.Now, City = "City" });
            addressBook.AddUser(new User() { FirstName = "First4", LastName = "Last4", Email = "somel@gmail.com", Birthdate = Convert.ToDateTime("06/05/2015") });
            
            Assert.That(addressBook.GetCountBirthday("City"), Is.EqualTo(1));
        }

        [Test]
        public void GetCountBirthday_When_AddressBookIsEmpty()
        {
            Assert.That(addressBook.GetCountBirthday("city"), Is.EqualTo(0));
        }

        [Test]
        public void GetCountBirthday_When_WrongParameters()
        {
            addressBook.AddUser(new User() { FirstName = "First", LastName = "Last", Email = "somegmail@mail.com", Birthdate = Convert.ToDateTime("01/01/2010") });
            addressBook.AddUser(new User() { FirstName = "First1", LastName = "Last1", Email = "somegmail@gmail.com", Birthdate = Convert.ToDateTime("02/01/2010") });
            addressBook.AddUser(new User() { FirstName = "First2", LastName = "Last2", Email = "somegil@gmail.com", Birthdate = Convert.ToDateTime("02/02/2010") });
            addressBook.AddUser(new User() { FirstName = "First3", LastName = "Last3", Email = "somegl@gmail.com", Birthdate = Convert.ToDateTime("05/05/2015") });

            Assert.That(addressBook.GetCountBirthday("city"), Is.EqualTo(0));
        }
    }
}