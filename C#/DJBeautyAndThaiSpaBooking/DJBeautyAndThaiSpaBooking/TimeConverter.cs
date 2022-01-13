using System;

namespace DJBeautyAndThaiSpaBooking
{
    public interface IReadFromUser
    {
        string ReadFromUser();
    }
    public class AskInputFromUser:IReadFromUser
    { 

        public string ReadFromUser()
        {
            var startTime = Console.ReadLine();
            return startTime;
        }
    }
    public class AskInputFromUserFake : IReadFromUser
    {
        public string ReadFromUser()
        {
            return "Expected";
        }
    }
    public class TimeConverter
    {
        public static TimeSpan ConvertStringHoursAndMinutesToTimeSpanFormat(string time)
        {
            string[] listOfTimeStringtime = time.Split(' ', ':', '.');
            return new TimeSpan(Int32.Parse(listOfTimeStringtime[0]), Int32.Parse(listOfTimeStringtime[1]),0);
        }
        public string GetStartTimeInputFromUser(Employee staff,IReadFromUser reader)
        {  
            if (reader==null)
            {
                reader = new AskInputFromUser();
            }
            bool isCorrectFormat = false;
            string startTime = "";
            while(!isCorrectFormat)
            {
                try
                {
                    Console.WriteLine($"What is the start time for{staff.Name}?Please input in 12:10 format");
                    startTime = reader.ReadFromUser();
                    isCorrectFormat = ValidateInputCorrectTimeSpanFormat(startTime);
                    

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return startTime;
          
            
        }
        public static bool ValidateInputCorrectTimeSpanFormat(string startTime)
        {
            //Check when split == 2

            var startTimeList = startTime.Split('.',' ',':');
            //Check only value entered
            if (startTimeList.Length != 2||!Int32.TryParse(startTimeList[0],out int result)||!Int32.TryParse(startTimeList[1],out int result1))
            {
                return false;
            }
            return true;

        }
    }
}
