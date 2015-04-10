using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class BranchNodeContainer : NodeContainerBase
    {
        public BranchNodeContainer(ParseData parseData) : base(parseData)
        {
            Node = new BranchNode<string>(parseData.Source, parseData.Value);
        }

        public override ParseResult Apply(Dictionary<int, INodeContainer> containers)
        {
            BranchNode<string> branchNode = (BranchNode<string>) Node;

            try
            {
                foreach (string argument in ParseData.Arguments)
                {
                    string[] pieces = argument.Split(':');

                    if (pieces.Length != 2) {
                        return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, "Invalid argument");
                    }

                    string value = pieces[0];
                    int index = Convert.ToInt32(pieces[1]);

                    if (!containers.ContainsKey(index))
                    {
                        return ParseResult.Failure.Derive(ParseData.LineNumber, ParseData.Line, String.Format("Invalid index argument \"{0}\"", index));
                    }

                    branchNode.AddBranch(value, containers[index].Node);
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
