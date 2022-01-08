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
            programmerEmployee = newEmployee;
        }
        public string NameOfCompany { get; set; }
        public int YearFounded { get; set; }

        List<Employee> programmerEmployee;
        public void RecruitNewEmployee(string[] skills)
        {
            Employee newRecruit = new ProgrammerFactoryEmployee().CreateOrFindEmployee(skills);
            //Process from interview to questionair must pass before add to companylist.

            programmerEmployee.Add(newRecruit);
        }
        public void IntroductionOfCompany()
        {
            Console.WriteLine($"Company Name:{NameOfCompany} Year Founded:{YearFounded} Employee:");
            PrintListOfEmployee(programmerEmployee);
        }
        public void PrintListOfEmployee(List<Employee> lstEmployee)
        {
            
            foreach (var item in lstEmployee)
            {
                Console.WriteLine(item.Name + " " + String.Join(" ",item.Skills));
            }
        }
    }

    class ProgrammerFactoryEmployee 
    {
        public Employee CreateOrFindEmployee(string[] skills)
        {
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
