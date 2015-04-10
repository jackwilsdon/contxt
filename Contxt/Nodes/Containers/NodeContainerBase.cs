using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public abstract class NodeContainerBase : INodeContainer
    {
        public int Index { get; private set;  }
        public ParseData ParseData { get; private set; }
        public INode<string> Node { get; protected set; }

        public NodeContainerBase(ParseData parseData)
        {
            Index = parseData.Index;
            ParseData = parseData;
        }

        public abstract ParseResult Apply(Dictionary<int, INodeContainer> containers);
    }
}
