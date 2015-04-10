using System;

using Contxt.Nodes;

namespace Contxt.Clients
{
    /// <summary>
    /// Client implementation that uses <see cref="System.Console"/> for output and <see cref="Contxt.Clients.ClientBase{T}"/> for storage.
    /// </summary>
    /// <seealso cref="System.Console"/>
    public class ConsoleClient : ClientBase<string>
    {
        public ConsoleClient(bool repeatChoiceOnFailure = false) : base(repeatChoiceOnFailure)
        { }

        /// <summary>
        /// Gets the color for a string.
        /// 
        /// <para>
        /// This is calculated by multiplying the last two digits
        /// of the string's hash code. This value is then modded with 6
        /// to find the remainder which ranges between 0-5. It is then
        /// compared against the following lookup table.
        /// </para>
        /// 
        /// <list type="bullet">
        ///   <item>
        ///     <term>0</term>
        ///     <description><see cref="System.ConsoleColor.Red"/></description>
        ///   </item>
        ///   <item>
        ///     <term>1</term>
        ///     <description><see cref="System.ConsoleColor.Green"/></description>
        ///   </item>
        ///   <item>
        ///     <term>2</term>
        ///     <description><see cref="System.ConsoleColor.Blue"/></description>
        ///   </item>
        ///   <item>
        ///     <term>3</term>
        ///     <description><see cref="System.ConsoleColor.Cyan"/></description>
        ///   </item>
        ///   <item>
        ///     <term>4</term>
        ///     <description><see cref="System.ConsoleColor.Magenta"/></description>
        ///   </item>
        ///   <item>
        ///     <term>5</term>
        ///     <description><see cref="System.ConsoleColor.Gray"/></description>
        ///   </item>
        /// </list>
        /// </summary>
        /// <param name="text">String to return the color for.</param>
        /// <returns>Color for the provided string.</returns>
        private ConsoleColor GetColor(string text)
        {
            // Retrieve the hash code of the text and
            // find the last two digits.
            int hashCode = text.GetHashCode(),
                firstDigit = hashCode % 10,
                secondDigit = (hashCode / 10) % 10;

            // Multiply the values and mod them with 6 to
            // ensure that the value is in the range 0-5
            int value = (firstDigit * secondDigit) % 6;

            // Look up the value and return the color assigned to it.
            switch (value)
            {
                case 0:
                    return ConsoleColor.Red;
                case 1:
                    return ConsoleColor.Green;
                case 2:
                    return ConsoleColor.Blue;
                case 3:
                    return ConsoleColor.Cyan;
                case 4:
                    return ConsoleColor.Magenta;
                case 5:
                    return ConsoleColor.Gray;
            }

            // If the value wasn't found, return white.
            return ConsoleColor.White;
        }

        /// <summary>
        /// Outputs a node to the user.
        /// 
        /// <para>
        /// If the node has a source, it is printed before the
        /// node's value in the color provided by <see cref="GetColor(string)"/>.
        /// </para>
        /// </summary>
        /// <param name="node">Node to output.</param>
        public override void Text(INode<string> node)
        {
            // If the node has a source, output it in
            // the color provided.
            if (node.Source != null)
            {
                Console.ForegroundColor = GetColor(node.Source);
                Console.Write("{0}", node.Source);
                Console.ResetColor();
                Console.Write(": ");

            }

            // Output the value of the node.
            Console.WriteLine(node.Value);
        }

        /// <summary>
        /// Outputs a choice to the user.
        ///
        /// <para>
        /// If the node has a source, it is printed before the
        /// node's value in the color provided by <see cref="GetColor(string)"/>.
        /// </para>
        ///
        /// <para>This color is also used for the choice numbers and input.</para>
        /// </summary>
        /// <param name="node">Node to output.</param>
        /// <param name="choices">Choices that the user can select from.</param>
        /// <param name="choice">The node selected by the user.</param>
        /// <returns><b>true</b> if the user has selected a valid node, otherwise <b>false</b></returns>
        public override bool DoChoice(INode<string> node, INode<string>[] choices, out INode<string> choice)
        {
            // Set the current choice to null.
            choice = null;

            // If no choices were provided, return a success.
            if (choices.Length == 0)
            {
                return true;
            }

            // Store the starting line before outputting choices.
            // This is used when removing the choices after a choice
            // has been made.
            int startLine = Console.CursorTop;

            // Store the current foreground color incase there is no source.
            ConsoleColor foregroundColor = Console.ForegroundColor;

            // If there is a source, retrieve the color for it and use it as
            // the foreground color.
            if (node.Source != null)
            {
                foregroundColor = GetColor(node.Source);
            }

            // Iterate each choice to be printed.
            for (int i = 0; i < choices.Length; i++)
            {
                // Set the foreground color for the choice number,
                // print the choice and reset the foreground color.
                Console.ForegroundColor = foregroundColor;
                Console.Write(" {0}", i + 1);
                Console.ResetColor();

                // Print the value for the current choice.
                Console.WriteLine(": {0}", choices[i].Value);
            }

            // Print the prompt for input.
            Console.Write("> ");

            // Set the foreground color for the input,
            // read the input and reset the foreground input.
            Console.ForegroundColor = foregroundColor;
            string response = Console.ReadLine();
            Console.ResetColor();

            // Store the end line after outputting choices.
            int endLine = Console.CursorTop;

            // Iterate the lines between the start and end lines,
            // setting the cursor to the start of the line and
            // writing a full line of spaces to clear the console.
            for (int i = startLine; i < endLine; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new String(' ', Console.WindowWidth));
            }

            // Reset the cursor's position to where it was before
            // output began.
            Console.SetCursorPosition(0, startLine);

            // Attempt to convert the user's choice to a number and output it
            // to the choice output. If this fails, the user is informed and
            // a failure is returned.
            try
            {
                // Convert the user's input to a number.
                int choiceNumber = Convert.ToInt32(response);

                // Ensure the number is within the range of choices available.
                // If it is not, inform the user (unless we are repeating) and return a failure.
                if (choiceNumber < 1 || choiceNumber > choices.Length)
                {
                    // If we are not repeating then output the error.
                    if (!RepeatChoiceOnFailure)
                    {
                        Console.WriteLine("Invalid choice!");
                    }

                    return false;
                }

                // Assign the node that the user selected to the choice output.
                choice = choices[choiceNumber - 1];

                return true;
            }
            catch (FormatException)
            {
                // If we are not repeating then output the error.
                if (!RepeatChoiceOnFailure)
                {
                    Console.WriteLine("Invalid choice!");
                }

                return false;
            }
        }
    }
}
