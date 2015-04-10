using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class ChoiceNodeContainer : NodeContainerBase
    {
        public ChoiceNodeContainer(ParseData parseData) : base(parseData)
        {
            Node = new ChoiceNode<string>(parseData.Source, parseData.Value);
        }

        public override ParseResult Apply(Dictionary<int, INodeContainer> containers)
        {
            ChoiceNode<string> choiceNode = (ChoiceNode<string>) Node;

            try
            {
                foreach (string argument in ParseData.Arguments)
                {
                    int index = Convert.ToInt32(argument);

                    if (!containers.ContainsKey(index))
                    {
                        return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, String.Format("Invalid index argument \"{0}\"", index));
                    }

                    choiceNode.AddChoice(containers[index].Node);
                }

                return ParseResult.Success;
            }
            catch (FormatException)
            {
                return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, "Failed to cast argument to an integer");
            }
        }
    }
}
