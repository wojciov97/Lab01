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
            

            List <string> permutation = program.ReadPermutation();
            program.DisplayPermutation(permutation);
            program.CountDistance(permutation, table);
            
            






           
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

        public void CountDistance (List< string> permutation, string[,] table)
        {
            int ComeBack = 0;
            int MainCity = int.Parse(permutation[0]);
            Console.Write(MainCity);
            for( int i=1; i< permutation.Count; i++)
            {
                if (int.Parse(permutation[i]) == MainCity)
                {
                    ComeBack = i;
                }
            }
            Console.WriteLine("\n"+ComeBack);

            string Distance = null;
            string jakas;
            for (int i =0; i < ComeBack; i++)
            {
                //Distance += double.Parse(table[MainCity - 1, int.Parse(permutation[i])]);
                jakas = table[(MainCity - 1), int.Parse(permutation[i])];
                Distance += jakas;
               // double wartosc = Convert.ToDouble(jakas);
                //Distance += Convert.ToDouble(jakas);
                Console.WriteLine("x: "+ (MainCity - 1) +"y: "+ int.Parse(permutation[i])+"  " + jakas);
                // Distance += double.Parse(table[MainCity - 1,1]);
                Console.WriteLine(Distance);
            }
            
        }


    }
}
