using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abdisamad__banker_
{
    class random
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
        public random(int procesSize, int resourceSize)
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
            var rnd = new Random();
           
            for (int i = 0; i < resourceSize; i++)
            {
                int r1 = rnd.Next(0, 13);
                if(r1 <= sumResources[i])
                {
                    oneD[i] = r1 + sumResources[i];
                }
                else
                {
                    oneD[i] = r1;
                }

            }
        }

        public void SetRandom(int procesSize, int resourceSize, int[,] twoD)
        {
            var rnd = new Random();

            for (int i = 0; i < procesSize; i++)
            {

                
                for (int j = 0; j < resourceSize; j++)
                {
                    twoD[i, j] = rnd.Next(0, 11);
                }
            }
        }
        public void SetRandomAlloc(int procesSize, int resourceSize, int[,] twoD)
        {
            var rnd = new Random();


            for (int i = 0; i < procesSize; i++)
            {

                for (int j = 0; j < resourceSize; j++)
                {
                    int r = rnd.Next(0, 9);
                    if (r <= max[i,j])
                    {
                        twoD[i, j] = r;
                    }
                    else
                    {
                        if (r - max[i, j] > max[i, j])
                            twoD[i, j] = r - r;
                        else
                            twoD[i, j] = r - max[i, j];
                    }
                    
                }
            }
        }

    }
}
