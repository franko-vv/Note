using Notebook.Data.Model;
using System;
using System.Collections.Generic;

namespace Notebook.Data.Extensions
{
    public static class ExtensionUser
    {
        public static IEnumerable<User> KievAdultPeople(this IEnumerable<User> users)
        {
            foreach (var item in users)
            {
                if (item.Birthdate <= DateTime.Now.AddYears(-18) && item.City == "Kiev")
                    yield return item;
            }
        }
    }
}
