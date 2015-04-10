using Contxt.Nodes;

namespace Contxt.Clients
{
    /// <summary>
    /// Client implementation base that uses a <see cref="Contxt.PropertySet"/> for <see cref="Get{I}(string, I)"/> and <see cref="Set(string, object)"/>.
    /// </summary>
    /// <typeparam name="T">Type of node value to be output.</typeparam>
    /// <seealso cref="Contxt.PropertySet"/>
    public abstract class ClientBase<T> : IClient<T> where T : class
    {
        /// <summary>
        /// The property set used by <see cref="Get{I}(string, I)"/> and <see cref="Set(string, object)"/>
        /// </summary>
        private PropertySet propertySet = new PropertySet();

        /// <summary>
        /// Whether or not to repeat choices if the user's choice is invalid.
        /// <para>If <b>true</b>, <see cref="DoChoice(INode{T}, INode{T}[], out INode{T})"/> will be called until it returns <b>true</b>.</para>
        /// </summary>
        public bool RepeatChoiceOnFailure = false;

        /// <summary>
        /// Create a new <see cref="Contxt.ClientBase{T}"/> instance.
        /// </summary>
        /// <param name="repeatChoiceOnFailure"> Whether or not to repeat choices if the user's choice is invalid.</param>
        /// <seealso cref="RepeatChoiceOnFailure"/>
        public ClientBase(bool repeatChoiceOnFailure = false)
        {
            RepeatChoiceOnFailure = repeatChoiceOnFailure;
        }

        /// <summary>
        /// Gets the value associated with the provided key.
        /// <para>Returns <paramref name="defaultValue"/> if the key is not found.</para>
        /// </summary>
        /// <typeparam name="I">Type of the value to return.</typeparam>
        /// <param name="key">Key of the value to get.</param>
        /// <param name="defaultValue">Default value to be returned if the key is not found.</param>
        /// <exception cref="Contxt.IncorrectTypeException">Thrown if the property in the set is not of the same type as <typeparamref name="T"/></exception>
        /// <seealso cref="Contxt.PropertySet.GetProperty{T}(string, T)"/>
        public I Get<I>(string key, I defaultValue)
        {
            return propertySet.GetProperty<I>(key, defaultValue);
        }

        /// <summary>
        /// Sets the value associated with the provided key.
        /// </summary>
        /// <param name="key">Key of the value to set.</param>
        /// <param name="value">Value to associate with the key.</param>
        /// <seealso cref="Contxt.PropertySet.SetProperty(string, object)"/>
        public void Set(string key, object value)
        {
            propertySet.SetProperty(key, value);
        }

        /// <summary>
        /// Outputs a node to the user.
        /// </summary>
        /// <param name="node">Node to output.</param>
        public abstract void Text(INode<T> node);

        /// <summary>
        /// Outputs a choice to the user.
        /// </summary>
        /// <param name="node">Node to output.</param>
        /// <param name="choices">Choices that the user can select from.</param>
        /// <returns>The node selected by the user.</returns>
        public INode<T> Choice(INode<T> node, INode<T>[] choices)
        {
            INode<T> choice;
            bool result = false;

            // Output the current node.
            Text(node);

            // Output the choice and repeat it if it fails
            // and RepeatChoiceOnFailure is true.
            do
            {
                result = DoChoice(node, choices, out choice);
            }
            while (!result && RepeatChoiceOnFailure);

            return choice;
        }

        /// <summary>
        /// Outputs a choice to the user.
        /// </summary>
        /// <param name="node">Node to output.</param>
        /// <param name="choices">Choices that the user can select from.</param>
        /// <param name="choice">The node selected by the user.</param>
        /// <returns><b>true</b> if the user has selected a valid node, otherwise <b>false</b></returns>
        public abstract bool DoChoice(INode<T> node, INode<T>[] choices, out INode<T> choice);
    }
}
