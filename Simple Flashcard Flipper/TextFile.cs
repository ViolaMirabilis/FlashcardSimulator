using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Flashcard_Flipper
{
    internal class TextFile
    {
        public void LoadTextFile()
        {
            string dir = @"C:\Users\zajac\Desktop\Flashcards";      // file path
            string[] flashcardTextFiles = Directory.GetFiles(dir);  // array which holds the names of the files
            int found = 0;

            // displays a message about the amount of 'flashcard sets'
            int counter = 1;
            Console.WriteLine($"Found {flashcardTextFiles.Length} sets in the folder.");
            foreach (string fileName in flashcardTextFiles)     // prints file names without 
            {
                found = fileName.IndexOf(".txt");
                // formatted print
                Console.Write($"{counter}. " + Path.GetFileName(fileName.Substring(0, found)));
                Console.WriteLine($" - " + Helpers.AmountOfFlashcardInGroup(fileName) + " flashcard(s)");

                counter++;
            }
        }
        public void LoadFolderFilesDefault()
        {
            string dir = @"C:\Users\zajac\Desktop\C# Projects\3. MyOwn\Simple Flashcard Flipper\Simple Flashcard Flipper\Flashcards";      // file path
            string[] flashcardTextFiles = Directory.GetFiles(dir);  // array which holds the names of the files
            int found = 0;


            // displays a message about the amount of 'flashcard sets'
            int counter = 1;
            Console.WriteLine($"Found {flashcardTextFiles.Length} sets in the folder.");
            foreach (string fileName in flashcardTextFiles)     // prints file names without 
            {
                found = fileName.IndexOf(".txt");
                // formatted print
                Console.Write($"{counter}. " + Path.GetFileName(fileName.Substring(0, found)));
                Console.WriteLine($" - " + Helpers.AmountOfFlashcardInGroup(fileName) + " flashcard(s)");

                counter++;
            }
        }

        public void LoadFolderFilesChosenByUser()
        {
            Console.Write("Enter folder's path: ");
            string dir = Console.ReadLine();

            string[] flashcardTextFiles = Directory.GetFiles(dir);  // array which holds the names of the files
            int found = 0;

            // displays a message about the amount of 'flashcard sets'
            int counter = 1;
            Console.WriteLine($"Found {flashcardTextFiles.Length} sets in the folder.");
            foreach (string fileName in flashcardTextFiles)     // prints file names without 
            {
                found = fileName.IndexOf(".txt");
                // formatted print
                Console.Write($"{counter}. " + Path.GetFileName(fileName.Substring(0, found)));
                Console.WriteLine($" - " + Helpers.AmountOfFlashcardInGroup(fileName) + " flashcard(s)");

                counter++;
            }

            //ChooseTxtFile(flashcardTextFiles);
            TransferTxtFileToFlashcardGroup (flashcardTextFiles, 0, dir);
        }

        public void ChooseTxtFile(string[] flashcardTextFiles)
        {
            Console.Write("File to import: ");
            int txtIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            string readText = File.ReadAllText(flashcardTextFiles[txtIndex]);       //txtIndex is basically the index of a text file to read
            Console.WriteLine(readText);
        }

        public void TransferTxtFileToFlashcardGroup(string[] flashcardTextFiles, int txtIndex, string dir)
        {
            int indexOfExtension = flashcardTextFiles[txtIndex].IndexOf(".txt");
            string flashcardGroupName = flashcardTextFiles[txtIndex].Substring(dir.Length + 1);         // cuts the path
            flashcardGroupName = flashcardGroupName.Substring(0, flashcardGroupName.IndexOf(".txt"));   // cuts the '.txt' at the end
            // creates a new group (with 0 flashcards)
            FlashcardLogic.FlashcardGroup flashcardGroup = new FlashcardLogic.FlashcardGroup(flashcardGroupName);
            // adds teh group to the global list of FlashcardGroups
            
            Console.WriteLine("Importing flashcards...");
            string[] readTextLineByLine = File.ReadAllLines(flashcardTextFiles[txtIndex]);

            for (int i = 0; i < readTextLineByLine.Length; i++)
            {
                string fullFlashcard = readTextLineByLine[i];       // reads the entire line with index i
                string l1 = fullFlashcard.Split('-')[0];            // reads the word before the symbol '-'
                string l2 = fullFlashcard.Split('-')[1];            // - after the symbol

                // creates a new flashcard (with language 1 and language 2)
                FlashcardLogic.Flashcard flashcard = new FlashcardLogic.Flashcard(l1, l2);
                flashcardGroup.Flashcards.Add(flashcard);       // adds the flashcard to the List<Flashcard> in the FlaschardGroup
            }

            FlashcardLogic.FlashcardGroup.AllGroups.Add(flashcardGroup);

        }
    }
}
