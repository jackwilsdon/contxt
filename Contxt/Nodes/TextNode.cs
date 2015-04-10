namespace Contxt.Nodes
{
    public class TextNode<T> : ValueNode<T> where T : class
    {
        public TextNode(string source, T value, INode<T> childNode = null) : base(source, value)
        { }

        public TextNode(T value, INode<T> childNode = null) : base(value)
        { }

        public override INode<T> Execute(IClient<T> client)
        {
            client.Text(this);

            return base.Execute(client);
        }
    }
}
