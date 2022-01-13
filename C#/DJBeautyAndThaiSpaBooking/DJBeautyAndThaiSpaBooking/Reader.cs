using System;

namespace DJBeautyAndThaiSpaBooking

{
    public class Reader
    {
        static IReadFromUser reader;
        public static string GetStartTimeInputFromUser(Employee staff,IReadFromUser reader_in)
            //To call a method, pass in the interface and call method from there provide flexibility and decouple.
        {
            if(reader_in == null)
            {
                reader = new AskInputFromUser();
            }
            else
            {
                reader = reader_in;
            }
            return reader_in.ReadFromUser();
        }
    //For reading user input with specific question
    public static string GetInputFromUserWithQuestion(string question)
        {
            Console.WriteLine(question);
            string result = Console.ReadLine();
            return result;
        }

    }
}

