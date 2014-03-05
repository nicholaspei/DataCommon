// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbParameterCollection))]

#else

using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
    /// <summary>
    /// The base class for a collection of parameters relevant to a <see cref="DbCommand" />.
    /// </summary>
    public abstract class DbParameterCollection : IList
    {
        /// <summary>
        /// Specifies the number of items in the collection.
        /// </summary>
        /// <value>
        /// The number of items in the collection.
        /// </value>
        public abstract int Count { get; }

        /// <summary>
        /// Specifies whether the collection is a fixed size.
        /// </summary>
        /// <value>
        /// <c>true</c> if the collection is a fixed size; otherwise <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Specifies whether the collection is read-only.
        /// </summary>
        /// <value>
        /// <c>true</c> if the collection is read-only; otherwise <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Specifies whether the collection is synchronized.
        /// </summary>
        /// <value>
        /// <c>true</c> if the collection is synchronized; otherwise <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Specifies whether the collection is a fixed size.
        /// </summary>
        /// <value>
        /// <c>true</c> if the collection is a fixed size; otherwise <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool IList.IsFixedSize
        {
            get
            {
                return IsFixedSize;
            }
        }

        /// <summary>
        /// Specifies whether the collection is read-only.
        /// </summary>
        /// <value>
        /// <c>true</c> if the collection is read-only; otherwise <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool IList.IsReadOnly
        {
            get
            {
                return IsReadOnly;
            }
        }

        /// <summary>
        /// Specifies whether the collection is synchronized.
        /// </summary>
        /// <value>
        /// <c>true</c> if the collection is synchronized; otherwise <c>false</c>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool ICollection.IsSynchronized
        {
            get
            {
                return IsSynchronized;
            }
        }

        /// <summary>
        /// Specifies the <see cref="Object"/> to be used to synchronize access to the collection.
        /// </summary>
        /// <value>
        /// A <see cref="Object"/> to be used to synchronize access to the <see cref="DbParameterCollection"/>.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract object SyncRoot { get; }

        /// <summary>
        /// Gets or sets the <see cref="DbParameter"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="DbParameter"/> at the specified index.
        /// </value>
        /// <param name="index">The zero-based index of the parameter.</param>
        public DbParameter this[int index]
        {
            get
            {
                return GetParameter(index);
            }
            set
            {
                SetParameter(index, value);
            }
        }
        /// <summary>
        /// Gets or sets the <see cref="DbParameter"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="DbParameter"/> with the specified name.
        /// </value>
        /// <param name="parameterName">The name of the parameter.</param>
        public DbParameter this[string parameterName]
        {
            get
            {
                return GetParameter(parameterName);
            }
            set
            {
                SetParameter(parameterName, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/> at the specified index.
        /// </value>
        /// <param name="index">The zero-based index of the parameter.</param>
        object IList.this[int index]
        {
            get
            {
                return GetParameter(index);
            }
            set
            {
                SetParameter(index, (DbParameter)value);
            }
        }

        /// <summary>
        /// Adds the specified <see cref="DbParameter"/> object to the <see cref="DbParameterCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="DbParameter.Value"/> of the <see cref="DbParameter"/> to add to the collection.</param>
        /// <returns>The index of the <see cref="DbParameter"/> object in the collection.</returns>
        public abstract int Add(object value);

        /// <summary>
        /// Adds an array of items with the specified values to the <see cref="DbParameterCollection"/>.
        /// </summary>
        /// <param name="values">An array of values of type <see cref="DbParameter"/> to add to the collection.</param>
        public abstract void AddRange(Array values);

        /// <summary>
        /// Indicates whether a <see cref="DbParameter"/> with the specified <see cref="DbParameter.Value"/> is contained in the collection.
        /// </summary>
        /// <param name="value">The <see cref="DbParameter.Value"/> of the <see cref="DbParameter"/> to look for in the collection.</param>
        /// <returns><c>true</c> if the <see cref="DbParameter"/> is in the collection; otherwise <c>false</c>.</returns>
        public abstract bool Contains(object value);

        /// <summary>
        /// Indicates whether a <see cref="DbParameter"/> with the specified name exists in the collection.
        /// </summary>
        /// <param name="value">The name of the <see cref="DbParameter"/> to look for in the collection.</param>
        /// <returns><c>true</c> if the <see cref="DbParameter"/> is in the collection; otherwise <c>false</c>.</returns>
        public abstract bool Contains(string value);

        /// <summary>
        /// Copies an array of items to the collection starting at the specified index.
        /// </summary>
        /// <param name="array">The array of items to copy to the collection.</param>
        /// <param name="index">The index in the collection to copy the items.</param>
        public abstract void CopyTo(Array array, int index);

        /// <summary>
        /// Removes all <see cref="DbParameter"/> values from the <see cref="DbParameterCollection"/>.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Returns an <see cref="IEnumerator" /> that can be used to iterate through the collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> that can be used to iterate through the collection.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract IEnumerator GetEnumerator();

        /// <summary>
        /// Returns the <see cref="DbParameter"/> object at the specified index in the collection.
        /// </summary>
        /// <param name="index">The index of the <see cref="DbParameter"/> in the collection.</param>
        /// <returns>The <see cref="DbParameter"/> object at the specified index in the collection.</returns>
        protected abstract DbParameter GetParameter(int index);

        /// <summary>
        /// Returns the <see cref="DbParameter"/> object with the specified name.
        /// </summary>
        /// <param name="parameterName">The name of the <see cref="DbParameter"/> in the collection.</param>
        /// <returns>The <see cref="DbParameter"/> the object with the specified name.</returns>
        protected abstract DbParameter GetParameter(string parameterName);

        /// <summary>
        /// Returns the index of the specified <see cref="DbParameter"/> object.
        /// </summary>
        /// <param name="value">The <see cref="DbParameter"/> object in the collection.</param>
        /// <returns>The index of the specified <see cref="DbParameter"/> object.</returns>
        public abstract int IndexOf(object value);

        /// <summary>
        /// Returns the index of the <see cref="DbParameter"/> object with the specified name.
        /// </summary>
        /// <param name="parameterName">The name of the <see cref="DbParameter"/> object in the collection.</param>
        /// <returns>The index of the <see cref="DbParameter"/> object with the specified name.</returns>
        public abstract int IndexOf(string parameterName);

        /// <summary>
        /// Inserts the specified <see cref="DbParameter"/> object into the collection at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert the <see cref="DbParameter"/> object.</param>
        /// <param name="value">The <see cref="DbParameter"/> object to insert into the collection.</param>
        public abstract void Insert(int index, object value);

        /// <summary>
        /// Removes the specified <see cref="DbParameter"/> object from the collection.
        /// </summary>
        /// <param name="value">The <see cref="DbParameter"/> object to remove.</param>
        public abstract void Remove(object value);

        /// <summary>
        /// Removes the <see cref="DbParameter"/> object at the specified from the collection.
        /// </summary>
        /// <param name="index">The index where the <see cref="DbParameter"/> object is located.</param>
        public abstract void RemoveAt(int index);

        /// <summary>
        /// Removes the <see cref="DbParameter"/> object with the specified name from the collection.
        /// </summary>
        /// <param name="parameterName">The name of the <see cref="DbParameter"/> object to remove.</param>
        public abstract void RemoveAt(string parameterName);

        /// <summary>
        /// Sets the <see cref="DbParameter"/> object at the specified index to a new value. 
        /// </summary>
        /// <param name="index">The index where the <see cref="DbParameter"/> object is located.</param>
        /// <param name="value">The new <see cref="DbParameter"/> value.</param>
        protected abstract void SetParameter(int index, DbParameter value);

        /// <summary>
        /// Sets the <see cref="DbParameter"/> object with the specified name to a new value.
        /// </summary>
        /// <param name="parameterName">The name of the <see cref="DbParameter"/> object in the collection.</param>
        /// <param name="value">The new <see cref="DbParameter"/> value.</param>
        protected abstract void SetParameter(string parameterName, DbParameter value);
    }
}

#endif
