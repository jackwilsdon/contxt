using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Contxt.Nodes;
using Contxt.Clients;
using System.Reflection;

namespace Contxt.Nodes.Containers
{
    public class Parser
    {
        private static Regex NodeExpression = new Regex("^\\[([0-9]+)\\] (?:(.+?) )?\\[([A-Za-z]+)(?:,(.*))*\\] (.*)$");
        private static Regex WhitespaceExpression = new Regex("^\\s+$");

        private Dictionary<string, Type> containerTypes = new Dictionary<string, Type>();
        private Dictionary<int, INodeContainer> containers = new Dictionary<int, INodeContainer>();

        public void AddContainer(string type, Type containerType)
        {
            if (!typeof(INodeContainer).IsAssignableFrom(containerType))
            {
                throw new ArgumentException("must be assignable from INodeContainer", "containerType");
            }

            if (!containerType.IsClass)
            {
                throw new ArgumentException("must be a class", "containerType");
            }

            if (containerType.GetConstructor(new Type[] { typeof(ParseData) }) == null)
            {
                throw new ArgumentException("must have a constructor that accepts ParseData", "containerType");
            }

            containerTypes.Add(type, containerType);
        }

        public void RemoveContainer(string type)
        {
            if (!containerTypes.ContainsKey(type))
            {
                return;
            }

            containerTypes.Remove(type);
        }

        private INodeContainer GetContainer(ParseData parseData)
        {
            string type = parseData.Type;

            if (!containerTypes.ContainsKey(type))
            {
                return null;
            }

            ConstructorInfo constructor = containerTypes[type].GetConstructor(new Type[] { typeof(ParseData) });

            return (INodeContainer) constructor.Invoke(new object[] { parseData });
        }

        private ParseData GetParseData(int lineNumber, string line)
        {
            Match match = NodeExpression.Match(line);

            if (!match.Success)
            {
                return null;
            }

            string indexString = match.Groups[1].Value,
                   source = match.Groups[2].Value,
                   type = match.Groups[3].Value,
                   argumentsString = match.Groups[4].Value,
                   value = match.Groups[5].Value;

            if (WhitespaceExpression.IsMatch(source))
            {
                return null;
            }

            if (source.Length == 0)
            {
                source = null;
            }

            string[] arguments = new string[0];

            if (argumentsString.Length > 0)
            {
                arguments = argumentsString.Split(',');
            }

            try
            {
                int index = Convert.ToInt32(indexString);

                return new ParseData(index, source, type, arguments, value, lineNumber, line);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private ParseResult ParseLine(int lineNumber, string line)
        {
            if (line.Length == 0)
            {
                return ParseResult.Success;
            }

            ParseData parseData = GetParseData(lineNumber, line);

            if (parseData == null)
            {
                return ParseResult.Failure.Derive(lineNumber, line, "Invalid syntax");
            }

            INodeContainer container = GetContainer(parseData);

            if (container == null)
            {
                return ParseResult.Failure.Derive(lineNumber, line, String.Format("Unable to get node container for type \"{0}\"", parseData.Type));
            }

            if (containers.ContainsKey(container.Index))
            {
                return ParseResult.Failure.Derive(lineNumber, line, String.Format("Duplicate index \"{0}\"", container.Index));
            }

            containers.Add(container.Index, container);

            return ParseResult.Success;
        }

        private ParseResult DoParse(IEnumerable<string> lines)
        {
            int lineNumber = 1;

            foreach (string line in lines)
            {
                ParseResult result = ParseLine(lineNumber, line);

                if (result.Result != ParseResult.ResultType.Success)
                {
                    return result;
                }

                lineNumber++;
            }

            foreach (INodeContainer container in containers.Values)
            {
                ParseResult result = container.Apply(containers);

                if (result.Result != ParseResult.ResultType.Success)
                {
                    return result;
                }
            }

            return ParseResult.Success;
        }

        public void Clear(bool clearContainerTypes = false)
        {
            if (clearContainerTypes)
            {
                containerTypes.Clear();
            }

            containers.Clear();
        }

        public ParseResult Parse(IEnumerable<string> lines)
        {
            ParseResult result = DoParse(lines);

            if (result == null)
            {
                result = ParseResult.Failure.Derive(message: "Generic Error");
            }

            if (result.Result != ParseResult.ResultType.Success)
            {
                Clear();
            }

            return result;
        }

        public T GetNode<T>(int index) where T : INode<string>
        {
            if (!containers.ContainsKey(index))
            {
                return default(T);
            }

            INodeContainer container = containers[index];
            INode<string> node = container.Node;

            if (!typeof(T).IsAssignableFrom(node.GetType()))
            {
                return default(T);
            }

            return (T) node;
        }
    }
}
