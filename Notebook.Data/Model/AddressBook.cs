using System;
using System.Linq;
using System.Collections.Generic;
using Notebook.Data.Interface;
using Notebook.Data.Extensions;

namespace Notebook.Data.Model
{
    public class AddressBook : IAddressBook
    {
        #region EVENT
        public delegate void AddressBookEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Event, when the user has been added
        /// </summary>
        public event AddressBookEventHandler UserAdded;
        /// <summary>
        /// Event, when the user has been removed
        /// </summary>
        public event AddressBookEventHandler UserRemoved;

        protected virtual void OnUserAddedEvent()
        {
            if (UserAdded != null)
                UserAdded(this, EventArgs.Empty);
        }

        protected virtual void OnUserRemovedEvent()
        {
            if (UserRemoved != null)
                UserRemoved(this, EventArgs.Empty);
        }
        #endregion

        private List<User> _userList;

        public AddressBook()
        {
            _userList = new List<User>();
        }

        public void AddUser(User userToAdd)
        {
            if (!ValidateUser(userToAdd))
                throw new ArgumentException("Can't add user.");

            // Check if user already exists
            bool existsUser = _userList.Exists(x => x.FirstName == userToAdd.FirstName && 
                                                    x.LastName == userToAdd.LastName &&
                                                    x.Birthdate == userToAdd.Birthdate && 
                                                    x.Email == userToAdd.Email);
            if (existsUser)
                throw new ArgumentException("User has already exist.");

            _userList.Add(userToAdd);

            OnUserAddedEvent();
        }

        public void RemoveUser(User userToDelete)
        {
            if (!ValidateUser(userToDelete))
                throw new ArgumentException("Can't delete user.");
            
            var user = _userList.FirstOrDefault(x =>    x.FirstName == userToDelete.FirstName ||
                                                        x.LastName == userToDelete.LastName ||
                                                        x.Birthdate == userToDelete.Birthdate || 
                                                        x.Email == userToDelete.Email);
            if (user == null)
                throw new ArgumentNullException("Can't delete user. User not found.");
            
            _userList.Remove(userToDelete);

            OnUserRemovedEvent();
        }

        public IEnumerable<User> GetUsersWithGmail()
        {
            var users = _userList.Where(x => x.Email.Contains("gmail.com"));
            return users;
        }

        public IEnumerable<User> GetNewGirls()
        {
            var users = from us in _userList
                       where us.Gender == Gender.Female &&
                             us.TimeAdded >= DateTime.Now.AddDays(-10)
                       select us;
            return users;
        }

        public IList<User> GetJanuaryBirthday()
        {
            var users = _userList.Where(x => x.Birthdate.Month == 1 && x.Address != "" && x.PhoneNumber != "")
                                 .OrderByDescending(x=>x.LastName).ToList();
            return users;
        }

        public Dictionary<Gender, List<User>> GetGenderDictionary()
        {
            Dictionary<Gender, List<User>> dictionaryGender = new Dictionary<Gender, List<User>>();

            dictionaryGender = _userList.GroupBy(x => x.Gender)
                                        .Where(x=>x.Key != Gender.Undefined)
                                        .ToDictionary(x => x.Key, 
                                                      x => x.Select(y => y).ToList());

            return dictionaryGender;
        }

        public IEnumerable<User> Paging(Func<User, bool> predicate, int from, int takeCount)
        {
            if (from < 0 || takeCount <= 0)
                throw new ArgumentException("Parameters can't be less then 0.");

            var users = _userList.Where(predicate).Skip(from).Take(takeCount);
            return users;
        }

        public int GetCountBirthday(string city)
        {
            if (city == null)
                throw new ArgumentNullException("Wrong city name.");

            int usersCount = (  from us in _userList
                                where us.Birthdate.Day == DateTime.Now.Day &&
                                        us.Birthdate.Month == DateTime.Now.Month &&
                                        us.City == city
                                select us).Count();

            return usersCount;
        }

        public IEnumerable<User> KievExtension()
        {
            var users = _userList.KievAdultPeople();
            return users;
        }

        public IList<User> TodayBirthday()
        {
            var users = _userList.Where(x => x.Birthdate.Day == DateTime.Now.Day && x.Birthdate.Month == DateTime.Now.Month).ToList();
            return users;
        }

        private bool ValidateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("User can't be null.");

            if (String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Email))
                return false;

            return true;
        }
    }
}
