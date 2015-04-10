using System;
using System.Collections.Generic;

using Contxt.Clients;
using Contxt.Nodes;

namespace Contxt.Nodes
{
    /// <summary>
    /// A node in the directed graph.
    /// </summary>
    /// <typeparam name="T">The type of value in the node.</typeparam>
    public abstract class NodeBase<T> : INode<T> where T : class
    {
        /// <summary>
        /// The unique identifier of the node.
        /// </summary>
        public Guid Identifier { get; private set; }

        /// <summary>
        /// The source of the node.
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// The value of the node.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Create a new node instance.
        ///
        /// <para>The node's <see cref="Identifier"/> is generated using <see cref="Guid.NewGuid()"/>.</para>
        /// </summary>
        /// <param name="source">The node's source.</param>
        /// <param name="value">The node's value.</param>
        public NodeBase(string source, T value)
        {
            Identifier = Guid.NewGuid();
            Source = source;
            Value = value;
        }

        /// <summary>
        /// Create a new node instance with no source.
        /// </summary>
        /// <param name="value"></param>
        /// <seealso cref="NodeBase(string, T)"/>
        public NodeBase(T value) : this(null, value)
        { }

        /// <summary>
        /// Create a new node instance with no source and no value.
        /// </summary>
        /// <seealso cref="NodeBase(T)"/>
        public NodeBase() : this(null)
        { }

        /// <summary>
        /// Gets the next node in the directed graph.
        /// </summary>
        /// <param name="client">The client to use when executing.</param>
        /// <returns>The next node in the directed graph or <b>null</b> if this is the last node.</returns>
        public abstract INode<T> Execute(IClient<T> client);
    }
}
