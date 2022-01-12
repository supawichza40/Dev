using System;
using System.Collections.Generic;

namespace DJBeautyAndThaiSpaBooking
{
    /// <summary>
    /// Employee class is design for working schedule especially for hourly based work, this is good with beauticiam, massage therapist and other servicing jobs.
    /// </summary>
    public class Employee
    {
        #region Employee Attributes
        public string Name { get; set; }
        public double RatePerHr { get; set; }
        public string[] DayWorking { get; set; }
        public TimeSpan numberOfHourWorked ;
        public List<Tuple<TimeSpan, TimeSpan>> Workingtime = new List<Tuple<TimeSpan, TimeSpan>>();
        #endregion
        #region Employee constructor
        //private constructor for builder class only
        public Employee(string name, string[] dayWorking ,double ratePerHr=22)
        {
            Name = name;
            RatePerHr = ratePerHr;
            DayWorking = dayWorking;
        }
        #endregion
        #region Employee non-static Methods
        public void AddingWorkingTime(string startTime, string numberOfHour)//Format 11.00 11 00 11:00=> 11:00 ensure given input is correct.
        {
            //var employeeStartTime = GetStartTimeInputFromUser();
            //var durationOfTreatment = GetDurationOfTreatment();
            var startTimeInTimeFormat = Time.ConvertStringHoursAndMinutesToTimeSpanFormat(startTime);
            var numberOfHoursInTimeFormat = Time.ConvertStringHoursAndMinutesToTimeSpanFormat(numberOfHour);
            numberOfHourWorked += numberOfHoursInTimeFormat;//Add number of hour to the staff totalHrWork.
            Workingtime.Add(new Tuple<TimeSpan,TimeSpan> (startTimeInTimeFormat, startTimeInTimeFormat+numberOfHoursInTimeFormat));
            Workingtime.Sort();
            

        }
        public void GetAvailableTime()
        {
            var staffWorkingTime = Workingtime;
            TimeSpan closingTime = new TimeSpan(21, 00, 00);
            TimeSpan openingTime = new TimeSpan(10, 00, 00);
            if (staffWorkingTime==null)
            {
                Console.WriteLine("Available Time: 10:00-21:00");

            }
            else
            {
                for (int i = Workingtime.Count; i >= 0; i--)
                {
                    if (Workingtime.Count == 0 &&i==0)
                    {
                        Console.WriteLine($"Name:{Name} Available Time:10AM - 9PM ");
                        break;
                    }
                    if (i == Workingtime.Count)
                    {
                        Console.WriteLine($"Available Time@:{Workingtime[Workingtime.Count - 1].Item2} with {closingTime - Workingtime[Workingtime.Count - 1].Item2}");
                    }
                    else if (i == 0)
                    {
                        Console.WriteLine($"Avaialbe Time@:{openingTime} with {Workingtime[0].Item1 - openingTime}");
                    }
                    else
                    {
                        Console.WriteLine($"Available Time@:{Workingtime[Workingtime.Count - 2].Item2} with {Workingtime[Workingtime.Count - 1].Item1 - Workingtime[Workingtime.Count - 2].Item2}");
                    }
                }
              
            }
        }
        #endregion
        #region Employee static method
        public static List<Employee> GetListOfEmployeeForToday(List<Employee> employeeList)
        {
            List<Employee> todayEmployeeList = new List<Employee>();

            //Loop through employee list and check if match today day of week then add to list.
            foreach (var employee in employeeList)
            {
                foreach (var day in employee.DayWorking)
                {
                    if (day.ToLower() == DateTime.Today.DayOfWeek.ToString().ToLower())
                    {
                        todayEmployeeList.Add(employee);
                    }
                }
            }
            return todayEmployeeList;
        }
        #endregion
        #region Employee Builder class
        //Create a builder class
        public class Builder
        {
            private string _name;
            private double _ratePerHr;
            private string[] _dayWorking;
            
            public Builder WithName(string name)
            {
                _name = name;
                return this;
            }
            public Builder WithRatePerHr(double ratePerHr)
            {
                _ratePerHr = ratePerHr;
                return this;
            }
            public Builder WithDayWorking(string[] dayWorking)
            {
                _dayWorking = dayWorking;
                return this;
            }
            public Employee Build()
            {
                return new Employee(_name, _dayWorking);
            }
        }
        #endregion
    }

    }

