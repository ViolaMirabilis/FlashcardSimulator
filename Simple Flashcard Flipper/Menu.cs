using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Simple_Flashcard_Flipper;
using static Simple_Flashcard_Flipper.FlashcardLogic;

namespace Simple_Flashcard_Flipper
{
    internal class Menu
    {
        internal void ShowMenu()
        {
            FlashcardLogic logic = new FlashcardLogic();

            bool isRunning = false;
            while (!isRunning)
            {
                Console.Clear();
                Console.WriteLine("------- Welcome to FlashCardZ -------");
                Console.WriteLine("1. Add new flashcards\n2. Add flashcards to a set\n3. Display available sets\n4. Display flashcards from a given set\n5. Flashcards one-by-one!\n6. Test your knowledge!\n7. Import flashcards from .txt");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        FlashcardLogic.NewFlashcard();
                        break;
                    case "2":
                        Console.Clear();
                        FlashcardLogic.AddFlashcardsToSet();
                        break;
                    case "3":
                        Console.Clear();
                        FlashcardLogic.DisplayAllGroups();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        FlashcardLogic.DisplayFlashcardFromGivenSet();
                        break;
                    case "5":
                        Console.Clear();
                        FlashcardLogic.FlashcardShuffle();
                        break;
                    case "6":
                        FlashcardLogic.FlashcardTestMode();
                        break;
                    case "7":
                        FlashcardLogic.ImportFlashcardsFromUserTxtFile();
                        Console.ReadKey();
                        break;
                    case "9":
                        isRunning = false; break;
                }
            }
            
        }


    }
}
