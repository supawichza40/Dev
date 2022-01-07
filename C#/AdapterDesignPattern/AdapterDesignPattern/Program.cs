using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            HotelsByCityFinderAdapter hotelFinder = new HotelsByCityFinderAdapter(new HotelsByZipCodeFinder());
            var listOfHotel = hotelFinder.FindByCity("London");
            foreach (var hotel in listOfHotel)
            {
                Console.WriteLine(hotel.Name);
            }
        }
    }
    public class Hotel
    {
        public string Name { get; set; }
        public Hotel(string name)
        {
            Name = name;
        }
    }
    public interface IHotelsByCityFinder
    {
        IEnumerable<Hotel> FindByCity(string city);
    }
    //We need to implement our own CityFinder using other team zipcode finder;
    public class HotelsByCityFinderAdapter : IHotelsByCityFinder
        
    {
        private readonly IHotelsByZipCodeFinder _hotelsByZipCodeFinder;//Use interface instead of class allow dependency injection in future class implementing interface.
        public HotelsByCityFinderAdapter(IHotelsByZipCodeFinder hotelsByZipCodeFinder)
        {
            _hotelsByZipCodeFinder = hotelsByZipCodeFinder;
        }

        public IEnumerable<Hotel> FindByCity(string city)
        {
            var zipCodes = GetZipCodesFromCity(city);
            return zipCodes.SelectMany(zipcodes => _hotelsByZipCodeFinder.FindByZipCode(zipcodes));
        }

        private IEnumerable<string> GetZipCodesFromCity(string city)
        {
            if(city == "London")
            {
                return new[] { "E1 6AN", "E1 7AA" };
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }
    }
    //Another group provide us with class implementing interface using postcode instead of city.
    public interface IHotelsByZipCodeFinder
    {
        IEnumerable<Hotel> FindByZipCode(string zipcode);
    }
    public class HotelsByZipCodeFinder : IHotelsByZipCodeFinder
    {
        public IEnumerable<Hotel> FindByZipCode(string zipcode)
        {
            switch (zipcode)
            {
                case "E1 6AN":
                    return new[]
                    {
                        new Hotel("Imperial Hotel"),
                        new Hotel("Golden Duck Hotel")
                    };
                case "E1 7AA":
                    return new[]
                    {
                        new Hotel("Ambassador Hotel")
                    };

                default:
                    return Enumerable.Empty<Hotel>();
            }
        }
    }
}
