using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace DJBeautyAndThaiSpaBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            Reception reception = new Reception("Supavich");
            reception.TakeBooking();
            Console.ReadKey();


        }


    }
}
