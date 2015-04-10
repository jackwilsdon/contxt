using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class ValueNodeContainer : NodeContainerBase
    {
        public ValueNodeContainer(ParseData parseData, bool overriden) : base(parseData)
        {
            if (!overriden)
            {
                Node = new ValueNode<string>(parseData.Source, parseData.Value);
            }
        }

        public ValueNodeContainer(ParseData parseData) : this(parseData, false)
        { }

        public override ParseResult Apply(Dictionary<int, INodeContainer> containers)
        {
            if (ParseData.Arguments.Length == 0)
            {
                return ParseResult.Success;
            }

            ValueNode<string> valueNode = (ValueNode<string>)Node;

            try
            {
                int index = Convert.ToInt32(ParseData.Arguments[0]);

                if (!containers.ContainsKey(index))
                {
                    return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, String.Format("Invalid index argument \"{0}\"", index));
                }

                if (index == ParseData.Index)
                {
                    return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, String.Format("Index argument is same as node index", index));
                }

                valueNode.Child = containers[index].Node;

                return ParseResult.Success;
            }
            catch (FormatException)
            {
                return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, "Failed to cast argument to an integer");
            }
        }
    }
}
