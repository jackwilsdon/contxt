using System.Collections.Generic;

namespace Contxt.Nodes
{
    public class BranchNode<T> : NodeBase<T> where T : class
    {
        private Dictionary<T, INode<T>> branches = new Dictionary<T, INode<T>>();

        public string Key
        {
            get
            {
                return Source;
            }
        }

        public BranchNode(string key, T defaultValue) : base(key, defaultValue)
        { }

        public void AddBranch(T value, INode<T> choice)
        {
            branches.Add(value, choice);
        }
    
        public override INode<T> Execute(IClient<T> client)
        {
            T value = client.Get(Key, Value);

            INode<T> node;

            branches.TryGetValue(value, out node);

            return node;
        }
    }
}