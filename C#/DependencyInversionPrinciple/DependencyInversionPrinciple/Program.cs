using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class SupavichAmazon
    {
        #region Bad version Not Following DIP
        BicycleDelivery deliveryServiceFromClass = new BicycleDelivery();//This is a case where dependency inversion principle was violated since the class depend on another class not abstraction.
        #endregion
        #region Good Following Dependency inversion principle
        IDelivery deliveryServiceFromInterface; //Any class implementing IDelivery, will be able to pass to this class contructor.
        //We should let our class depend on abstraction e.g.interface
        //Create an interface where all functionality that this class can call must be in there, but bare in mind interface segregation principle and liskov substitution principle.
        //By following the D, we make our class more flexible(different delivery class can be pass instead of one) and loosely couple.
        //After inteface has been create, it follow the contract, and if it is a public library, it is unlikely to change.
        //Code is easy to maintain and modify.
        #endregion
        #region Dependency Injection
        //Constructor Injection
        public SupavichAmazon(IDelivery externalDependency)//This is dependency injection, we are injecting external dependency to our class using contructor.
        {
            deliveryServiceFromInterface = externalDependency;
        }
        //Property Injection
        public IDelivery deliveryProperty{ get; set; }

        //Method Injection
        public void SetDeliveryDependency(IDelivery externalDependency)
        {
            deliveryServiceFromInterface = externalDependency;
        }
        #endregion
        public void DeliveryPackage()
        {
            deliveryServiceFromClass.DeriveryProduct("Washing Machine");
        }
        
    }
    interface IDelivery
    {
        void DeriveryProduct(string product);
    }
    class HeavyItemDelivery : IDelivery
    {
        public void DeriveryProduct(string product)
        {
            //Delivery all heavy good material
        }
    }
    class BicycleDelivery :IDelivery
        //IDelivery interface added for using dependency injection.
    {
        public void DeriveryProduct(string product)
        {
            //Delivery all product about bycicle.
        }
    }
}
