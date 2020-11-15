using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lab01
{
    class Program

       
    {
        private string path = "C:\\Users\\Wojtek\\Desktop\\przemysl\\PL.csv";
        static void Main(string[] args)
        {

           
         //   var reader = new StreamReader(File.OpenRead("C:\\Users\\Wojtek\\Desktop\\przemysl\\PL.csv"));

           

            
            // table of distances 
            

            
            







            Program program = new Program();
            StreamReader reader  = program.OpenCsvFile(program.path);


            // Variable for quantity of cities, first part parse to int next reading one line and split by sign ";" 
            int quantityOfCities = int.Parse(reader.ReadLine().Split(";")[0]);

            // Displaying quantity of cities
            //Console.WriteLine("Ilość miast: " + quantityOfCities);
            //Variable to read unit (km or miles)
            var unit = reader.ReadLine().Split(";")[0];
            //Console.WriteLine("Jednostka odległości: " + unit);

            string[,] table = new string[quantityOfCities,quantityOfCities];
            table = program.WriteTable(quantityOfCities, reader);
            //program.DisplayTable( table, quantityOfCities);
            List<string> zipCode = new List<string>();
            List<string> cityName = new List<string>();
            List<double> latitude = new List<double>();
            List<double> longitude = new List<double>();
            List<double> distances = new List<double>();

            for (int i = 0; i<quantityOfCities; i++)
            {
                var line = reader.ReadLine().Split(";");
                zipCode.Add(line[0]);
                cityName.Add(line[1]);
                latitude.Add(Convert.ToDouble( line[2]));
                longitude.Add(Convert.ToDouble(line[3]));
            }

                List<string> permutation = program.ReadPermutation();
            List<int> finalPermutation = new List<int>();
               // distances  = program.CountTotalDistance(program.VRP(table, permutation,1), table);

            finalPermutation = program.VRP(table, permutation, 1);
            Console.Write("Kolejność przejazdu: ");
            program.DisplayPermutationint(finalPermutation);
            distances = program.CountTotalDistance(finalPermutation, table);


            Console.WriteLine("");
            for (int i = 0; i< distances.Count; i++)
            {
                if (i != distances.Count - 1)
                {
                    Console.WriteLine("Dlugość trasy nr. " + (i + 1) + " to: " + distances[i]);
                }
                else
                {
                    Console.WriteLine("Dlugość trasy całkowita to: " + distances[i]);
                }
                

            }






           
        }

        private StreamReader OpenCsvFile (string path)
        {
            // Reading a file in path
            var reader = new StreamReader(path);

            return reader;
        }
       // private int GetQuantityOfCities ()

        private string [,] WriteTable(int quantityOfCities, StreamReader reader)
        {
            string[,] tableOfDistance = new string[quantityOfCities, quantityOfCities];


            //Writing distances in table
            for (int i = 0; i < quantityOfCities; i++)
            {
                var line = reader.ReadLine();
                var values = line.Split(";");

                for (int j = 0; j < quantityOfCities; j++)
                {
                    tableOfDistance[i, j] = values[j];
                }

            }

            return tableOfDistance;
        }
        private void DisplayTable (string [,] table, int quantityOfCities)
        {

            ///* Displaying of table
           
            for (int i = 0; i < quantityOfCities; i++)
            {
                string row = null;
                for (int j = 0; j < quantityOfCities; j++)
                {
                    row += table[i, j] + " ";
                }
                Console.WriteLine(row);
            } 
            
           // */

        }
        private List <string> ReadPermutation()
        {
          //  Console.WriteLine("Wpisz numer huba");

            // Reading permutation
            Console.WriteLine("Wpisz permutację: ");

            List<string> permutation = new List<string>();

            string city = null;
            do
            {

                city = Console.ReadLine();
                if (city.Equals("ALL"))
                {
                    for (int i =1; i<25; i++)
                    {
                        permutation.Add(i.ToString());
                       // DisplayPermutation(permutation);
                    }
                    DisplayPermutation(permutation);
                    return permutation;
                }
                else
                {
                    // Console.WriteLine(city);
                    if (city.Length != 0)
                    {
                        int value = int.Parse(city);
                        if (value <= 25)
                            permutation.Add(city);
                        else
                        {
                            Console.WriteLine("Podałeś miasto nieprawidłowy numer miasta");
                        }
                    }
                }

               
            } while (city.Length != 0);

            DisplayPermutation(permutation);
            return permutation;
        }
        private void DisplayPermutation (List <string> permutation)
        {
            for (int i = 0; i < permutation.Count; i++)
            {
                Console.Write(permutation[i] + " ");

            }
        }


        private void DisplayPermutationint(List<int> permutation)
        {
            for (int i = 0; i < permutation.Count; i++)
            {
                Console.Write(permutation[i] + " ");

            }
        }
        private double FindMaxDistance (string[,] table, List<string> permutation, int startArg)
        {
            int indexToReturn=0;
            double maxDistance = 0;
            double tempDistance = 0;
            //DisplayPermutation(permutation);
            //Console.WriteLine("\n");
            for (int i=0; i<permutation.Count; i++)
            {
                
                int indexY = int.Parse(permutation[i]);
                int indexX = startArg;
                if (indexY >=0)
                    tempDistance = double.Parse(table[indexX, indexY]);
              
               if(tempDistance > 0)
                {
                    if (maxDistance < tempDistance)
                    {
                        maxDistance = tempDistance;
                        indexToReturn = i;
                    }
                }
                
                
            }

            return maxDistance;
        }

        private int FindMaxIndex(string[,] table, List<string> permutation, int argStart)
        {
            int indexToReturn = 0;
            double maxDistance = 0;
            double tempDistance = 0;
            //DisplayPermutation(permutation);
            //Console.WriteLine("\n");
            for (int i = 0; i < permutation.Count; i++)
            {

                int indexY = int.Parse(permutation[i])-1 ;
                int indexX = argStart;
                if (indexY >= 0)
                    tempDistance = double.Parse(table[indexX, indexY]);

                if (tempDistance > 0)
                {
                    if (maxDistance < tempDistance)
                    {
                        maxDistance = tempDistance;
                        
                        indexToReturn = i;
                    }
                }


            }
           // Console.WriteLine(maxDistance);
            return indexToReturn;
        }


        private int FindMinIndex (string[,] table, List<string> permutation, int argStart)
        {
            int indexToReturn = 0;
            double maxDistance = 999999;
            double tempDistance = 0;
            //DisplayPermutation(permutation);
            //Console.WriteLine("\n");
            for (int i = 0; i < permutation.Count; i++)
            {

                int indexY = int.Parse(permutation[i])-1;
                int indexX = int.Parse(permutation[i]);
                if (indexY >= 0)
                    tempDistance = double.Parse(table[indexX, indexY]);

                if (tempDistance > 0)
               {
                    if (maxDistance > tempDistance)
                    {
                        maxDistance = tempDistance;
                        indexToReturn = i;
                    }
                }


            }
            //Console.WriteLine(maxDistance);
            return indexToReturn;



           // return 0;
        }

        private List<int> VRP (string[,] table, List<string> permutation, int truckCount)
        {

           
            int Hub = int.Parse( permutation[0]);
            double OrderCount =
                (permutation.Count+1) / truckCount;
            //Console.WriteLine("ORDER COUNT:" + OrderCount);
         

            List<string> tempPermutation = new List<string>();
            tempPermutation = permutation;
            List<int> Pi = new List<int>();

         
            Console.WriteLine("\n");
            Pi.Clear();
           
            int k = 1;
            tempPermutation.RemoveAt(0);

            while (tempPermutation.Count != 0) 
            {
                Pi.Add(Hub);
               
                int j = FindMinIndex(table, tempPermutation, Hub - 1);
                Pi.Add(int.Parse(tempPermutation[j]));
                tempPermutation.RemoveAt(FindMinIndex(table, tempPermutation, Hub - 1));
                int x_k = 1;
                int l = j;

                while (x_k < OrderCount && tempPermutation.Count != 0)
                {
                    //Console.WriteLine("Jestem tu");
                    // Console.WriteLine("Jestem w:" + l);
                    l = FindMinIndex(table, tempPermutation, l);
                   // Console.WriteLine("Jade do:" + int.Parse(tempPermutation[l]));

                    if (x_k + 1 < OrderCount)
                    {
                        Pi.Add(int.Parse(permutation[l]));
                        tempPermutation.RemoveAt(l);
                        
                    }
                    x_k++;



                }
                // k++; 

            }


            Pi.Add(Hub);

                return Pi;
        }

        private List<double> CountTotalDistance (List< int> permutation, string[,] table)
        {
        
            int MainCity = permutation[0];      
            double partDistance = 0.0;
            double totalDistance = 0.0;
            List<double> listOfDistances = new List<double>();
            
            for (int i =0; i < permutation.Count; i++)
            {
                if (i > 0)
                {
                    int indexX = permutation[i - 1] - 1;
                    int indexY = permutation[i] - 1;
                    partDistance += double.Parse(table[indexX, indexY]);
                    totalDistance += double.Parse(table[indexX, indexY]);
                   // Console.WriteLine("X: " + indexX.ToString() + " Y: " + indexY.ToString());

                    if (permutation[i] == MainCity && permutation[i] == MainCity)
                    {
                        listOfDistances.Add(partDistance);
                        partDistance = 0.0;
                      //  Console.WriteLine("JESTEM W DOMU");
                    }
                }
                else
                {
                    partDistance = 0;
                }
                
                
                
            }
           // Console.WriteLine(totalDistance + "\n\n\n\n\n");
            listOfDistances.Add(totalDistance);


            return listOfDistances;
        }

        



    }
}
