// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbDataReader))]

#else

using System.Collections;
using System.ComponentModel;
using System.Data.Utilities;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
    /// <summary>
    /// Reads a forward-only stream of rows from a data source.
    /// </summary>
    public abstract class DbDataReader : IDisposable, IEnumerable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="DbDataReader" /> class.
        /// </summary>
        ~DbDataReader()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating the depth of nesting for the current row.
        /// </summary>
        /// <value>
        /// The depth of nesting for the current row.
        /// </value>
        public abstract int Depth { get; }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        /// <value>
        /// The number of columns in the current row.
        /// </value>
        public abstract int FieldCount { get; }

        /// <summary>
        /// Gets a value that indicates whether this <c>DbDataReader</c> contains one or more rows.
        /// </summary>
        /// <value>
        /// <c>true</c> if the <c>DbDataReader</c> contains one or more rows; otherwise <c>false</c>.
        /// </value>
        public abstract bool HasRows { get; }

        /// <summary>
        /// Gets a value indicating whether the <c>DbDataReader</c> is closed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the <c>DbDataReader</c> is closed; otherwise <c>false</c>.
        /// </value>
        public abstract bool IsClosed { get; }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
        /// </summary>
        /// <value>
        /// The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected or
        /// the statement failed
        /// </value>
        /// <remarks>
        /// The <c>RecordsAffected</c> property is not set until all rows are read and you close the
        /// <c>DbDataReader</c>.
        /// </remarks>
        public abstract int RecordsAffected { get; }

        /// <summary>
        /// Gets the number of fields in the <c>DbDataReader</c> that are not hidden.
        /// </summary>
        /// <value>
        /// The number of fields that are not hidden.
        /// </value>
        /// <remarks>
        /// This value is used to determine how many fields in the <c>DbDataReader</c> are visible. For
        /// example, a SELECT on a partial primary key returns the remaining parts of the key as hidden fields. The
        /// hidden fields are always appended behind the visible fields.
        /// </remarks>
        public virtual int VisibleFieldCount
        {
            get { return FieldCount; }
        }

        /// <summary>
        /// Gets the value of the specified column as an instance of <see cref="Object" />.
        /// </summary>
        /// <value>
        /// The value of the specified column.
        /// </value>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        public abstract object this[int ordinal] { get; }

        /// <summary>
        /// Gets the value of the specified column as an instance of <see cref="Object" />.
        /// </summary>
        /// <value>
        /// The value of the specified column.
        /// </value>
        /// <param name="name">The name of the column.</param>
        public abstract object this[string name] { get; }

        /// <summary>
        /// Closes the <c>DbDataReader</c> object.
        /// </summary>
        public virtual void Close()
        {
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <c>DbDataReader</c> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases the unmanaged and optionally the managed resources used by the current instance of the
        /// <c>DbDataReader</c> class.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            Close();
        }

        /// <summary>
        /// Gets name of the data type of the specified column.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>A string representing the name of the data type.</returns>
        public abstract string GetDataTypeName(int ordinal);

        /// <summary>
        /// Returns an <see cref="IEnumerator" /> that can be used to iterate through the rows in the data reader.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract IEnumerator GetEnumerator();

        /// <summary>
        /// Gets the data type of the specified column.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The data type of the specified column.</returns>
        public abstract Type GetFieldType(int ordinal);

        /// <summary>
        /// Gets the name of the column, given the zero-based column ordinal.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The name of the specified column.</returns>
        public abstract string GetName(int ordinal);

        /// <summary>
        /// Gets the column ordinal given the name of the column.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <returns>The zero-based column ordinal.</returns>
        public abstract int GetOrdinal(string name);

        /// <summary>
        /// Returns a <see cref="DataTable" /> that describes the column metadata of the <c>DbDataReader</c>.
        /// </summary>
        /// <returns> A <see cref="DataTable" /> that describes the column metadata.</returns>
        public virtual DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the value of the specified column as a Boolean.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract bool GetBoolean(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a byte.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract byte GetByte(int ordinal);

        /// <summary>
        /// Reads a stream of bytes from the specified column, starting at location indicated by
        /// <paramref name="dataOffset" />, into the buffer, starting at the location indicated by
        /// <paramref name="bufferOffset" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
        /// <param name="buffer">The buffer into which to copy the data.</param>
        /// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
        /// <param name="length">The maximum number of characters to read.</param>
        /// <returns></returns>
        public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);

        /// <summary>
        /// Gets the value of the specified column as a single character.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract char GetChar(int ordinal);

        /// <summary>
        /// Reads a stream of characters from the specified column, starting at location indicated by
        /// <paramref name="dataOffset" />, into the buffer, starting at the location indicated by
        /// <paramref name="bufferOffset" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
        /// <param name="buffer">The buffer into which to copy the data.</param>
        /// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
        /// <param name="length">The maximum number of characters to read.</param>
        /// <returns></returns>
        public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

        /// <summary>
        /// Returns a <c>DbDataReader</c> object for the requested column ordinal.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>A <c>DbDataReader</c> object.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DbDataReader GetData(int ordinal)
        {
            return GetDbDataReader(ordinal);
        }

        /// <summary>
        /// Returns a <c>DbDataReader</c> object for the requested column ordinal that can be overridden with a
        /// provider-specific implementation.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>A <c>DbDataReader</c> object.</returns>
        protected virtual DbDataReader GetDbDataReader(int ordinal)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="DateTime" /> object.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract DateTime GetDateTime(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a <see cref="Decimal" /> object.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract Decimal GetDecimal(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a double-precision floating point number.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract double GetDouble(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a single-precision floating point number.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract float GetFloat(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a globally-unique identifier (GUID).
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract Guid GetGuid(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a 16-bit signed integer.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract short GetInt16(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a 32-bit signed integer.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract int GetInt32(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as a 64-bit signed integer.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract long GetInt64(int ordinal);

        /// <summary>
        /// Returns the provider-specific field type of the specified column
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The <see cref="Type" /> object that describes the data type of the specified column.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Type GetProviderSpecificFieldType(int ordinal)
        {
            return GetFieldType(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as an instance of <see cref="Object" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        /// <remarks>
        /// To determine the specific type of the object returned, use
        /// <see cref="GetProviderSpecificFieldType" />.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual object GetProviderSpecificValue(int ordinal)
        {
            return GetValue(ordinal);
        }

        /// <summary>
        /// Gets all provider-specific attribute columns in the collection for the current row.
        /// </summary>
        /// <param name="values">An array into which to copy the attribute columns.</param>
        /// <returns>The number of elements copied to the array.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual int GetProviderSpecificValues(object[] values)
        {
            return GetValues(values);
        }

        /// <summary>
        /// Gets the value of the specified column as an instance of <see cref="String" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract string GetString(int ordinal);

        /// <summary>
        /// Retrieves data as a <see cref="Stream" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column as a <see cref="Stream" />.</returns>
        public virtual Stream GetStream(int ordinal)
        {
            using (var memoryStream = new MemoryStream())
            {
                var dataOffset = 0L;
                var buffer = new byte[4096];
                long bytes;
                do
                {
                    bytes = GetBytes(ordinal, dataOffset, buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, (int)bytes);
                    dataOffset += bytes;
                } while (bytes > 0);
                return new MemoryStream(memoryStream.ToArray(), false);
            }
        }

        /// <summary>
        /// Retrieves data as a <see cref="TextReader" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        /// <remarks>
        /// <c>GetTextReader</c> only supports the retrieval of values that can be converted to character
        /// arrays (strings).
        /// </remarks>
        public virtual TextReader GetTextReader(int ordinal)
        {
            if (IsDBNull(ordinal))
            {
                return new StringReader(string.Empty);
            }
            else
            {
                return new StringReader(GetString(ordinal));
            }
        }

        /// <summary>
        /// Gets the value of the specified column as an instance of <see cref="Object" />.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The value of the specified column.</returns>
        public abstract object GetValue(int ordinal);

        /// <summary>
        /// Gets the value of the specified column as an instance of <paramref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="ordinal">The column to be retrieved.</param>
        /// <returns>The value of the specified column.</returns>
        public virtual T GetFieldValue<T>(int ordinal)
        {
            return (T)GetValue(ordinal);
        }

        /// <summary>
        /// Asynchronously gets the value of the specified column as an instance of <paramref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="ordinal">The column to be retrieved.</param>
        /// <returns>The value of the specified column.</returns>
        public Task<T> GetFieldValueAsync<T>(int ordinal)
        {
            return GetFieldValueAsync<T>(ordinal, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously gets the value of the specified column as an instance of <paramref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="ordinal">The column to be retrieved.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The value of the specified column.</returns>
        public virtual Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return TaskHelper.FromCancellation<T>();
            }
            try
            {
                return Task.FromResult(GetFieldValue<T>(ordinal));
            }
            catch (Exception ex)
            {
                return TaskHelper.FromException<T>(ex);
            }
        }

        /// <summary>
        /// Populates an array of objects with the column values of the current row.
        /// </summary>
        /// <param name="values">An array into which to copy the attribute columns.</param>
        /// <returns>The number of objects copied to the array.</returns>
        public abstract int GetValues(object[] values);

        /// <summary>
        /// Gets a value that indicates whether the column contains a non-existent or missing value.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>
        /// <c>true</c> if the specified column is equivalent to <see cref="DBNull" />; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Call this method to check for null column values before calling the value type get methods (for
        /// example, <c>GetByte</c>, <c>GetChar</c>, and so on) to avoid raising an error.
        /// </remarks>
        public abstract bool IsDBNull(int ordinal);

        /// <summary>
        /// An asynchronous version of <see cref="IsDBNull" />, which gets a value that indicates whether the column
        /// contains a non-existent or missing value.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>
        /// <c>true</c> if the specified column is equivalent to <see cref="DBNull" />; otherwise <c>false</c>.
        /// </returns>
        public Task<bool> IsDBNullAsync(int ordinal)
        {
            return IsDBNullAsync(ordinal, CancellationToken.None);
        }

        /// <summary>
        /// An asynchronous version of <see cref="IsDBNull" />, which gets a value that indicates whether the column
        /// contains a non-existent or missing value.
        /// </summary>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// <c>true</c> if the specified column is equivalent to <see cref="DBNull" />; otherwise <c>false</c>.
        /// </returns>
        public virtual Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return TaskHelper.FromCancellation<bool>();
            }
            try
            {
                return Task.FromResult(IsDBNull(ordinal));
            }
            catch (Exception ex)
            {
                return TaskHelper.FromException<bool>(ex);
            }
        }

        /// <summary>
        /// Advances the reader to the next result when reading the results of a batch of statements.
        /// </summary>
        /// <returns><c>true</c> if there are more result sets; otherwise <c>false</c>.</returns>
        /// <remarks>
        /// This method allows you to process multiple result sets returned when a batch is submitted to the
        /// data provider.
        /// </remarks>
        public abstract bool NextResult();

        /// <summary>
        /// An asynchronous version of <see cref="NextResult" />, which advances the reader to the next result when
        /// reading the results of a batch of statements.
        /// Invokes <see cref="NextResultAsync(CancellationToken)" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<bool> NextResultAsync()
        {
            return NextResultAsync(CancellationToken.None);
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="NextResult" />. Providers should override with an
        /// appropriate implementation.
        /// Other methods and properties of the <c>DbDataReader</c> object should not be invoked while the returned
        /// <c>Task</c> is not yet completed.
        /// </summary>
        /// <remarks>
        /// The default implementation invokes the synchronous <see cref="NextResult" /> method and returns a
        /// completed task, blocking the calling thread. The default implementation will return a cancelled task if
        /// passed an already cancelled <c>CancellationToken</c>. Exceptions thrown by <see cref="NextResult" /> will
        /// be communicated via the returned <c>Task</c> <c>Exception</c> property.
        /// </remarks>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                TaskHelper.FromCancellation<bool>();
            }
            try
            {
                return Task.FromResult(NextResult());
            }
            catch (Exception ex)
            {
                return TaskHelper.FromException<bool>(ex);
            }
        }

        /// <summary>
        /// Advances the reader to the next record in a result set.
        /// </summary>
        /// <remarks>
        /// The default position of a data reader is before the first record. Therefore, you must call Read to
        /// begin accessing data.
        /// </remarks>
        /// <returns><c>true</c> if there are more rows; otherwise <c>false</c>.</returns>
        public abstract bool Read();

        /// <summary>
        /// An asynchronous version of <see cref="Read" />, which advances the reader to the next record in a result
        /// set.
        /// This method invokes <see cref="ReadAsync(CancellationToken)" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <returns><c>true</c> if there are more rows; otherwise <c>false</c>.</returns>
        public Task<bool> ReadAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="Read" />. Providers should override with an appropriate
        /// implementation.
        /// Other methods and properties of the <c>DbDataReader</c> object should not be invoked while the returned
        /// <c>Task</c> is not yet completed.
        /// </summary>
        /// <remarks>
        /// The default implementation invokes the synchronous <see cref="Read" /> method and returns a
        /// completed task, blocking the calling thread. The default implementation will return a cancelled task if
        /// passed an already cancelled <c>CancellationToken</c>. Exceptions thrown by <see cref="Read" /> will be
        /// communicated via the returned <c>Task</c> <c>Exception</c> property.
        /// </remarks>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns><c>true</c> if there are more rows; otherwise <c>false</c>.</returns>
        public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                TaskHelper.FromCancellation<bool>();
            }
            try
            {
                return Task.FromResult(Read());
            }
            catch (Exception ex)
            {
                return TaskHelper.FromException<bool>(ex);
            }
        }
    }
}

#endif