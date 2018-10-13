using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsLander
{
    class MarsLander
    {
        // positive speed == speed towards Mars (DOWNWARD)
        private MarsLanderHistory mlh = new MarsLanderHistory();
        private int height;
        private int speed;
        private int fuel;

        public MarsLander()
        {
            height = 1000;
            speed = 100;
            fuel = 500;
        }
        public int getHeight()
        {
            return height;
        }
        public int getSpeed()
        {
            return speed;
        }
        public int getFuel()
        {
            return fuel;
        }
        public void CalculateNewSpeed(int x)
        {
            fuel -= x;
            speed = (speed + 50 - x);
            height -= speed;
            if (height < 0)
                height = 0;
            mlh.AddRound(height, speed);
        }
        public MarsLanderHistory getHistory()
        {
            return mlh;
        }
    }
}
