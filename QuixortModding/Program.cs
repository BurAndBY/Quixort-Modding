using Newtonsoft.Json;

namespace QuixortModding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Complain if we don't have a file to parse.
            if (args.Length == 0)
            {
                Console.WriteLine("Drag a txt file onto this program, where each line is a prompt to be converted.\nFor formatting information, refer to the GitHub README.");
                Console.ReadKey();
                return;
            }

            // Split text file.
            string[] text = File.ReadAllLines(args[0]);

            // Find the file type.
            switch (text[0])
            {
                case "[Teams]":
                    Teams.Import(args, text);
                    break;

                default:
                    Console.WriteLine("No data found to import.");
                    Console.ReadKey();
                    return;
            }
        }
    }
}