using System;

namespace DelegateRealWorldExample
{
    public class Car
    {
        string name;
        bool carIsDead;
        int currSpeed;
        int maxSpeed;
        public Car(string name,int maxSpeed,int currSpeed)
        {
            this.name = name;
            this.maxSpeed = maxSpeed;
            this.currSpeed = currSpeed;
        }
        // Define the delegate types.
        public delegate void AboutToBlow(string msg);
        public delegate void Exploded(string msg);
        // Define member variables of each delegate type.
        private AboutToBlow almostDeadList;
        private Exploded explodedList;
        // Add members to the invocation lists using helper methods.
        public void OnAboutToBlow(AboutToBlow clientMethod)
        { almostDeadList = clientMethod; }
        public void OnExploded(Exploded clientMethod)
        { explodedList = clientMethod; }
        public void Accelerate(int delta)
        {
            // If the car is dead, fire Exploded event.
            if (carIsDead)
            {
                if (explodedList != null)
                    explodedList("Sorry, this car is dead...");
            }
            else
            { 
                currSpeed += delta;
                // Almost dead?
                if (10 == maxSpeed - currSpeed
                && almostDeadList != null)
                {
                    almostDeadList("Careful buddy! Gonna blow!");
                }
                // Still OK!
                if (currSpeed >= maxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("->CurrSpeed = {0}", currSpeed);
            }
        }

    }
}
