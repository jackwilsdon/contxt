using Contxt.Clients;

namespace Contxt.Nodes
{
    public class ValueNode<T> : NodeBase<T> where T : class
    {
        public INode<T> Child;

        public ValueNode(string source, T value, INode<T> childNode = null) : base(source, value)
        {
            Child = childNode;
        }

        public ValueNode(T value, INode<T> childNode = null) : this(null, value)
        { }

        public override INode<T> Execute(IClient<T> client)
        {
            return Child;
        }
    }
}
