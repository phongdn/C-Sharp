using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsLander
{
    class UserInterface
    {
        public void PrintGreeting()
        {
            Console.WriteLine("Welcome to the Mars Lander game!");
        }

        public void PrintLocation(MarsLander ml)
        {
            int limit = 1000;
            int height = ml.getHeight()/100;
            height = height * 100;
            if (height > limit)
            {
                limit = height;
            }
            for (int x = limit; x >= 0; x -= 100)
            {
                Console.Write("{0}m: ", x);
                if (x == height)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }                                     
        }

        public void PrintLanderInfo(MarsLander ml)
        {
            Console.WriteLine("Exact height: {0}",ml.getHeight());
            Console.WriteLine("Current (downward) speed: {0}", ml.getSpeed());
            Console.WriteLine("Fuel points left: {0}", ml.getFuel());
        }
        public int GetFuelToBurn(MarsLander ml)
        {
            string input;
            int num;
            while (true)
            {
                Console.WriteLine("How many points of fuel would you like to burn?");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out num) == true)
                {
                    if(num < 0)
                        Console.WriteLine("You can't burn less than 0 points of fuel!");
                    else if(num > ml.getFuel())
                        Console.WriteLine("You don't have {0} points of fuel!", num);
                    else
                    {
                        return num;
                    }
                }
                Console.WriteLine("You need to type a whole number of fuel points to burn!");
                Console.WriteLine("");
                Console.WriteLine("Just as a reminder, here's where the lander is: ");
                PrintLanderInfo(ml);
                Console.WriteLine("");
            }
        }
        public void PrintEndOfGameResult(MarsLander ml, int maxSpeed)
        {
            Console.WriteLine("The maximum speed for a safe landing is {1}; your lander's current speed is {0}", ml.getSpeed(), maxSpeed);
            if (ml.getSpeed() <= maxSpeed && ml.getSpeed() >= 0)
                Console.WriteLine("Congratulations!! You've successfully landed your Mars Lander, without crashing!!!");
            else
                Console.WriteLine("You have crashed the lander into the surface of Mars, killing everyone on board,costing NASA millions of dollars, and setting the space program back by decades!");
            Console.WriteLine("Here's the height/speed info for you: ");
            PrintHistory(ml.getHistory());
        }
        
        public void PrintHistory(MarsLanderHistory mlh)
        {
            Console.WriteLine("Round #\t\tHeight (in m)\t\tSpeed (downwards, in m/s)");
            for (int i = 0; i < mlh.NumberOfRounds(); i++)
            {
                Console.WriteLine("{0}\t\t{1}\t\t\t{2}", i, mlh.GetHeight(i), mlh.GetSpeed(i));
            }
        }
    }
}
