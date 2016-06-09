using Notebook.Data.Model;
using Quartz;
using Quartz.Impl;

namespace Notebook.Data.Service
{
    public class ReminderSheduler
    {
        public static void Start(AddressBook adBook)
        {
            if (adBook == null) return;

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail sendCongratulationLetters = JobBuilder.Create<CongratulationBirthday>().Build();
            sendCongratulationLetters.JobDataMap.Put("users", adBook.TodayBirthday());

#if (!DEBUG)
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 0))
                  )
                .Build();
#else
            ITrigger trigger = TriggerBuilder.Create()
                                              .StartNow()
                                              .WithSimpleSchedule(x => x
                                                  .WithIntervalInSeconds(5)
                                                  .RepeatForever())
                                              .Build();
#endif

            scheduler.ScheduleJob(sendCongratulationLetters, trigger);
        }
    }
}
