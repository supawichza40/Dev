using System;

namespace DJBeautyAndThaiSpaBooking
{
    public class Time
    {
        public static TimeSpan ConvertStringHoursAndMinutesToTimeSpanFormat(string time)
        {
            string[] listOfTimeStringtime = time.Split(' ', ':', '.');
            return new TimeSpan(Int32.Parse(listOfTimeStringtime[0]), Int32.Parse(listOfTimeStringtime[1]),0);
        }
        public string GetStartTimeInputFromUser(Employee staff)
        {
            try
            {
                Console.WriteLine($"What is the start time for{staff.Name}?Please input in 12:10 format");
                var startTime = Console.ReadLine();
                bool IsCorrectFormat = ValidateInputCorrectTimeSpanFormat(startTime);
                return startTime;

            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public static bool ValidateInputCorrectTimeSpanFormat(string startTime)
        {
            //Check when split == 2

            var startTimeList = startTime.Split('.',' ',':');
            //Check only value entered
            if (startTimeList.Length != 2||Int32.TryParse(startTimeList[0],out int result)||Int32.TryParse(startTimeList[1],out int result1))
            {
                return false;
            }
            return true;

        }
    }
}
