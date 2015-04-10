namespace Contxt.Nodes
{
    public class SetNode<T> : ValueNode<T> where T : class
    {
        public string Key
        {
            get
            {
                return Source;
            }
        }

        public SetNode(string key, T value, INode<T> childNode = null) : base(key, value)
        { }

        public override INode<T> Execute(IClient<T> client)
        {
            client.Set(Key, Value);

            return base.Execute(client);
        }
    }
}
