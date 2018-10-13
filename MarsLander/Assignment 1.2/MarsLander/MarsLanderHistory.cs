using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsLander
{
    class MarsLanderHistory
    {
        RoundInfo[] rounds = new RoundInfo[10];
        private int numRounds = 0;
        public MarsLanderHistory Clone()
        {
            MarsLanderHistory copy = new MarsLanderHistory();

            copy.rounds = new RoundInfo[this.rounds.Length];
            copy.numRounds = this.numRounds;
            for (int i = 0; i < copy.numRounds; i++)
                copy.rounds[i] = this.rounds[i];
            return copy;
        }
        public int NumberOfRounds()
        {
            return numRounds;
        }
        public int GetHeight(int x)
        {
            return rounds[x].GetHeight();
        }
        public int GetSpeed(int x)
        {
            return rounds[x].GetSpeed();
        }
        public void AddRound(int height, int speed) //Without the resize code, the code will crash when it overflows
        {
            //rounds[numRounds] = new RoundInfo(height, speed); 
            //This line would crash because if numrounds is greater than array length, it will crash
            if (numRounds >= rounds.Length)
            {
                RoundInfo[] temp = new RoundInfo[rounds.Length + 10];
                for(int x = 0; x < rounds.Length; x++)
                {
                    temp[x] = rounds[x];
                }
                rounds = temp;
            }
            rounds[numRounds] = new RoundInfo(height, speed);
            numRounds++;
        }
    }
    class RoundInfo
    {
        private int height;
        private int speed;

        #region Accessors
        public int GetHeight()
        {
            return height;
        }
        public void SetHeight(int newValue)
        {
            height = newValue;
        }

        public int GetSpeed()
        {
            return speed;
        }
        public void SetSpeed(int newValue)
        {
            speed = newValue;
        }
        #endregion Accessors

        public RoundInfo(int h, int s)
        {
            height = h;
            speed = s;
        }
    }
}
