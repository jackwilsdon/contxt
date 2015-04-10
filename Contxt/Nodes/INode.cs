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

        /// <summary>
        /// The value of the node.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Gets the next node in the directed graph.
        /// </summary>
        /// <param name="client">The client to use when executing.</param>
        /// <returns>The next node in the directed graph or <b>null</b> if this is the last node.</returns>
        INode<T> Execute(IClient<T> client);
    }
}
