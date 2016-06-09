using Notebook.Data.Model;
using System;
using System.Collections.Generic;

namespace Notebook.Data.Interface
{
    interface IAddressBook
    {
        void AddUser(User userToAdd);
        int GetCountBirthday(string city);
        Dictionary<Gender, List<User>> GetGenderDictionary();
        IList<User> GetJanuaryBirthday();
        IEnumerable<User> GetNewGirls();
        IEnumerable<User> GetUsersWithGmail();
        IEnumerable<User> Paging(Func<User, bool> predicate, int from, int takeCount);
        void RemoveUser(User userToDelete);
        IList<User> TodayBirthday();
    }
}