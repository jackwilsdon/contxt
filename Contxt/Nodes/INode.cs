using System;

using Contxt.Clients;

namespace Contxt.Nodes
{
    /// <summary>
    /// A node in the directed graph.
    /// </summary>
    /// <typeparam name="T">The type of value in the node</typeparam>
    public interface INode<T> where T : class
    {
        /// <summary>
        /// The unique identifier of the node.
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// The source of the node.
        /// </summary>
        string Source { get; }
        T Value { get; }

        INode<T> Execute(IClient<T> client);
    }
}
