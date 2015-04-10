using System;
using System.Collections.Generic;

using Contxt.Clients;
using Contxt.Nodes;

namespace Contxt.Nodes
{
    public abstract class NodeBase<T> : INode<T> where T : class
    {
        public Guid Identifier { get; private set; }
        public string Source { get; private set; }
        public T Value { get; private set; }

        public NodeBase(string source, T value)
        {
            Identifier = Guid.NewGuid();
            Source = source;
            Value = value;
        }

        public NodeBase(T value) : this(null, value)
        { }

        public NodeBase() : this(null)
        { }

        public abstract INode<T> Execute(IClient<T> client);
    }
}
