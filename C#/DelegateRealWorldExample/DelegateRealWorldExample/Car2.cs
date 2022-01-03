using System;

namespace DelegateRealWorldExample
{
    public class Car2
    {
        public string name { get; set; }
        public bool carIsDead { get; set; }
        public int currSpeed { get; set; }
        public int maxSpeed { get; set; }
        public Car2(string name,int maxSpeed,int currSpeed)
        {
            this.name = name;
            this.maxSpeed = maxSpeed;
            this.currSpeed = currSpeed;
        }
        //Define the delegate
        public delegate void AboutToBlow(string msg);
        public delegate void Exploded(string msg);
        //initialise the delegate
        private AboutToBlow almostDeadList;
        private Exploded explodedList;

        //Helper class, to assign method to delegate;
        public void AddAboutToBlowMethod(AboutToBlow meth)
        {
            //With+= allow us to add more than one methods and also initilise it.
            almostDeadList += meth;
            
        }
        public void AddExplodeMethod(Exploded meth)
        {
            explodedList = meth;
        }
        public void RemoveAboutToBlowMethod(AboutToBlow method)
        {
            almostDeadList -= method;
        }
        public void RemoveExplodeMethod(Exploded method)
        {
            explodedList -= method;

        }
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
