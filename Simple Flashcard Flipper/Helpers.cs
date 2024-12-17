using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Flashcard_Flipper
{
    internal class Helpers
    {
        public static void Date()   // yet to have impact
        {
            DateTime date = new DateTime();

        }
        public static void Color(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static string AmountOfFlashcardInGroup(string filename)     // counts the amount of flashcards (rows) in a text file
        {
            string[] lines = File.ReadAllLines($@"{filename}");
            int cnt = lines.Count();

            return cnt.ToString();
        }
    }
}
