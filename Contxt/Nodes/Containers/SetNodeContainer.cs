using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class SetNodeContainer : ValueNodeContainer
    {
        public SetNodeContainer(ParseData parseData) : base(parseData, true)
        {
            Node = new SetNode<string>(parseData.Source, parseData.Value);
        }
    }
}
