using Contxt.Clients;

namespace Contxt.Nodes
{
    /// <summary>
    /// A node in the directed graph.
    /// </summary>
    /// <typeparam name="T">The type of value in the node.</typeparam>
    public class ValueNode<T> : NodeBase<T> where T : class
    {
        /// <summary>
        /// The child of this node.
        /// Returned by <see cref="Execute(IClient{T})"/>.
        /// </summary>
        public INode<T> Child;

        /// <summary>
        /// Create a new value node instance.
        ///
        /// <para>The node's <see cref="INode{T}.Identifier"/> is generated using <see cref="System.Guid.NewGuid()"/>.</para>
        /// </summary>
        /// <param name="source">The node's source.</param>
        /// <param name="value">The node's value.</param>
        /// <param name="childNode">The node's child.</param>
        public ValueNode(string source, T value, INode<T> childNode = null) : base(source, value)
        {
            Child = childNode;
        }

        /// <summary>
        /// Create a new value node instance with no source.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="childNode">The node's child.</param>
        /// <seealso cref="ValueNode(string, T, INode{T})"/>
        public ValueNode(T value, INode<T> childNode = null) : this(null, value)
        { }

        /// <summary>
        /// Gets the next node in the directed graph.
        /// </summary>
        /// <param name="client">The client to use when executing.</param>
        /// <returns>The next node in the directed graph or <b>null</b> if this is the last node.</returns>
        /// <seealso cref="Child"/>
        public override INode<T> Execute(IClient<T> client)
        {
            return Child;
        }
    }
}
