using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simple_Flashcard_Flipper
{
    internal class FlashcardLogic
    {
        public class Flashcard
        {
            // a list of flashcard TUPLES
            //public static List<(string polish, string english)> Flashcards = new List<(string polish, string english)>();
            public static List<Flashcard> Flashcards = new List<Flashcard>();
            public string PolishCard { get; set; }
            public string EnglishCard { get; set; }

            public Flashcard(string polish, string english)
            {
                PolishCard = polish;
                EnglishCard = english;

                //var flashcardTuple = (polish: polish, english: english);             // a tuple that holds polish and english card
                //Flashcards.Add(flashcardTuple);         // adding a tuple to the list with the .ToString()
            }
        }

        public class FlashcardGroup
        {
            public static List<FlashcardGroup> AllGroups = new List<FlashcardGroup>();
            public string Name { get; set; }    // name of the FlashcardGroup, e.g., "Maths", "Polish", "English", which has a few flashcards inside
            public List<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

            // a constructor that creates an empty list if a user doesn't provide a flashcard
            public FlashcardGroup(string name)
            {
                Name = name;
                Flashcards = new List<Flashcard>();
                //AllGroups.Add(this);

            }

            public FlashcardGroup(string name, List<Flashcard> flashcards)
            {
                Name = name;
                Flashcards = new List<Flashcard>();
                //AllGroups.Add(this);
            }
        }

        // make it a bool method so it runs IF input is true/false? It would skip the bottom while (input) loop!
        public static Flashcard NewFlashcard()      // adds infinite amount of flashcards to the GLOBAL FLASHCARD LIST
        {
            Console.Write("Polish: ");
            string polish = Console.ReadLine();
            if (string.IsNullOrEmpty(polish))
                return null;

            Console.Write("English: ");
            string english = Console.ReadLine();
            if (string.IsNullOrEmpty(english))
                return null;

            Flashcard newFlashcard = new Flashcard(polish, english);        // creates a new flashcard

            Flashcard.Flashcards.Add(newFlashcard);                         // adds the flashcard to the GLOBAL list of flashcards

            return newFlashcard;
        }
        public static void AnswerShufflingAlgorithm(List<string> testAnswers)      // takes a list of possible "answers" and shuffles them.
        {
            Random rnd = new Random();
            for (int i = 0; i < testAnswers.Count; i++)
            {
                int randomIndex = rnd.Next(i, testAnswers.Count);
                string temp = testAnswers[i];       // stores a temporary answer
                testAnswers[i] = testAnswers[randomIndex];  // replaces the current index with a randomly chosen variable
                testAnswers[randomIndex] = temp;            // puts the temporarily picked variable into the previous spot
            }
        }
        public static void AddFlashcardsToSet()
        {
            int counter = 0;
            Console.Write("Name of your set: ");
            string flashcardGroupName = Console.ReadLine();
            bool setExists = false;

            foreach (var FlashcardGroup in FlashcardGroup.AllGroups)        // checks if a set with the given name already exists
            {
                if (FlashcardGroup.Name == flashcardGroupName)
                {
                    Console.WriteLine($"Set {flashcardGroupName} already exists!\nAdding new flashcards to the {flashcardGroupName} set!");
                    while (true)
                    {
                        var flashcard = NewFlashcard();

                        if (flashcard == null)
                        {
                            FlashcardGroup.AllGroups.Add(FlashcardGroup); // Add the new set to the global list
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"Successfully added {counter} flashcards to the set!");
                            Console.ResetColor();
                            Console.ReadKey();
                            break;
                        }
                        FlashcardGroup.Flashcards.Add(flashcard);
                    }

                    setExists = true;
                    break;
                }
            }

            if (!setExists)
            {
                FlashcardGroup newGroup = new FlashcardGroup(flashcardGroupName);
                Console.WriteLine($"Flashcard set '{flashcardGroupName}' created!");
                while (true)
                {
                    var flashcard = NewFlashcard();
                    counter++;

                    if (flashcard == null)
                    {
                        FlashcardGroup.AllGroups.Add(newGroup); // Add the new set to the global list
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"Successfully added {counter} flashcards to the set!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    }

                    newGroup.Flashcards.Add(flashcard);
                }
            }
        }

        public static void DisplayAllGroups()
        {
            int counter = 1;

            foreach (var FlashcardGroup in FlashcardGroup.AllGroups)            // might want to change it to a normal for loop instead of foreach to get rid of the counter variable
            {
                Console.WriteLine($"{counter}. {FlashcardGroup.Name} - {FlashcardGroup.Flashcards.Count} flashcards!");
                counter++;
            }
        }

        public static void DisplayFlashcardFromGivenSet()
        {
            DisplayAllGroups();
            Console.Write("Choose your set: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Clear();

            var chosenSet = FlashcardGroup.AllGroups[index];

            foreach (var flashcard in chosenSet.Flashcards)
            {
                Console.WriteLine($"{flashcard.PolishCard} - {flashcard.EnglishCard}");
            }
            Console.ReadKey();
        }

        public static void FlashcardShuffle()
        {
            DisplayAllGroups();

            Console.Write("Choose your set: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Clear();

            var chosenSet = FlashcardGroup.AllGroups[index];
            int amountOfFlashcards = FlashcardGroup.AllGroups[index].Flashcards.Count;
            int counter = 1;
            foreach (var flashcard in chosenSet.Flashcards)
            {
                Console.WriteLine($"{flashcard.PolishCard}");
                Console.WriteLine($"{counter}/{amountOfFlashcards}");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine($"{flashcard.EnglishCard}");
                Console.WriteLine($"{counter}/{amountOfFlashcards}");
                Console.ReadKey();
                Console.Clear();

                counter++;
            }
            Console.WriteLine("Press ANY key to go to the main menu!");
            Console.ReadKey();
        }
        public static void FlashcardTestMode()
        {
            List<int> usedNumbers = new List<int>();        // a list used to store used answers in the test mode so they do not repeat.
            Random rnd = new Random();

            DisplayAllGroups();
            Console.Write("Choose your set: ");
            int index = Convert.ToInt32(Console.ReadLine()) -1;
            var chosenSet = FlashcardGroup.AllGroups[index];      // chooses a set with the input from above
            Console.Clear();

            int questionsCounter = 0;       // declaring an integer before the while loop. It indicates test mode answers, i.e.: 1/17, 2/17, 3/17...
            bool isDoingTheTest = false;

            while (!isDoingTheTest)
            {
                int amountOfFlashcards = FlashcardGroup.AllGroups[index].Flashcards.Count;  // checks how many flashcards are there in the set
                int randomFlashcardIndex;
                
                do
                {
                    randomFlashcardIndex = rnd.Next(amountOfFlashcards);        // chooses random index in the range of the flashcard Set
                } while (usedNumbers.Contains(randomFlashcardIndex));

                usedNumbers.Add(randomFlashcardIndex);                          // adds flashcard index to the list so it won't be used again in this rotation.

                // try catch if there aren't enough flashcards in the set (4 is the minimum)!!!!!!!!!!!
                var correctPolishFlashcard = chosenSet.Flashcards[randomFlashcardIndex].PolishCard;
                var correctEnglishFlashcard = chosenSet.Flashcards[randomFlashcardIndex].EnglishCard;

                List<string> testAnswers = new List<string> { correctEnglishFlashcard };        // a list of four answers, including the correct one.

                int loopCounter = 0;
                while (loopCounter < 3)
                {
                    string incorrectAnswer;
                    do
                    {
                        incorrectAnswer = chosenSet.Flashcards[rnd.Next(amountOfFlashcards)].EnglishCard;

                    } while (testAnswers.Contains(incorrectAnswer));

                    testAnswers.Add(incorrectAnswer);
                    loopCounter++;

                }

                // SHUFFLING ALGORITHM ("Fisher–Yates shuffle")
                AnswerShufflingAlgorithm(testAnswers);

                // Displays the test question and 4 answers
                char answerIndication = 'A';
                Console.WriteLine($"{correctPolishFlashcard}");
                for (int i = 0; i < testAnswers.Count; i++)
                {
                    Console.WriteLine($"{answerIndication}. {testAnswers[i]}");
                    answerIndication++;     // increments a char, which results in the answers shown as: a, b, c, d
                }
                questionsCounter += 1;
                Console.WriteLine($"\t{questionsCounter} / {chosenSet.Flashcards.Count}");       // show's a counter of questions, i.e.: 3/17

                bool isAnswering = false;
                while (!isAnswering)
                {
                    Console.Write("Answer: ");
                    string input = Console.ReadLine();
                    // totally not future proof. switching the ternary operator to if/else is crucial so I can store correct/incorrect answers.
                    switch (input[0])
                    {
                        case 'a':
                            Console.WriteLine(testAnswers[0] == correctEnglishFlashcard ? "Correct" : "Incorrect");
                            isAnswering = false;
                            break;
                        case 'b':
                            Console.WriteLine(testAnswers[1] == correctEnglishFlashcard ? "Correct" : "Incorrect");
                            isAnswering = false;
                            break;
                        case 'c':
                            Console.WriteLine(testAnswers[2] == correctEnglishFlashcard ? "Correct" : "Incorrect");
                            isAnswering = false;
                            break;
                        case 'd':
                            Console.WriteLine(testAnswers[3] == correctEnglishFlashcard ? "Correct" : "Incorrect");
                            isAnswering = false;
                            break;
                        case 'e':       // this is for exit
                            break;
                        default:
                            Helpers.Color(ConsoleColor.Red, "No answer provided. Try again!");
                            break;
                    }
                    isAnswering = false;
                    break;
                }

                if (questionsCounter == chosenSet.Flashcards.Count)
                {
                    Console.WriteLine("TEST COMPLETE!");
                    Console.ReadLine();
                    isDoingTheTest = true;
                    break;

                }
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            
            
            Console.Write("PRESS ANY KEY TO EXIT");
            Console.ReadKey();

        }
        public static void ImportFlashcardsFromTxtFileDefault()
        {

        }
        public static void ImportFlashcardsFromUserTxtFile()
        {
            TextFile txtFile = new TextFile();
            txtFile.LoadFolderFilesChosenByUser();

        }

        
    }
}
