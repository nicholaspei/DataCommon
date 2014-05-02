// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

#if NET451

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbConnectionStringBuilder))]

#else

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Utilities;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Common
{
    /// <summary>
    /// Provides a base class for strongly typed connection string builders.
    /// </summary>
    public class DbConnectionStringBuilder : IDictionary
    {
        private IDictionary<string, object> _currentValues;
        private string _connectionString = string.Empty;
        private bool _browsableConnectionString = true;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ConnectionString"/> property is visible
        /// in Visual Studio designers.
        /// </summary>
        /// <value>
        /// <c>true</c> if the connection string is visible within designers; <c>false</c> otherwise.
        /// The default is <c>true</c>.
        /// </value>
        /// <remarks>
        /// Developers creating designers that take advantage of the <see cref="DbConnectionStringBuilder"/>
        /// class must be able to make the connection string visible or invisible within the designer's property grid.
        /// The <see cref="BrowsableConnectionString"/> property lets developers indicate that the property should be
        /// invisible by setting the property to <c>false</c>.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool BrowsableConnectionString
        {
            get { return _browsableConnectionString; }
            set { _browsableConnectionString = value; }
        }

        private IDictionary<string, object> CurrentValues
        {
            get
            {
                if (_currentValues == null)
                {
                    _currentValues = new Dictionary<string, object>();
                }

                return _currentValues;
            }
        }

        /// <summary>
        /// Gets or sets the connection string associated with the <see cref="DbConnectionStringBuilder" />.
        /// </summary>
        /// <value>
        /// The current connection string, created from the key/value pairs that are contained within the
        /// <see cref="DbConnectionStringBuilder" />. The default value is an empty string.
        /// </value>
        /// <exception cref="ArgumentException">An invalid connection string argument has been supplied.</exception>
        public string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    var builder = new StringBuilder();

                    foreach (string keyword in Keys)
                    {
                        object value;
                        if (!ShouldSerialize(keyword) || !TryGetValue(keyword, out value) || value == null)
                        {
                            continue;
                        }

                        AppendKeyValuePair(
                            builder,
                            keyword,
                            value != null ? Convert.ToString(value, CultureInfo.InvariantCulture) : null);
                    }

                    _connectionString = builder.ToString();
                }

                return _connectionString;
            }
            set
            {
                var options = new DbConnectionOptions(value);
                var originalConnectionString = ConnectionString;
                Clear();

                try
                {
                    for (var option = options.KeyChain; option != null; option = option.Next)
                    {
                        if (option.Value == null)
                        {
                            Remove(option.Name);
                        }
                        else
                        {
                            this[option.Name] = option.Value;
                        }
                    }

                    _connectionString = null;
                }
                catch (ArgumentException)
                {
                    ConnectionString = originalConnectionString;
                    _connectionString = originalConnectionString;
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the current number of keys that are contained within the <see cref="ConnectionString" /> property.
        /// </summary>
        /// <value>
        /// The number of keys that are contained within the connection string maintained by the
        /// <see cref="DbConnectionStringBuilder" /> instance.
        /// </value>
        public virtual int Count
        {
            get { return CurrentValues.Count; }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="DbConnectionStringBuilder" /> has a fixed size.
        /// </summary>
        /// <value>true if the <see cref="DbConnectionStringBuilder" /> has a fixed size; otherwise false.</value>
        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="DbConnectionStringBuilder" /> is read-only.
        /// </summary>
        /// <value>
        /// true if the <see cref="DbConnectionStringBuilder" /> is read-only; otherwise false. The default is false.
        /// </value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an <see cref="ICollection" /> that contains the keys in the <see cref="DbConnectionStringBuilder" />.
        /// </summary>
        /// <value>
        /// An <see cref="ICollection" /> that contains the keys in the <see cref="DbConnectionStringBuilder" />.
        /// </value>
        public virtual ICollection Keys
        {
            get { return ((IDictionary)CurrentValues).Keys; }
        }

        /// <summary>
        /// Gets an <see cref="ICollection" /> that contains the values in the <see cref="DbConnectionStringBuilder" />.
        /// </summary>
        /// <value>
        /// An <see cref="ICollection" /> that contains the values in the <see cref="DbConnectionStringBuilder" />.
        /// </value>
        public virtual ICollection Values
        {
            get
            {
                return new ReadOnlyCollection<object>(Keys.Cast<string>().Select(keyword => this[keyword]).ToArray());
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="ICollection" /> is synchronized (thread safe).
        /// </summary>
        /// <value>
        /// true if access to the <see cref="ICollection" /> is synchronized (thread safe); otherwise, false.
        /// </value>
        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)CurrentValues).IsSynchronized; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ICollection" />.
        /// </summary>
        /// <value>An object that can be used to synchronize access to the <see cref="ICollection" />.</value>
        object ICollection.SyncRoot
        {
            get { return ((ICollection)CurrentValues).SyncRoot; }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="keyword">The key of the item to get or set.</param>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, trying to get it returns a
        /// null reference (Nothing in Visual Basic), and trying to set it creates a new element using the specified
        /// key.Passing a null (Nothing in Visual Basic) key throws an <see cref="ArgumentNullException" />. Assigning
        /// a null value removes the key/value pair.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The property is set, and the <see cref="DbConnectionStringBuilder" /> is read-only. -or-The property is set,
        /// <paramref name="keyword" /> does not exist in the collection, and the
        /// <see cref="DbConnectionStringBuilder" /> has a fixed size.
        /// </exception>
        public virtual object this[string keyword]
        {
            get
            {
                Check.NotEmpty(keyword, "keyword");

                object value;
                if (!CurrentValues.TryGetValue(keyword, out value))
                {
                    throw new ArgumentException(Strings.FormatKeywordNotSupported(keyword));
                }

                return value;
            }
            set
            {
                Check.NotEmpty(keyword, "keyword");

                if (value == null)
                {
                    Remove(keyword);
                }
                else
                {
                    CurrentValues[keyword] = value;
                }

                _connectionString = null;
            }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="keyword">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        object IDictionary.this[object keyword]
        {
            get { return this[(string)keyword]; }
            set { this[(string)keyword] = value; }
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IDictionary" /> object.
        /// </summary>
        /// <param name="keyword">The <see cref="Object" /> to use as the key of the element to add.</param>
        /// <param name="value">The <see cref="Object" /> to use as the value of the element to add.</param>
        void IDictionary.Add(object keyword, object value)
        {
            this[(string)keyword] = value;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IDictionary"/> object has a fixed size. 
        /// </summary>
        /// <value>
        /// <c>true</c> if the IDictionary object has a fixed size; otherwise, <c>false</c>.
        /// </value>
        bool IDictionary.IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IDictionary"/> object is read-only. 
        /// </summary>
        /// <value>
        /// <c>true</c>  if the <see cref="IDictionary"/> object is read-only; otherwise, <c>false</c>.
        /// </value>
        bool IDictionary.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Adds an entry with the specified key and value into the <see cref="DbConnectionStringBuilder"/>.
        /// </summary>
        /// <param name="keyword">The key to add to the <see cref="DbConnectionStringBuilder"/>.</param>
        /// <param name="value">The value for the specified key.</param>
        public void Add(string keyword, object value)
        {
            this[keyword] = value;
        }

        /// <summary>
        /// Provides an efficient and safe way to append a key and value to an existing <see cref="StringBuilder" />
        /// object.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder" /> to which to add the key/value pair.</param>
        /// <param name="keyword">The key to be added.</param>
        /// <param name="value">The value for the supplied key.</param>
        public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
        {
            Check.NotNull(builder, "builder");
            Check.NotEmpty(keyword, "keyword");

            DbConnectionOptions.AppendKeyValuePair(builder, keyword, value);
        }

        /// <summary>
        /// Clears the contents of the <see cref="DbConnectionStringBuilder" /> instance.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// The <see cref="DbConnectionStringBuilder" /> is read-only.
        /// </exception>
        public virtual void Clear()
        {
            _connectionString = string.Empty;
            CurrentValues.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="IDictionary" /> object contains an element with the specified key.
        /// </summary>
        /// <param name="keyword">The key to locate in the <see cref="IDictionary" /> object.</param>
        /// <returns>
        /// true if the <see cref="IDictionary" /> contains an element with the key; otherwise, false.
        /// </returns>
        bool IDictionary.Contains(object keyword)
        {
            Check.NotNull(keyword, "keyword");

            return CurrentValues.ContainsKey((string)keyword);
        }

        /// <summary>
        /// Determines whether the <see cref="DbConnectionStringBuilder" /> contains a specific key.
        /// </summary>
        /// <param name="keyword">The key to locate in the <see cref="DbConnectionStringBuilder" />.</param>
        /// <returns>
        /// true if the <see cref="DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise
        /// false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).
        /// </exception>
        public virtual bool ContainsKey(string keyword)
        {
            Check.NotEmpty(keyword, "keyword");

            return CurrentValues.ContainsKey(keyword);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection" /> to an <see cref="Array" />, starting at a particular
        /// <see cref="Array" /> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array" /> that is the destination of the elements copied from
        /// <see cref="ICollection" />. The <see cref="Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)CurrentValues).CopyTo(array, index);
        }

        /// <summary>
        /// Returns an <see cref="IDictionaryEnumerator" /> object for the <see cref="IDictionary" /> object.
        /// </summary>
        /// <returns>An <see cref="IDictionaryEnumerator" /> object for the <see cref="IDictionary" /> object.</returns>
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)CurrentValues).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)CurrentValues).GetEnumerator();
        }

        /// <summary>
        /// Removes the entry with the specified key from the <see cref="DbConnectionStringBuilder" /> instance.
        /// </summary>
        /// <param name="keyword">
        /// The key of the key/value pair to be removed from the connection string in this
        /// <see cref="DbConnectionStringBuilder" />.
        /// </param>
        /// <returns>
        /// true if the key existed within the connection string and was removed; false if the key did not exist.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keyword" /> is null (Nothing in Visual Basic)
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The <see cref="DbConnectionStringBuilder" /> is read-only, or the <see cref="DbConnectionStringBuilder" />
        /// has a fixed size.
        /// </exception>
        public virtual bool Remove(string keyword)
        {
            Check.NotEmpty(keyword, "keyword");

            if (!CurrentValues.Remove(keyword))
            {
                return false;
            }

            _connectionString = null;

            return true;
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IDictionary" /> object.
        /// </summary>
        /// <param name="keyword">The key of the element to remove.</param>
        void IDictionary.Remove(object keyword)
        {
            Remove((string)keyword);
        }

        /// <summary>
        /// Indicates whether the specified key exists in this <see cref="DbConnectionStringBuilder" /> instance.
        /// </summary>
        /// <param name="keyword">The key to locate in the <see cref="DbConnectionStringBuilder" />.</param>
        /// <returns>
        /// true if the <see cref="DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise
        /// false.
        /// </returns>
        public virtual bool ShouldSerialize(string keyword)
        {
            Check.NotEmpty(keyword, "keyword");

            return CurrentValues.ContainsKey(keyword);
        }

        /// <summary>
        /// Retrieves a value corresponding to the supplied key from this <see cref="DbConnectionStringBuilder" />.
        /// </summary>
        /// <param name="keyword">The key of the item to retrieve.</param>
        /// <param name="value">The value corresponding to the <paramref name="key" />.</param>
        /// <returns>
        /// true if <paramref name="keyword" /> was found within the connection string, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keyword" /> contains a null value (Nothing in Visual Basic).
        /// </exception>
        public virtual bool TryGetValue(string keyword, out object value)
        {
            Check.NotEmpty(keyword, "keyword");

            return CurrentValues.TryGetValue(keyword, out value);
        }

        /// <summary>
        /// Returns the connection string associated with this <see cref="DbConnectionStringBuilder" />.
        /// </summary>
        /// <returns>The current <see cref="ConnectionString" /> property.</returns>
        public override string ToString()
        {
            return ConnectionString;
        }
    }
}

#endif