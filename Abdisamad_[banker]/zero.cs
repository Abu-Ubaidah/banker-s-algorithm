using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abdisamad__banker_
{
    class zero
    {
        public int procesSize { get; set; }
        public int resourceSize { get; set; }

        public int[,] max;
        public int[,] allocation;
        public int[,] need; 
        public int[] resources; 
        public int[] VectorAvailable; 
        public int[] sumResources;
        public int[] safeSequence;
        public zero(int procesSize, int resourceSize)
        {
            max = new int[procesSize, resourceSize];
            allocation = new int[procesSize, resourceSize];
            need = new int[procesSize, resourceSize];
            resources = new int[resourceSize];
            VectorAvailable = new int[resourceSize];
            sumResources = new int[resourceSize];
            safeSequence = new int[procesSize];
        }


        public void SetRandomOneD(int resourceSize, int[] oneD)
        { 
            for (int i = 0; i < resourceSize; i++)
            {
           
                oneD[i] = 0;
            }
        }

        public void SetRandom(int procesSize, int resourceSize, int[,] twoD)
        {
            for (int i = 0; i < procesSize; i++)
            {    
                for (int j = 0; j < resourceSize; j++)
                {
                    twoD[i, j] = 0;
                }
            }
        }
   

    }
}
