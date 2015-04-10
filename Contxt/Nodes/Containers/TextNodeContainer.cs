using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class TextNodeContainer : ValueNodeContainer
    {
        public TextNodeContainer(ParseData parseData) : base(parseData, true)
        {
            Node = new TextNode<string>(parseData.Source, parseData.Value);
        }
    }
}
