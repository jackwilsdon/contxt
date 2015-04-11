using Contxt.Clients;

namespace Contxt.Nodes
{
    /// <summary>
    /// A node that outputs it's own value and returns it's child upon execution.
    /// </summary>
    /// <typeparam name="T">The type of value in the node.</typeparam>
    public class TextNode<T> : ValueNode<T> where T : class
    {
        /// <summary>
        /// Create a new text node instance.
        ///
        /// <para>The node's <see cref="Contxt.Nodes.INode{T}.Identifier"/> is generated using <see cref="System.Guid.NewGuid()"/>.</para>
        /// </summary>
        /// <param name="source">The node's source.</param>
        /// <param name="value">The node's value.</param>
        /// <param name="childNode">The node's child.</param>
        public TextNode(string source, T value, INode<T> childNode = null) : base(source, value)
        { }

        /// <summary>
        /// Create a new value node instance with no source.
        /// </summary>
        /// <param name="value">The node's value.</param>
        /// <param name="childNode">The node's child.</param>
        /// <seealso cref="TextNode(string, T, INode{T})"/>
        public TextNode(T value, INode<T> childNode = null) : base(value)
        { }

        /// <summary>
        /// Calls <see cref="Contxt.Clients.IClient{T}.Text(INode{T})"/> before returning the next node in the directed graph.
        /// </summary>
        /// <param name="client">The client to use when executing.</param>
        /// <returns>The next node in the directed graph or <b>null</b> if this is the last node.</returns>
        /// <seealso cref="Contxt.Nodes.ValueNode{T}.Child"/>
        public override INode<T> Execute(IClient<T> client)
        {
            client.Text(this);

            return base.Execute(client);
        }
    }
}
