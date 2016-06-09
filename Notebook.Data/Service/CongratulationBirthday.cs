using System;
using System.Collections.Generic;
using Quartz;
using Notebook.Data.Model;
using Notebook.Data.Interface;

namespace Notebook.Data.Service
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class CongratulationBirthday : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var users = context.JobDetail.JobDataMap.Get("users") as List<User>;

            foreach (var item in users)
            {
                if (item != null && item.Email != "")
                    SendEmail(item);
            }
        }
        
        public void SendEmail(User user)
        {
            IEmail email = new EmailService();

            EmailLetter letter = new EmailLetter();
            letter.Subject = $"Happy birthday {user.FirstName} {user.LastName}!";
            letter.Message = "Best wishes for a joyous day filled with love and laughter.";
            letter.Receiver = user.Email;

            try
            {
                email.SendEmail(letter);
            }
            catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
}
    }
}
