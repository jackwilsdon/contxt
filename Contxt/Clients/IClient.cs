using Contxt.Nodes;

namespace Contxt.Clients
{
    /// <summary>
    /// Receiver for any actions performed by a node.
    /// </summary>
    /// <typeparam name="T">Type of node value to be output.</typeparam>
    public interface IClient<T> where T : class
    {
        /// <summary>
        /// Gets the value associated with the provided key.
        /// <para>Returns <paramref name="defaultValue"/> if the key is not found.</para>
        /// </summary>
        /// <typeparam name="I">Type of the value to return.</typeparam>
        /// <param name="key">Key of the value to get.</param>
        /// <param name="defaultValue">Default value to be returned if the key is not found.</param>
        I Get<I>(string key, I defaultValue);

        /// <summary>
        /// Sets the value associated with the provided key.
        /// </summary>
        /// <param name="key">Key of the value to set.</param>
        /// <param name="value">Value to associate with the key.</param>
        void Set(string key, object value);

        /// <summary>
        /// Outputs a node to the user.
        /// </summary>
        /// <param name="node">Node to output.</param>
        void Text(INode<T> node);

        /// <summary>
        /// Outputs a choice to the user.
        /// </summary>
        /// <param name="node">Node to output.</param>
        /// <param name="choices">Choices that the user can select from.</param>
        /// <returns>The node selected by the user.</returns>
        INode<T> Choice(INode<T> node, INode<T>[] choices);
    }
}
