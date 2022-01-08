using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Company DearCom = new Company("DearCom", 1997, new List<Employee> { new TechEmployee("Angela", 10, new string[] { "Annoying" }) });
            DearCom.IntroductionOfCompany();
            DearCom.RecruitNewEmployee(new string[] { "Python","Java"});
            DearCom.IntroductionOfCompany();
        }
    }
    class Company
    {
        public Company(string name,int yearFounded,List<Employee> newEmployee)
        {
            NameOfCompany = name;
            YearFounded = yearFounded;
            _employeeList = newEmployee;
        }
        public string NameOfCompany { get; set; }
        public int YearFounded { get; set; }

        List<Employee> _employeeList;
        public void RecruitNewEmployee(string[] skills)
        {
            var existingEmployee = new EmployeeDataBase().FindEmployeeFromSkills(skills);
            if (existingEmployee!=null)
            {
                _employeeList.Add(existingEmployee);
            }
            else
            {
                Employee newRecruit = EmployeeFactory.Create(skills);
                _employeeList.Add(newRecruit);
            }
            
        

        }
        public void IntroductionOfCompany()
        {
            Console.WriteLine($"Company Name:{NameOfCompany} Year Founded:{YearFounded} Employee:");
            PrintListOfEmployee(_employeeList);
        }
        public void PrintListOfEmployee(List<Employee> lstEmployee)
        {
            
            foreach (var item in lstEmployee)
            {
                Console.WriteLine(item.Name + " " + String.Join(" ",item.Skills));
            }
        }
    }

    class EmployeeFactory 
    {
        public static Employee Create(string[] skills)
        {
            //find the employee in the repository if return false
            //Imagine skills has been passed into database and return the most relevant employee result in the database.
            return new EmployeeDataBase().FindEmployeeFromSkills(skills);

        }
    }
    class EmployeeDataBase
    {
        public Employee FindEmployeeFromSkills(string[] skills)
        {
            //Do some database work and return employee
            return new TechEmployee("Supavich", 24, new string[] { "Java", "Python" });
        }
    }
    abstract class  Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string[] Skills { get; set; }
        public Employee(string name,int age,string[] skills)
        {
            Name = name;
            Age = age;
            Skills = skills;
        }
        public void Introduction()
        {
            Console.WriteLine($"Name:{Name} Age:{Age} Skills:{String.Join(" ",Skills)}");
        }
    }
    class TechEmployee : Employee
    {
        public TechEmployee(string name, int age, string[] skills) : base(name,age,skills)
        {

        }
    }
}
