using System;
using System.IO;
using System.Threading;

using Contxt.Clients;
using Contxt.Nodes;
using Contxt.Nodes.Containers;

namespace Contxt
{
    //       C
    //      / \
    //     /   \
    //    O     N
    //   / \   / \
    //  /   \ /   \
    // T     X     T

    public class Program
    {
        private static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("        C");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("       / \\");
            Console.WriteLine("      /   \\");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("     O     N");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("    / \\   / \\");
            Console.WriteLine("   /   \\ /   \\");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  T     X     T");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine();

            Console.ResetColor();
        }

        public static void Main(string[] args)
        {
            Console.Title = "Contxt";

            PrintLogo();

            ConsoleClient client = new ConsoleClient(true);

            Parser parser = new Parser();

            parser.AddContainer("Choice", typeof(ChoiceNodeContainer));
            parser.AddContainer("Text", typeof(TextNodeContainer));
            parser.AddContainer("Set", typeof(SetNodeContainer));
            parser.AddContainer("Value", typeof(ValueNodeContainer));
            parser.AddContainer("Branch", typeof(BranchNodeContainer));

            string[] lines = File.ReadAllLines("./story.ctxt");

            ParseResult result = parser.Parse(lines);

            if (result.Result != ParseResult.ResultType.Success)
            {
                Console.WriteLine("{0} on line {1}: {2}", result.Message, result.LineNumber, result.Line);
            }

            INode<string> current = parser.GetNode<INode<string>>(1);

            while (current != null)
            {
                current = current.Execute(client);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("THE END");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
