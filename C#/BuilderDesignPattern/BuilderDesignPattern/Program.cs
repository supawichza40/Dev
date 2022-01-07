using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderDesignPattern
{/// <summary>
/// Builder design pattern provide a step-by-step guide to creating a complex object.
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var supaBuilder = new Person.Builder().WithAge(10).WithFirstName("Supavich");//The reason we return builder, so that we can continue calling builder class function.

            //Advantage of builder class
            //1.Parameter can be provide one by one
            //2.No person will be created until Build function is called(we can change it whatever we like but the last attribute will get create when build called).
            supaBuilder.WithAge(20);
            var supaObj = supaBuilder.Build();
            supaObj.GetInfo();
            //3.Improve Readability
            var emilyObj = new Person.Builder().WithFirstName("Emily")
                                               .WithLastName("James")
                                               .WithAge(17)
                                               .Build();
            //Compared to without builder
            var emily = new PersonWithOutBuilder("Emily", "James", 17);//We do not know if emily or james is the first name, 
            //People reading your code should not have to guess what your code is doing.

            //Disadvantage of Builder class
            //1. More code to write;
            //2.At compile time, does not enforced to provide all parameter needed.(need to create our enforcement or default value).
            //3.Accident overwrite value before building.

        }
    }
    class PersonWithOutBuilder
    {
        private string FirstName;
        private string LastName;
        private int Age;
        public PersonWithOutBuilder(string firstName,string lastName,int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
    class Person
    {
        private string FirstName;
        private string LastName;
        private int Age;

        private Person(string firstName,string lastName,int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public void GetInfo()
        {
            Console.WriteLine($"First Name:{FirstName} Last Name:{LastName} Age:{Age}");
        }
        /// <summary>
        /// Builder design pattern provide a step-by-step guide to creating a complex object.
        /// </summary>
        public class Builder
        {
            private string _firstName;
            private string _lastName;
            private int _age;

            public Builder WithFirstName(string firstName)
            {
                _firstName = firstName;
                return this;
            }
            public Builder WithLastName(string lastName)
            {
                _lastName = lastName;
                return this;
            }
            public Builder WithAge(int age)
            {
                _age = age;
                return this;
            }
            public Person Build()
            {
                return new Person(_firstName, _lastName, _age);
            }
        }
    }
}
