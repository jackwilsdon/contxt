using System;
using System.Collections.Generic;

namespace Contxt
{
    /// <summary>
    /// Set of generic properties.
    /// </summary>
    public class PropertySet
    {
        /// <summary>
        /// The dictionary that holds all data in the property set.
        /// </summary>
        private Dictionary<string, object> data = new Dictionary<string, object>();

        /// <summary>
        /// Returns whether or not the property set contains the provided key.
        /// </summary>
        /// <param name="key">The key to test the presence of.</param>
        /// <returns><b>true</b> if the key is present, otherwise <b>false</b>.</returns>
        public bool HasProperty(string key)
        {
            return data.ContainsKey(key);
        }

        /// <summary>
        /// Gets the value associated with the provided key.
        /// <para>Returns <paramref name="defaultValue"/> if the key is not found.</para>
        /// </summary>
        /// <typeparam name="T">Type of the value to return.</typeparam>
        /// <param name="key">Key of the value to get.</param>
        /// <param name="defaultValue">Default value to be returned if the key is not found.</param>
        public T GetProperty<T>(string key, T defaultValue)
        {
            // If the key isn't present, return the default value.
            if (!HasProperty(key)) {
                return defaultValue;
            }

            object value = data[key];

            // Retrieve the type of the value to return and the type of
            // the value associated with the key.
            Type genericType = typeof(T);
            Type valueType = value.GetType();

            // If the value type cannot be assigned from the generic type,
            // throw an exception.
            if (!valueType.IsAssignableFrom(genericType))
            {
                throw new IncorrectTypeException(genericType, valueType);
            }

            // Cast the value to the generic type and return it.
            return (T) value;
        }

        /// <summary>
        /// Sets the value associated with the provided key.
        /// </summary>
        /// <param name="key">Key of the value to set.</param>
        /// <param name="value">Value to associate with the key.</param>
        public void SetProperty(string key, object value)
        {
            data[key] = value;
        }
    }
}
