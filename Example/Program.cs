using System;
using System.IO;
using System.Threading;

using Contxt.Clients;
using Contxt.Nodes;
using Contxt.Nodes.Containers;

namespace ContxtExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set the console title
            Console.Title = "Contxt Example";

            // Create a console client
            ConsoleClient client = new ConsoleClient();

            // Create a parser
            Parser parser = new Parser();

            // Add containers to the parser
            parser.AddContainer("Choice", typeof(ChoiceNodeContainer));
            parser.AddContainer("Text", typeof(TextNodeContainer));
            parser.AddContainer("Set", typeof(SetNodeContainer));
            parser.AddContainer("Value", typeof(ValueNodeContainer));
            parser.AddContainer("Branch", typeof(BranchNodeContainer));

            // Read the example script
            string[] lines = File.ReadAllLines("./example.ctxt");

            // Parse the script
            ParseResult result = parser.Parse(lines);

            // If parsing failed, print the error
            if (result.Result != ParseResult.ResultType.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Fatal error!");
                Console.ResetColor();

                Console.Error.WriteLine("{0} on line {1}: {2}", result.Message, result.LineNumber, result.Line);
            }

            // Get the first node in the parsed tree
            INode<string> current = parser.GetNode<INode<string>>(1);

            // Execute every node until the end
            while (current != null)
            {
                current = current.Execute(client);
            }

            // Wait for input to exit
            Console.WriteLine("\nPress any key to exit . . .");
            Console.ReadKey(true);
        }
    }
}
