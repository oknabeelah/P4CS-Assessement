using System;
using System.Collections; //for the trueforall method, and 
using System.ComponentModel;
using System.Collections.Generic; //for lists
using System.Linq; //for the sum of list 
using System.Globalization; 
using static System.Runtime.InteropServices.JavaScript.JSType; 
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;

namespace p4cs
{ //on the github website 
    class Coursework
    {
        public static void Main(string[] args)
        {
            bool ShouldEnd = false;
            while (!ShouldEnd) //when option d is chosen
            {
                string choice = ""; // declaring and initialising the input variable
                bool ValidChoice = false; // declaring and initialising the while loop condition
                while (ValidChoice == false) // while loop for error handling
                {
                    Console.WriteLine("Welcome to the Menu");
                    Console.WriteLine("---------------------- \n");
                    Console.WriteLine("a) Trinary Converter \n");
                    Console.WriteLine("b) School Roster \n");
                    Console.WriteLine("c) ISBN Verifier\n");
                    Console.WriteLine("d) End the Program \n");
                    Console.Write("What application would you like to run:");
                    // is it possible to return back to "welcome to the menu" after any application except end program is chosen
                    choice = Console.ReadLine().ToLower(); // take into account capital letter inputs
                    if (choice.Length == 1)
                    {
                    }
                    else
                    {
                        Console.WriteLine("Enter only one character!"); // ensuring input lenght is not more than 1 character
                    }
                    switch (choice)
                    {
                        case "a":
                            Console.WriteLine("You have selected the Trinary Converter");
                            ValidChoice = true;
                            TrinaryConverter(); //method
                            break;
                        case "b":
                            Console.WriteLine("You have selected the School Roster");
                            ValidChoice = true;
                            SchoolRoster(); //
                            break;
                        case "c":
                            Console.WriteLine("You have selected the ISBN Verifier");
                            ValidChoice = true;
                            ISBNVerifier(); // 
                            break;
                        case "d":
                            Console.WriteLine("You have selected to End the Program");
                            ValidChoice = true;
                            Console.WriteLine("Goodbye...");
                            ShouldEnd = true;
                            break;
                        default:
                            Console.WriteLine("Enter only the values 'a', 'b', 'c', or 'd'");
                            ValidChoice = false;
                            break;
                    }
                }
            }

            //hello hello this is the main method    
        }
        public static void TrinaryConverter()
        {
            /* test plan
            input--expected result--output
            1--1--1
            2--2--2
            10--3--3
            0--0--0
            112--14--14
            1122000120--32091--32091
            7--error--error
            12--5--5
            22--8--8
            100--9--9
            102--11--11
            221--25--25
            1000--27--27
            1222--53--53
            2222--80--80
            22122--233--233
             */
            bool ValidInput = false; //for tryparse
            int input = 0; //initialising here to use in all parts of the method
            string trinput = " "; //same           
            do
            { 
                Console.WriteLine("Welcome to the Trinary Converter !");
                Console.WriteLine("---------------------------------\n");
                Console.WriteLine("Enter a number in trinary to be converted to decimal or 'q' to quit:");
                trinput = Console.ReadLine();

                if (trinput.ToLower() == "q")
                {
                    break; // end loop 
                }
                if (int.TryParse(trinput, out input)) // to validate digits
                {
                    DigitsList = GetSplitDigit(input);
                    ValidInput = DigitsList.TrueForAll(IsGreater);
                    if (ValidInput == false)
                    {
                        Console.WriteLine("ERROR! ONLY USE VALID NUMNBERS (0, 1, OR 2) \n");
                        DigitsList.Clear(); // clears digitlist when an error is detected so it can properly validate the next number
                    }
                }
                else
                {
                    Console.WriteLine("INVALID INPUT! TRY AGAIN \n");
                }
            }
            while (DigitsList.Count() == 0 || !DigitsList.TrueForAll(IsGreater)); // digitlist is empty and all numbers do not meet the isgreater parameters
            if (trinput.ToLower() != "q") // avoid further computation when q is entred 
            {
                GetPowerList(DigitsList);
                double result = PowerList.Sum();
                Console.WriteLine("The decimal equivalent of {0} is {1}", input, result); //possible option of showing solution                                       
            }
        }
        static List<int> DigitsList = new List<int>(); //initilising this list here to make it accessible amongst other methods
        private static List<int> GetSplitDigit(int input)
        {
            int modulus = 10;  //initialise variable
            int divider = 1;
            for (bool stop = false; stop == false; modulus *= 10, divider *= 10)  //setting variables for the loop, do stop twice to double check and catch non zero values after a 0 
            {
                if (input / modulus == 0)
                {
                    stop = true;
                }
                else
                {
                }
                int result = input % modulus;
                result = result / divider;
                DigitsList.Add(result);
            }
            return DigitsList;
        }
        private static bool IsGreater(int number) => 0 <= number && number < 3; // check if trinary parameters are met by input list

        static List<double> PowerList = new List<double>();
        private static List<double> GetPowerList(List<int> DigitList)
        {
            //foreach digit in the digit list, multiply by 3^its position
            for (int index = 0; index < DigitList.Count; ++index)
            {
                int digit = DigitList[index];
                double powerdigit = digit * Math.Pow(3, index);
                PowerList.Add(powerdigit);
                // Console.WriteLine($"{digit} * 3^{index} = {powerdigit}");
            }
            return PowerList;
        }
        public static void SchoolRoster()
        {
            Console.WriteLine("Welcome to the School Roster");
            Console.WriteLine("we keep a register of all the students in this school and their respective forms.");
            MakeRoster();
            Console.WriteLine("You have selected to stop entering students to the register. ");
            Console.WriteLine("Here are all the students in the register seperated by forms:");
            DisplaySortedNames(Roster);
        }
        static Dictionary<string, int> Roster = new Dictionary<string, int>(); //(key, value) pair
        public static void MakeRoster()
        {
            //dictionary where the key is the name since its unique and the values can be the form          
            string student = ""; //declaring and intialising variables 
            int form = 0;
            do
            {
                Console.WriteLine("Enter a student name or 'q' to quit:");
                student = Console.ReadLine().ToLower(); //case insensitivity
                if (student == "q")
                {
                    break; // exit loop
                }
                if (!IsAllLetters(student))
                {
                    Console.WriteLine("ERROR! ENTER ONLY LETTERS FOR THE STUDENT'S NAME:");
                    continue; //ensure while loop is continued
                }
                Console.WriteLine("What form is {0} in?: ", student);
                string tryform = Console.ReadLine();
                if (int.TryParse(tryform, out form))
                {
                    if (Roster.TryAdd(student, form))
                    {
                        Console.WriteLine("{0} has been added to form {1}", student, form);
                    }
                    else
                    {
                        Console.WriteLine("INVALID INPUT! {0} ALREADY EXISTS IN OUR REGISTER. ENTER A DIFFERENT NAME:", student);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR! ENTER ONLY A NUMBER FOR THE STUDENT'S FORM:");
                }
            }
            while (true);  // ensures while loop continues regardless and so when q is typed it ends.                           
        }
        public static void DisplaySortedNames(Dictionary<string, int> Roster)
        {
            var SortedRoster = Roster.OrderBy(input => input.Value).ThenBy(input => input.Key, StringComparer.OrdinalIgnoreCase).GroupBy(input => input.Value);
                                      // sort by form               then by name (alphabetically)                               group by form and action case insensitivity
            foreach (var input in SortedRoster)
            {
                Console.Write("Form {0}: ", input.Key); // form x: 
                Console.WriteLine(string.Join(", ", input.Select(input => input.Key))); // form x: name, name, name...
            }
        }
        // error handling name must be all letters?
        public static bool IsAllLetters(string input)
        {
            return input.All(char.IsLetter); //check if all characters in the name is a letter
        }

        public static void ISBNVerifier()
        {
            string isbn = " ";
            while (true)
            {
                Console.WriteLine("Welcome to the ISBN Verifier");
                Console.WriteLine("Enter an ISBN to check its validity or 'q' to quit: ");
                isbn = Console.ReadLine();
                //remove hypen, convert x to 10 and check validity
                //error handling, check format is followed, check there is an x, check there are 10 digits/characters inputted
                if (isbn.ToLower() == "q")
                {
                    break;
                }
                string[] isbnparts = isbn.Split('-'); // split the input isbn into strings in an array at the hyphens
                if (isbnparts.Length != 4 || isbnparts[0].Length != 1 || isbnparts[1].Length != 3 || isbnparts[2].Length != 5 || isbnparts[3].Length != 1)
                {
                    Console.WriteLine("INVALID INPUT! USE THE APPROPRIATE ISBN FORMAT.");
                }

                if (ValidISBN(isbn)) // if check == 0 and return true
                {
                    Console.WriteLine("VALID ISBN");
                }
                else
                {
                    Console.WriteLine("INVALID ISBN");
                }
            }      
        }      
        public static bool ValidISBN(string isbn)
        {
            var isbndigits = isbn.Where(x => char.IsDigit(x) || char.ToLower(x) == 'x' && isbn.IndexOf(x) == isbn.Length - 1)  // ensures x is only considered a 10 when it is in the last place of the isbn and nowhere else
                              .Select(x => char.IsDigit(x) ? int.Parse(x.ToString()) : 10) // change x to 10: select x from isbn if found appropriately, then convert from a char to a string and then parse as a number assigned 10, also convert all other characters to their int value
                              .ToList(); // put all the converted int into a list<int>
                                         //https://stackoverflow.com/questions/21128872/how-do-i-convert-part-of-a-string-to-int-in-c 
                                         //https://stackoverflow.com/questions/37414839/selecting-max-number-from-a-list-of-string-in-linq

            if (isbndigits.Count != 10)
            {
                return false;
            }
            int check = 0;
            int index = 0;
            foreach (int digit in isbndigits) 
            {
                check += (10 - index) * digit; // check = check + new digit* indexcorrected for every loop turn
                index++;                       // ensures index increases with every loop
            }
            check %= 11; // check = check % 11 
            
            return check == 0;  //check (the regular one) if result is 0 to confirm validity and return true
            
        }       
    }
}
