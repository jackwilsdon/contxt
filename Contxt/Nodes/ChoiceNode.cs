using System.Collections.Generic;

using Contxt.Clients;

namespace Contxt.Nodes
{
    public class ChoiceNode<T> : NodeBase<T> where T : class
    {
        private List<INode<T>> choices = new List<INode<T>>();

        public ChoiceNode(string source, T value, params INode<T>[] choices) : base(source, value)
        {
            this.choices.AddRange(choices);
        }

        public ChoiceNode(T value, params INode<T>[] choices) : this(null, value, choices)
        { }

        public void AddChoice(INode<T> choice)
        {
            choices.Add(choice);
        }

        public override INode<T> Execute(IClient<T> client)
        {
            return client.Choice(this, choices.ToArray());
        }
    }
}
