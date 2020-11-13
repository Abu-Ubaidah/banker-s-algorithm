using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abdisamad__banker_
{
    class Program
    {
        static void Main(string[] args)
        {
            top:
            string fileNAme = "";
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("".PadLeft(7) + "Bnker's Algoritm for multiple Resource Allocation");
            Console.WriteLine("".PadLeft(7) + "=================================================");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.Write("".PadLeft(5) + "Enter Resource size: ");
            var resourceSize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" ");
            Console.Write("".PadLeft(5) + "Enter Process size: ");
            var procesSize = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Enter I to get input data from a file or press any key for random data: ");
            var cmd = Console.ReadLine();
            if(cmd != "I")
            {
                random obj = new random(procesSize,resourceSize);
                
                obj.SetRandom(procesSize, resourceSize, obj.max);
                Thread.Sleep(100);
                obj.SetRandomAlloc(procesSize, resourceSize, obj.allocation);
                int r = 0;
                for (int i = 0; i < resourceSize; i++)
                {
                    obj.sumResources[i] = Sum(procesSize, obj.allocation, r);
                    r++;
                }
             
                obj.SetRandomOneD(resourceSize, obj.resources);
                for (int i = 0; i < resourceSize; i++)
                {
                    obj.VectorAvailable[i] = obj.resources[i] - obj.sumResources[i];

                }
                for (int i = 0; i < procesSize; i++)
                {

                    for (int j = 0; j < resourceSize; j++)
                    {
                        obj.need[i, j] = obj.max[i, j] - obj.allocation[i, j];
                    }
                }

                Console.WriteLine(" ");
                Console.Write("Resources: ");
                for (int i = 0; i < resourceSize; i++)
                {
                    Console.Write("R{0}= {1} ", i, obj.resources[i]);
                    if (i != resourceSize - 1)
                        Console.Write(", ");
                }
                Console.WriteLine();
                Console.Write("Process: ");
                for (int i = 0; i < procesSize; i++)
                {
                    Console.Write("p{0}", i);
                    if (i != procesSize - 1)
                        Console.Write(", ");
                }
                Console.WriteLine();
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("".PadLeft(5) + "Max matrix claim: ");
                Console.WriteLine(" ");
                displayMatrix(procesSize, resourceSize, obj.max);

                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("".PadLeft(5) + "allocation matrix claim: ");
                Console.WriteLine(" ");
                displayMatrix(procesSize, resourceSize, obj.allocation);

                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("".PadLeft(5) + "Need matrix claim: ");
                Console.WriteLine(" ");
                displayMatrix(procesSize, resourceSize, obj.need);
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("".PadLeft(4) + "available Vector: ");
               
                Console.Write("".PadLeft(2));
                for (int i = 0; i < resourceSize; i++)
                {
                    Console.Write("".PadLeft(2) + "R{0} = {1} ", i, obj.VectorAvailable[i]);
                    if (i != resourceSize - 1)
                        Console.Write(",");
                }

                Console.WriteLine(" ");
                isSafe(procesSize, resourceSize, obj.VectorAvailable, obj.need, obj.safeSequence, obj.allocation, obj.max);
                Console.WriteLine(" ");


                // Keep the console window open in debug mode.
                Console.Write("Press Y to continue or any key to Exit: ");
                var cm = Console.ReadLine();
                if (cm != "y")
                {
                        System.Console.ReadKey();
                }

                else
                {
                     goto top;
                   
                }
                        
            }
            else
            {
          
                Console.WriteLine(" ");
                Console.Write(" Enter the file name (with .txt extension): ");
                fileNAme = Console.ReadLine();
                var path = Path.Combine(Directory.GetCurrentDirectory(), fileNAme);
                if (File.Exists(path))
                {
                    string[] lines = System.IO.File.ReadAllLines(path);
    
                
                    string[] temp =  new string[procesSize];
                    Array.Copy(lines, procesSize+1, temp, 0, procesSize);
                    int[,] max = new int[procesSize, resourceSize];
                    int[,] allocation = new int[procesSize, resourceSize];
                    int[,] need = new int[procesSize, resourceSize];
                    int[] resources = new int[resourceSize];
                    int[] VectorAvailable = new int[resourceSize];
                    int[] sumResources = new int[resourceSize];
                    string[] stringNumbers = new string[resourceSize];

                    int[] safeSequence = new int[procesSize];

                    allocate(procesSize, resourceSize,stringNumbers, lines, max);
                    allocate(procesSize, resourceSize,stringNumbers, temp, allocation);
                    stringNumbers = lines[2*procesSize+2].Split(' ');
                    for (int i = 0; i < resourceSize; i++)
                    {
                        resources[i] = Convert.ToInt32(stringNumbers[i]);
                    }
                    Console.WriteLine(" ");
                    Console.Write("Resources: ");
                    for (int i = 0; i < resourceSize; i++)
                    {
                        Console.Write("R{0}= {1} ", i, resources[i]);
                        if (i != resourceSize - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine();
                    Console.Write("Process: ");
                    for (int i = 0; i < procesSize; i++)
                    {
                        Console.Write("p{0}", i);
                        if (i != procesSize - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine();

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(5) + "Max matrix claim: ");
                    Console.WriteLine(" ");
                    displayMatrix(procesSize, resourceSize, max);

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(5) + "allocation matrix claim: ");
                    Console.WriteLine(" ");
                    displayMatrix(procesSize, resourceSize, allocation);

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(5) + "Need matrix claim: ");
                    Console.WriteLine(" ");
                    for (int i = 0; i < procesSize; i++)
                    {
               

                        for (int j = 0; j < resourceSize; j++)
                        {
                            need[i, j] = max[i,j] - allocation[i, j];
                        }
                    }
                    displayMatrix(procesSize, resourceSize, need);
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(4) + "available Vector: ");
                    int r = 0;
                    for (int i = 0; i < resourceSize; i++)
                    {
                        sumResources[i] = Sum(procesSize, allocation, r);
                        r++;
                    }
                    for (int i = 0; i < resourceSize; i++)
                    {
                        VectorAvailable[i] = resources[i] - sumResources[i];
               
                    }
                    Console.Write("".PadLeft(2));
                    for (int i = 0; i < resourceSize; i++)
                    {
                        Console.Write("".PadLeft(2) + "R{0} = {1} ", i, VectorAvailable[i]);
                        if (i != resourceSize - 1)
                            Console.Write(",");
                    }
           
                    Console.WriteLine(" ");
   

                    isSafe(procesSize, resourceSize, VectorAvailable, need, safeSequence, allocation,max);



            

                    Console.WriteLine();
                        // Keep the console window open in debug mode.
                    Console.Write("Press Y to continue or any key to Exit: ");
                    var cm = Console.ReadLine();
                    if (cm == "y")
                    {
                        goto top;
                    }
                        
                    else
                    {
                            System.Console.ReadKey();
                    }
                }
                else
                {
                    zero obj = new zero(procesSize, resourceSize);

                    obj.SetRandom(procesSize, resourceSize, obj.max);
                    Thread.Sleep(100);
                    obj.SetRandom(procesSize, resourceSize, obj.max);
                    int r = 0;
                    for (int i = 0; i < resourceSize; i++)
                    {
                        obj.sumResources[i] = Sum(procesSize, obj.allocation, r);
                        r++;
                    }

                    obj.SetRandomOneD(resourceSize, obj.resources);
                    for (int i = 0; i < resourceSize; i++)
                    {
                        obj.VectorAvailable[i] = obj.resources[i] - obj.sumResources[i];

                    }
                    for (int i = 0; i < procesSize; i++)
                    {

                        for (int j = 0; j < resourceSize; j++)
                        {
                            obj.need[i, j] = obj.max[i, j] - obj.allocation[i, j];
                        }
                    }

                    Console.WriteLine(" ");
                    Console.Write("Resources: ");
                    for (int i = 0; i < resourceSize; i++)
                    {
                        Console.Write("R{0}= {1} ", i, obj.resources[i]);
                        if (i != resourceSize - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine();
                    Console.Write("Process: ");
                    for (int i = 0; i < procesSize; i++)
                    {
                        Console.Write("p{0}", i);
                        if (i != procesSize - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine();
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(5) + "Max matrix claim: ");
                    Console.WriteLine(" ");
                    displayMatrix(procesSize, resourceSize, obj.max);

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(5) + "allocation matrix claim: ");
                    Console.WriteLine(" ");
                    displayMatrix(procesSize, resourceSize, obj.allocation);

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(5) + "Need matrix claim: ");
                    Console.WriteLine(" ");
                    displayMatrix(procesSize, resourceSize, obj.need);
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("".PadLeft(4) + "available Vector: ");

                    Console.Write("".PadLeft(2));
                    for (int i = 0; i < resourceSize; i++)
                    {
                        Console.Write("".PadLeft(2) + "R{0} = {1} ", i, obj.VectorAvailable[i]);
                        if (i != resourceSize - 1)
                            Console.Write(",");
                    }

                    Console.WriteLine(" ");
                    isSafe(procesSize, resourceSize, obj.VectorAvailable, obj.need, obj.safeSequence, obj.allocation, obj.max);
                    Console.WriteLine(" ");


                    // Keep the console window open in debug mode.
                    Console.Write("Press Y to continue or any key to Exit: ");
                    var cm = Console.ReadLine();
                    if (cm != "y")
                    {
                        System.Console.ReadKey();
                    }

                    else
                    {
                        goto top;

                    }
                }
            }

        }
        
        static int Sum(int procesSize, int[,] arrayname, int r)
        {
            int sum = 0;
            for (int j = 0; j < procesSize; j++)
            {
                sum+=  (arrayname[j, r]);
            }
            return sum;
        }
        static void allocate(int procesSize, int resourceSize , string[] stringNumbers, string[] lines, int[,] twoD )
        {
            for (int i = 0; i < procesSize; i++)
            {
                stringNumbers = lines[i].Split(' ');

                for (int j = 0; j < resourceSize; j++)
                {
                    twoD[i, j] = Convert.ToInt32(stringNumbers[j]);
                }
            }
        }
        static void displayMatrix(int procesSize, int resourceSize, int[,] arrayName)
        {
            Console.Write("".PadLeft(7));
            for (int i = 0; i < resourceSize; i++)
                Console.Write("".PadLeft(1)+"R{0}",i);
            Console.WriteLine();
            for (int i = 0; i < procesSize; i++)
            {
                Console.Write("".PadLeft(4)+"P{0}",i);
                for (int j = 0; j < resourceSize; j++)
                {
                    Console.Write("".PadLeft(2)+arrayName[i, j]);
                    
                }
                Console.WriteLine();
            }
        }
        static void isSafe(int procesSize, int resourceSize, int[] VectorAvailable, int[,] need, int[] safeSequence, int[,] allocation, int[,] max)
        {
            int count = 0;

            Boolean[] fnish = new Boolean[procesSize];
            for (int i = 0; i < procesSize; i++)
            {
                fnish[i] = false;
            }

            int[] work = new int[resourceSize];
            for (int i = 0; i < resourceSize; i++)
            {
                work[i] = VectorAvailable[i];
            }

            while (count < procesSize)
            {
                Boolean flag = false;
             

                for (int i = 0; i < procesSize; i++)
                {
                   
                    if (fnish[i] == false)
                    {
                        int j;
                        for (j = 0; j < resourceSize; j++)
                        {
                            if (need[i, j] > work[j])
                                break;
                        }
                        if (j == resourceSize)
                        {
                            safeSequence[count++] = i;
                            fnish[i] = true;
                            flag = true;
                            for (j = 0; j < resourceSize; j++)
                            {
                                work[j] = work[j] + allocation[i, j];
                            }
                            Console.WriteLine(" ");
                       
                            Console.WriteLine("iteration #{0}", count);
                            for (j = 0; j < resourceSize; j++)
                            {
                                max[i, j] = 0;
                            }


                            for (j = 0; j < resourceSize; j++)
                            {
                                allocation[i, j] = 0;
                            }


                            for (j = 0; j < resourceSize; j++)
                            {
                                need[i, j] = 0;
                            }
                            Console.WriteLine(" ");
                            Console.WriteLine("".PadLeft(5) + "Max matrix claim: ");
                            Console.WriteLine(" ");
                            displayMatrix(procesSize, resourceSize, max);
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                            Console.WriteLine("".PadLeft(5) + "Allocation matrix claim: ");
                            Console.WriteLine(" ");
                            displayMatrix(procesSize, resourceSize, allocation);
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                            Console.WriteLine("".PadLeft(5) + "Needed matrix claim: ");
                            Console.WriteLine(" ");
                            displayMatrix(procesSize, resourceSize, need);
                            Console.WriteLine(" ");
                           
                            Console.WriteLine("".PadLeft(4) + "available Vector: ");                         
                            Console.Write("".PadLeft(2));                         
                            for (j = 0; j < resourceSize; j++)
                            {
                                Console.Write("".PadLeft(2) + "R{0} = {1}",j,work[j]);
                                if (j != resourceSize - 1)
                                    Console.Write(",");
                            }
                            Console.WriteLine();

                        }
                    }
                    Console.WriteLine(" ");

                }
                if (flag == false)
                {
                    break;
                }
               
            }

            if (count < procesSize)
            {
                Console.WriteLine("Resource allocation is failed after {0} iterations!",count);
            }
            else
            {
                Console.WriteLine("".PadLeft(2)+"The esource allocation has been completed within {0} iterations!r: ",count);
                Console.WriteLine("".PadLeft(1)+ "The Safe - state orde: ");

                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.Write("  < ");
                for (int i = 0; i < procesSize; i++)
                {
                    Console.Write("P{0}", safeSequence[i]);
                    if (i != procesSize - 1)
                        Console.Write(", ");
                }
                Console.Write(" >");
            }

        }




    }
}
