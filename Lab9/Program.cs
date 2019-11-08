using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab9
{
    class Program
    {
        //use regex for validation when adding name.
        static void Main(string[] args)
        {
            List<string> names = new List<string>() { "Michael Scott", "Jim Halpert", "Dwight Schrute" };
            List<string> spouse = new List<string>() { "Holly Flax", "Pam Beesly", "Angela Martin" };
            List<string> city = new List<string>() { "Boulder, CO", "Austin, TX", "Scranton, PA" };
            List<string> job = new List<string>() { "Department of Natural Resources", "Athleap", "Dunder Mifflin" };
            List<string> color = new List<string>() { };

            bool repeat = true, again = true;

            

            while (again)
            {

                Console.WriteLine("Do you want to know somthing about a person(type 'know') or add a new person (type 'add')? ");
                string whatToDo = Console.ReadLine();
                if (whatToDo == "add")
                {
                    Console.Write("Name (First and Last name): ");
                    string newName = Console.ReadLine();
                    string namePattern =  @"([A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+)";
                    names.Add(GetMatch(newName, namePattern));
                    

                    Console.Write("Spouse Name (First and Last name. If no spouse, write None None): ");
                    string spouseName = Console.ReadLine();
                    spouse.Add(GetMatch(spouseName, namePattern));

                    Console.Write("City: ");
                    string newCity = Console.ReadLine();
                    string cityPattern = @"(([A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+)|[A-Z]{1}[a-z]+)";
                    city.Add(GetMatch(newCity, cityPattern));

                    Console.Write("Job: ");
                    string newJob = Console.ReadLine();
                    string jobPattern = @"([A-Za-z]+\ |[A-Za-z])";
                    job.Add(GetMatch(newJob, jobPattern));

                    
                    

                    again = true;
                }
                else if (whatToDo == "know")
                {
                    color.Clear();

                    Console.WriteLine("Please add a color for each person");

                    for (int i = 0; i < names.Count; i++)
                    {
                        Console.Write($"{names[i]}: ");
                        string newColor = Console.ReadLine();//assume that they will input a valid color.
                        string colorPattern = @"([A-Za-z]+\ |[A-Za-z])";
                        color.Add(GetMatch(newColor, colorPattern));//used to validate for string only. know numbers will be allowed. 
                    }

                    Console.WriteLine("Who do you want to learn about?");
                    for (int i = 0; i < names.Count; i++)
                    {
                        Console.WriteLine(i + 1 + "." + names[i]);
                    }
                    //take in user input
                    string personName = Console.ReadLine();
                    //Using method for try catch 
                    int person = Who(personName, names);

                    Console.WriteLine($"You want to learn more about {names[person]}");
                    Console.WriteLine("What would you like to learn about him?");
                    Console.WriteLine("Spouse/SO, Current City, Job, Color or would you like to quit?");
                    //for spouse, city, job, and quiting/asking for another person. 

                    while (repeat)
                    {
                        string learn = Console.ReadLine().ToLower();

                        if (string.IsNullOrEmpty(learn))
                        {
                            Console.WriteLine("Please enter a valid response");
                            repeat = true;
                        }
                        else if (learn == "spouse" || learn == "so")
                        {
                            Console.WriteLine($"{spouse[person]} is the spouse of {names[person]}.");
                            Console.WriteLine("What else would you like to know? Or would you like to quit?");// Would you like to add a person?");
                            repeat = true;

                        }
                        else if (learn == "current city" || learn == "city")
                        {
                            Console.WriteLine($"{names[person]} lives in {city[person]}.");
                            Console.WriteLine("What else would you like to know? Or would you like to quit?");// Would you like to add a person?");
                            repeat = true;
                        }
                        else if (learn == "job")
                        {
                            Console.WriteLine($"{names[person]} works at {job[person]}.");
                            Console.WriteLine("What else would you like to know? Would you like to quit?");// Would you like to add a person?");
                            repeat = true;
                        }
                        else if (learn == "color")
                        {
                            Console.Write($"{names[person]} assigned color is {color[person]}. ");
                            Console.WriteLine("What else would you like to know? Would you like to quit?");// Would you like to add a person?");
                            repeat = true;

                        }
                        else if (learn == "quit")
                        {
                            Console.WriteLine("Would you like to learn about someone else? [y/n]: ");
                            string ans = Console.ReadLine();

                            if (ans == "n" || ans == "no")
                            {
                                //quiting the whole program
                                Console.WriteLine("Goodbye!");
                                repeat = false;
                                again = false;
                            }
                            else if (ans == "y" || ans == "yes")
                            {
                                //going back to first while loop to ask new person
                                again = true;
                                repeat = false;
                            }
                        }
                        //else if (string.IsNullOrEmpty(learn))
                        //{
                        //    Console.WriteLine("Please enter a valid response");
                        //    repeat = true;
                        //}
                        else
                        {
                            Console.WriteLine("Please enter a valid response");
                            repeat = true;
                        }
                    }
                    repeat = true;//resets repeat while loop
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                    again = true;
                }

            }

        }
        public static int Who(string input, List<string> listInput)
        {


            int person = 0;
            bool again = true;
            string test = "";
            while (again)
            {

                try
                {
                    person = int.Parse(input) - 1;
                    test = listInput[person];
                    again = false;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("That person does not exist. Please enter a valid number");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
                //again = true;
            }
            return person;

        }        
        /// <summary>
        /// TryCatch for the newly input data using Regex. 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GetMatch(string input, string pattern)
        {
            bool again = true;
            while(again)
            {
                try
                {
                    Match getMatch = Regex.Match(input, pattern);
                    if (getMatch.Success)
                    {
                        string output = $"{input}";
                        //return output;
                        again = false;
                    }
                    else
                    {
                        //string output = "input is NOT valid";
                        //return output;
                        Console.WriteLine($"Please input a valid input");
                        input = Console.ReadLine();
                        again = true;

                    }
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("Please enter a valid input");
                    input = Console.ReadLine();
                    again = true;
                }
            }
            return input;
            
        }
    }
}
