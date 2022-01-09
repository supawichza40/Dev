using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJBeautyAndThaiSpaBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a list of all employee
            Employee joy = new Employee.Builder().WithName("Joy").WithRatePerHr(22).WithDayWorking(new string[] { "Monday","Tuesday","Wednesday" }).Build();
            Employee na = new Employee("Na", 22, new string[] { "Sunday", "Monday", "Tuesday" });
            na.AddingWorkingTime("10:00", "1:00");
            na.AddingWorkingTime("13:00", "2:00");
            na.AddingWorkingTime("12:00", "1:00");

            List<Employee> employeeList = new List<Employee> { joy, na };

            List<Employee> todayEmployeeList = Employee.GetListOfEmployeeForToday(employeeList);

            //Console.WriteLine($"{DateTime.Today.DayOfWeek}");//Generate Day of week for today.

            //print list of working employee for today.
            //foreach (var employee in todayEmployeeList)
            //{
            //    Console.WriteLine(employee.Name);
            //}
        }


    }
}
