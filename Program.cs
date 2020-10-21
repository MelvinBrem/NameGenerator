using System;
using System.Security.Cryptography;

namespace NameGenerator
{
    class RandomLetter
    {
        static Random rnd = new Random();
        public static char GetLetter(int count, string name)
        {
            //Defining the character sets
            string vowels = "aeiou";
            string vowelsUnrepeatable = "iu";
            int vowelRepeatChance = 30;
            string consonants = "bcdfghjklmnpqrstvwxz";
            string consonantsRepeatable = "bdfgklmnprst";
            int consonantsRepeatChance = 20;

            char newLetter = '?';

            if(count == 0) //First character of the name
            {
                //Decide whether to start with a consonant or vowel
                if (rnd.Next(2) == 0) 
                {
                    newLetter = Char.ToUpper(vowels[rnd.Next(vowels.Length)]);

                } else
                {
                    newLetter = Char.ToUpper(consonants[rnd.Next(consonants.Length)]);
                }

            } else if(count == 1) //Second character of the name
            {
                char lastLetter = Char.ToLower(name[name.Length - 1]);

                if (vowels.Contains(lastLetter))
                {
                    newLetter = consonants[rnd.Next(consonants.Length)];
                }
                else if (consonants.Contains(lastLetter))
                {
                    newLetter = vowels[rnd.Next(vowels.Length)];
                }
                else //Failsafe
                {
                    newLetter = '?';
                }

            } else if(count > 1) //Third or onward character of the name
            {
                char lastLetter = Char.ToLower(name[name.Length - 1]);
                char scndlastLetter = Char.ToLower(name[name.Length - 2]);

                //If the last letter is a non-repeatable consonant, or if the last two letters are consonants
                if (consonants.Contains(lastLetter) && !consonantsRepeatable.Contains(lastLetter) || consonants.Contains(lastLetter) && consonants.Contains(scndlastLetter)) 
                {
                    
                    newLetter = vowels[rnd.Next(vowels.Length)];

                //If the last letter is a repeatable consonant, and the second-last character is not a consonant
                //then there is a chance of either the consonant to repeat, or a letter to be chosen from either set
                } else if (consonants.Contains(lastLetter) && !consonants.Contains(scndlastLetter) && consonantsRepeatable.Contains(lastLetter))
                {
                    
                    if(rnd.Next(100) < consonantsRepeatChance)
                    {
                        newLetter = lastLetter;
                    } else
                    {
                        //Pick a random letter from both sets
                        if (rnd.Next(2) == 0)
                        {
                            newLetter = vowels[rnd.Next(vowels.Length)];

                        }
                        else
                        {
                            newLetter = consonants[rnd.Next(consonants.Length)];
                        }
                    }

                 //if the last character is a repeatable wovel
                }else if (vowels.Contains(lastLetter) && !vowelsUnrepeatable.Contains(lastLetter))
                {
                    //if the second last character is alreadt a vowel
                    if (vowels.Contains(scndlastLetter)){

                        newLetter = consonants[rnd.Next(consonants.Length)];
                    }
                    else
                    {
                        //Pick a random letter from both sets
                        if (rnd.Next(100) < vowelRepeatChance)
                        {
                            newLetter = vowels[rnd.Next(vowels.Length)];

                        } else {
                            newLetter = consonants[rnd.Next(consonants.Length)];
                        }
                    }
                } else
                {
                    newLetter = consonants[rnd.Next(consonants.Length)];
                }

            }

            return newLetter;
        }
    }
    class NameGenerator
    {
        static void Main(string[] args)
        {
            enterNumOfLetters: Console.WriteLine("Enter the length of the name:");
            askNumOfLetters: string numOfLetters = Console.ReadLine();

            Console.WriteLine("Enter the number of names to generate:");
            string numOfNames = Console.ReadLine();

            int num = -1;
            int lengthOfName;
            int AmountOfNames;
            string name = "";

            //Catch any input on number of letters that isn't a number
            if(!int.TryParse(numOfLetters, out num))
            {
                Console.WriteLine("\"{0}\" Is not a recognized int\n\nEnter the number of letters:", numOfLetters);
                goto askNumOfLetters;
            }
            else
            {
                lengthOfName = Convert.ToInt32(numOfLetters);
            }

            //Catch any input on number of names that isn't a number
            if (!int.TryParse(numOfNames, out num))
            {
                Console.WriteLine("\"{0}\" Is not a recognized int\n\nEnter the number of names to generate:", numOfNames);
                goto askNumOfLetters;
            }
            else
            {
                AmountOfNames = Convert.ToInt32(numOfNames);
            }

            for (int z = 0; z < AmountOfNames; z++)
            {

                for (int y = 0; y < lengthOfName; y++)
                {

                    name += RandomLetter.GetLetter(y, name);

                };

                Console.WriteLine("{0}", name);

                name = "";
            };

            Console.WriteLine("\n");


            goto enterNumOfLetters;
        }
    }
}
