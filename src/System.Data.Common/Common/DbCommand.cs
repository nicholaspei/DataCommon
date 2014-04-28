// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET451

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbCommand))]

#else

using System.Data.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
    /// <summary>
    /// Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for
    /// database-specific classes that represent commands.
    /// </summary>
    public abstract class DbCommand : IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="DbCommand" /> class.
        /// </summary>
        ~DbCommand()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the <see cref="DbConnection" /> used by this <see cref="DbCommand" />.
        /// </summary>
        /// <value>The connection to the data source.</value>
        public DbConnection Connection
        {
            get { return DbConnection; }
            set { DbConnection = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="DbTransaction" /> within which this <see cref="DbCommand" /> object executes.
        /// </summary>
        /// <value>
        /// The transaction within which a Command object of a .NET Framework data provider executes. The default value
        /// is a null reference (<c>Nothing</c> in Visual Basic).
        /// </value>
        public DbTransaction Transaction
        {
            get { return DbTransaction; }
            set { DbTransaction = value; }
        }

        /// <summary>
        /// Gets or sets the text command to run against the data source.
        /// </summary>
        /// <value>The text command to execute. The default value is an empty string ("").</value>
        public abstract string CommandText { get; set; }

        /// <summary>
        /// Gets or sets the wait time before terminating the attempt to execute a command and generating an error.
        /// </summary>
        /// <value>The time in seconds to wait for the command to execute.</value>
        public abstract int CommandTimeout { get; set; }

        /// <summary>
        /// Indicates or specifies how the <see cref="DbCommand.CommandText" /> property is interpreted.
        /// </summary>
        /// <value>One of the <see cref="CommandType" /> values. The default is <c>Text</c>.</value>
        public abstract CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the command object should be visible in a customized interface
        /// control.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the command object should be visible in a control; otherwise <c>false</c>.
        /// The default is <c>true</c>.
        /// </value>
        public abstract bool DesignTimeVisible { get; set; }

        /// <summary>
        /// Gets or sets how command results are applied to the in-memory row.
        /// </summary>
        /// <value>
        /// One of the <see cref="UpdateRowSource" /> values.
        /// The default is <c>Both</c> unless the command is automatically generated. Then the default is <c>None</c>.
        /// </value>
        public abstract UpdateRowSource UpdatedRowSource { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="DbParameter" /> objects.
        /// </summary>
        /// <value>The parameters of the SQL statement or stored procedure.</value>
        public DbParameterCollection Parameters
        {
            get { return DbParameterCollection; }
        }

        /// <summary>
        /// Gets or sets the <see cref="DbConnection" /> used by this <see cref="DbCommand" />.
        /// </summary>
        /// <value>The connection to the data source.</value>
        protected abstract DbConnection DbConnection { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="DbParameter" /> objects.
        /// </summary>
        /// <value>The parameters of the SQL statement or stored procedure.</value>
        protected abstract DbParameterCollection DbParameterCollection { get; }

        /// <summary>
        /// Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this
        /// <see cref="DbCommand" /> object executes.
        /// </summary>
        /// <value>
        /// The transaction within which a Command object of a .NET Framework data provider executes. The default value
        /// is a null reference (<c>Nothing</c> in Visual Basic).
        /// </value>
        protected abstract DbTransaction DbTransaction { get; set; }

        /// <summary>
        /// Attempts to cancels the execution of a <see cref="DbCommand" />.
        /// </summary>
        public abstract void Cancel();

        /// <summary>
        /// Creates a new instance of a <see cref="DbParameter" /> object.
        /// </summary>
        /// <returns>A <see cref="DbParameter" /> object.</returns>
        public DbParameter CreateParameter()
        {
            return CreateDbParameter();
        }

        /// <summary>
        /// Executes a SQL statement against a connection object.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public abstract int ExecuteNonQuery();

        /// <summary>
        /// An asynchronous version of <see cref="ExecuteNonQuery" />, which executes a SQL statement against a
        /// connection object.
        /// Invokes <see cref="ExecuteNonQueryAsync" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<int> ExecuteNonQueryAsync()
        {
            return ExecuteNonQueryAsync(CancellationToken.None);
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="ExecuteNonQuery" />. Providers should override with an
        /// appropriate implementation. The cancellation token may optionally be ignored.
        /// The default implementation invokes the synchronous <see cref="ExecuteNonQuery" /> method and returns a
        /// completed task, blocking the calling thread. The default implementation will return a cancelled task if
        /// passed an already cancelled cancellation token. Exceptions thrown by <see cref="ExecuteNonQuery" /> will be
        /// communicated via the returned Task Exception property.
        /// Do not invoke other methods and properties of the <c>DbCommand</c> object until the returned Task is
        /// complete.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            return TaskHelper.FromOperation(ExecuteNonQuery, CancelIgnoreFailure, cancellationToken);
        }

        /// <summary>
        /// Executes the <see cref="CommandText" /> against the <see cref="Connection" />,
        /// and returns a <see cref="DbDataReader" />.
        /// </summary>
        /// <returns>A <see cref="DbDataReader" /> object.</returns>
        public DbDataReader ExecuteReader()
        {
            return ExecuteDbDataReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Executes the <see cref="CommandText" /> against the <see cref="Connection" />,
        /// and returns a <see cref="DbDataReader" /> using one of the <see cref="CommandBehavior" /> values.
        /// </summary>
        /// <param name="behavior">One of the <see cref="CommandBehavior" /> values.</param>
        /// <returns>A <see cref="DbDataReader" /> object.</returns>
        public DbDataReader ExecuteReader(CommandBehavior behavior)
        {
            return ExecuteDbDataReader(behavior);
        }

        /// <summary>
        /// An asynchronous version of <see cref="ExecuteReader" />, which executes the <see cref="CommandText" />
        /// against the <see cref="Connection" /> and returns a <see cref="DbDataReader" />.
        /// Invokes <see cref="ExecuteDbDataReaderAsync" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<DbDataReader> ExecuteReaderAsync()
        {
            return ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
        }

        /// <summary>
        /// An asynchronous version of <see cref="ExecuteReader" />, which executes the <see cref="CommandText" />
        /// against the <see cref="Connection" /> and returns a <see cref="DbDataReader" />. This method propagates a
        /// notification that operations should be canceled.
        /// Invokes <see cref="ExecuteDbDataReaderAsync" />.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
        {
            return ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
        }

        /// <summary>
        /// An asynchronous version of <see cref="ExecuteReader" />, which executes the <see cref="CommandText" />
        /// against the <see cref="Connection" /> and returns a <see cref="DbDataReader" />.
        /// Invokes <see cref="ExecuteDbDataReaderAsync" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <param name="behavior">One of the <see cref="CommandBehavior" /> values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior)
        {
            return ExecuteReaderAsync(behavior, CancellationToken.None);
        }

        /// <summary>
        /// An asynchronous version of <see cref="ExecuteReader" />, which executes the <see cref="CommandText" />
        /// against the <see cref="Connection" /> and returns a <see cref="DbDataReader" />. This method propagates a
        /// notification that operations should be canceled.
        /// Invokes <see cref="ExecuteDbDataReaderAsync" />.
        /// </summary>
        /// <param name="behavior">One of the <see cref="CommandBehavior" /> values.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            return ExecuteDbDataReaderAsync(behavior, cancellationToken);
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// All other columns and rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set.</returns>
        public abstract object ExecuteScalar();

        /// <summary>
        /// An asynchronous version of <see cref="ExecuteScalar" />, which executes the query and returns the first
        /// column of the first row in the result set returned by the query. All other columns and rows are ignored.
        /// Invokes <see cref="ExecuteScalarAsync" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<object> ExecuteScalarAsync()
        {
            return ExecuteScalarAsync(CancellationToken.None);
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="ExecuteScalar" />. Providers should override with an
        /// appropriate implementation. The cancellation token may optionally be ignored.
        /// The default implementation invokes the synchronous <see cref="ExecuteScalar" /> method and returns a
        /// completed task, blocking the calling thread. The default implementation will return a cancelled task if
        /// passed an already cancelled cancellation token. Exceptions thrown by ExecuteScalar will be communicated via
        /// the returned Task Exception property.
        /// Do not invoke other methods and properties of the <c>DbCommand</c> object until the returned Task is
        /// complete.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            return TaskHelper.FromOperation<object>(ExecuteScalar, CancelIgnoreFailure, cancellationToken);
        }

        /// <summary>
        /// Creates a prepared (or compiled) version of the command on the data source.
        /// </summary>
        public abstract void Prepare();

        /// <summary>
        /// Releases all resources used by the current instance of the <c>DbCommand</c> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged and optionally the managed resources used by the current instance of the
        /// <c>DbCommand</c> class.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Creates a new instance of a <see cref="DbParameter" /> object.
        /// </summary>
        /// <returns>A <see cref="DbParameter" /> object.</returns>
        protected abstract DbParameter CreateDbParameter();

        /// <summary>
        /// Executes the command text against the connection.
        /// </summary>
        /// <param name="behavior">An instance of <see cref="CommandBehavior" />.</param>
        /// <returns>A task representing the operation.</returns>
        /// <exception cref="DbException">An error occurred while executing the command text.</exception>
        /// <exception cref="ArgumentException">An invalid <see cref="CommandBehavior" /> value.</exception>
        protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

        /// <summary>
        /// Providers should implement this method to provide a non-default implementation for
        /// <see cref="ExecuteReader" /> overloads.
        /// The default implementation invokes the synchronous <see cref="ExecuteReader" /> method and returns a
        /// completed task, blocking the calling thread. The default implementation will return a cancelled task if
        /// passed an already cancelled cancellation token. Exceptions thrown by ExecuteReader will be communicated via
        /// the returned Task Exception property.
        /// This method accepts a cancellation token that can be used to request the operation to be cancelled early.
        /// Implementations may ignore this request.
        /// </summary>
        /// <param name="behavior">Options for statement execution and data retrieval.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected virtual Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior,
            CancellationToken cancellationToken)
        {
            return TaskHelper.FromOperation(ExecuteReader, behavior, CancelIgnoreFailure, cancellationToken);
        }

        private void CancelIgnoreFailure()
        {
            try
            {
                Cancel();
            }
            catch (Exception)
            {
            }
        }
    }
}

#endif