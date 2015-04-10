using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public interface INodeContainer
    {
        int Index { get; }
        ParseData ParseData { get; }
        INode<string> Node { get; }

        ParseResult Apply(Dictionary<int, INodeContainer> containers);
    }
}
