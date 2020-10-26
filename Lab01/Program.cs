using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
            Console.WriteLine("Ilość miast: " + quantityOfCities);
            //Variable to read unit (km or miles)
            var unit = reader.ReadLine().Split(";")[0];
            Console.WriteLine("Jednostka odległości: " + unit);

            string[,] table = new string[quantityOfCities,quantityOfCities];
            table = program.WriteTable(quantityOfCities, reader);
            program.DisplayTable( table, quantityOfCities);
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

            /*
            for (int i = 0; i < quantityOfCities; i++)
            {
                Console.WriteLine(cityName[i]);

            }
            */

              //  while (true)
            //{
                List<string> permutation = program.ReadPermutation();
                //program.DisplayPermutation(permutation);
                distances  = program.CountTotalDistance(permutation, table);
            program.VRP(table, permutation);
            //}

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

            // Reading permutation
            Console.WriteLine("Wpisz permutację: ");

            List<string> permutation = new List<string>();

            string city = null;
            do
            {

                city = Console.ReadLine();


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
            } while (city.Length != 0);


            return permutation;
        }
        private void DisplayPermutation (List <string> permutation)
        {
            for (int i = 0; i < permutation.Count; i++)
            {
                Console.Write(permutation[i] + " ");

            }
        }

        private int FindMaxDistance (string[,] table, List<string> permutation)
        {
            int indexToReturn=0;
            double maxDistance = 0;
            for (int i=1; i<permutation.Count; i++)
            {
                int indexY = int.Parse(permutation[i])-1;
                int indexX = int.Parse(permutation[0])-1;
                double tempDistance = double.Parse(table[indexX, indexY]);
              
               
                if (maxDistance < tempDistance)
                {
                    maxDistance = tempDistance;
                    indexToReturn = indexY;
                }
                
            }

            return indexToReturn;
        }

        private void VRP (string[,] table, List<string> permutation)
        {
            List<string> tempPermutation = new List<string>();
            tempPermutation = permutation;
            List<string> Pi = new List<string>();
           // Console.WriteLine(FindMaxDistance(table, permutation));


            Pi.Clear();

            for (int i=0; i<permutation.Count; i++)
            { int whatToRemove = FindMaxDistance(table, tempPermutation);
                Pi.Add(whatToRemove.ToString());
                tempPermutation.RemoveAt(whatToRemove);

                Console.WriteLine("USUWAM: " + whatToRemove);
                DisplayPermutation(tempPermutation);
                Console.WriteLine("\n");
            }
            
         }

        private List<double> CountTotalDistance (List< string> permutation, string[,] table)
        {
        
            int MainCity = int.Parse(permutation[0]);      
            double partDistance = 0.0;
            double totalDistance = 0.0;
            List<double> listOfDistances = new List<double>();
            
            for (int i =0; i < permutation.Count; i++)
            {
                if (i > 0)
                {
                    int indexX = int.Parse(permutation[i - 1]) - 1;
                    int indexY = int.Parse(permutation[i]) - 1;
                    partDistance += double.Parse(table[indexX, indexY]);
                    totalDistance += double.Parse(table[indexX, indexY]);
                   // Console.WriteLine("X: " + indexX.ToString() + " Y: " + indexY.ToString());

                    if (int.Parse(permutation[i]) == MainCity && int.Parse(permutation[i]) == MainCity)
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
