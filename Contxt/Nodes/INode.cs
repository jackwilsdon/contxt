using System;

using Contxt.Clients;

namespace Contxt.Nodes
{
    public interface INode<T> where T : class
    {
        Guid Identifier { get; }
        string Source { get; }
        T Value { get; }

        INode<T> Execute(IClient<T> client);
    }
}
