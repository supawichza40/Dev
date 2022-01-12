using System;
using System.Collections.Generic;

namespace DJBeautyAndThaiSpaBooking
{
    public class Reception
    {
        public List<Employee> todayStaffList;
        public DateTime todayDate = DateTime.Now;
        public string Name { get; set; }

        List<Employee> allStaffList ;
        public Reception(string receptionName,List<Employee> allStaffList_in=null)
        {

            if (allStaffList_in == null)
            {
                allStaffList = new List<Employee>
                {
                    new Employee.Builder().WithName("Joy").WithRatePerHr(22).WithDayWorking(new string[] { "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday" }).Build(),
                    new Employee("Na",new string[]{ "Sunday","Monday","Tuesday"},22),
                    new Employee("Nisa",new string[]{"Wednesday","Thursday","Friday","Saturday","Sunday"}),
                    new Employee("Wilai",new string[]{"Wednesday"})
                };
            }
            else
            {
                allStaffList = allStaffList_in;
            }
            todayStaffList = Employee.GetListOfEmployeeForToday(allStaffList);
        }
        //Take booking method
        public void TakeBooking()
        {
            //Get available time
            foreach (var staff in todayStaffList)
            {
                staff.GetAvailableTime();
            }
            //Ask for therapist name
            var therapistName = Reader.GetInputFromUserWithQuestion("What is the therapist you would like to book?");

            //Ask for time and duration of hour
            var startTime = Reader.GetInputFromUserWithQuestion("What is the start time would you like to do? format 13:00 = 1pm");
            var duration = Reader.GetInputFromUserWithQuestion("What is the duration of time would you like to do?2:00 = 2hrs");
            //Check for valid duration
            bool isValidTime = Validation.IsTheTimeValidGivenTherapist(todayStaffList,therapistName, startTime, duration);

            //Select available time
            if (isValidTime == false)
            {
                Console.WriteLine("Could not add time specify, please try again.");
            }


        }
        //Rearrange booking

        //Get list of staff working

        //Calculate Gross Profit Net Profit

        //Get staff available hours

        //Calculate today pay.


    }
    public class Validation
    {
        public static bool IsTheTimeValidGivenTherapist(List<Employee> todayEmployee,string staffName,string startTime,string duration)
        {
            //Given that the input is correct format
            foreach (var employee in todayEmployee)
            {
                if(employee.Name.ToLower() == staffName.ToLower())
                {
                    employee.AddingWorkingTime(startTime, duration);
                    return true;
                }

            }
            return false;
        }
    }

    }

