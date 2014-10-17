// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbConnection))]

#else

using System.Data.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
    /// <summary>
    /// Represents a connection to a database.
    /// </summary>
    public abstract class DbConnection : IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="DbConnection" /> class.
        /// </summary>
        ~DbConnection()
        {
            Dispose(false);
        }

        /// <summary>
        /// Occurs when the state of the event changes.
        /// </summary>
        public virtual event StateChangeEventHandler StateChange;

        /// <summary>
        /// Gets or sets the string used to open the connection.
        /// </summary>
        /// <value>
        /// The connection string used to establish the initial connection. The exact contents of the connection string
        /// depend on the specific data source for this connection. The default value is an empty string.
        /// </value>
        public abstract string ConnectionString { get; set; }

        /// <summary>
        /// Gets the time to wait while establishing a connection before terminating the attempt and generating an
        /// error.
        /// </summary>
        /// <value>
        /// The time (in seconds) to wait for a connection to open. The default value is determined by the specific
        /// type of connection that you are using.
        /// </value>
        public virtual int ConnectionTimeout
        {
            get { return 15; }
        }

        /// <summary>
        /// Gets the name of the database server to which to connect.
        /// </summary>
        /// <value>The name of the database server to which to connect. The default value is an empty string.</value>
        public abstract string DataSource { get; }

        /// <summary>
        /// Gets the name of the current database after a connection is opened, or the database name specified in the
        /// connection string before the connection is opened.
        /// </summary>
        /// <value>
        /// The name of the current database or the name of the database to be used after a connection is opened. The
        /// default value is an empty string.
        /// </value>
        public abstract string Database { get; }

        /// <summary>
        /// Gets a string that represents the version of the server to which the object is connected.
        /// </summary>
        /// <value>
        /// The version of the database. The format of the string returned depends on the specific type of connection
        /// you are using.
        /// </value>
        /// <exception cref="InvalidOperationException">
        /// <see cref="ServerVersion" /> was called while the returned Task was not completed and the connection was
        /// not
        /// opened after a call to <see cref="OpenAsync" />.
        /// </exception>
        public abstract string ServerVersion { get; }

        /// <summary>
        /// Gets the state of the connection.
        /// </summary>
        /// <value> The state of the connection. </value>
        public abstract ConnectionState State { get; }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <returns>An object representing the new transaction.</returns>
        public DbTransaction BeginTransaction()
        {
            return BeginDbTransaction(IsolationLevel.Unspecified);
        }

        /// <summary>
        /// Starts a database transaction with the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginDbTransaction(isolationLevel);
        }

        /// <summary>
        /// Creates and returns a <see cref="DbCommand" /> object associated with the current connection.
        /// </summary>
        /// <returns>A <see cref="DbCommand" /> object.</returns>
        public DbCommand CreateCommand()
        {
            return CreateDbCommand();
        }

        /// <summary>
        /// Changes the current database for an open connection.
        /// </summary>
        /// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
        public abstract void ChangeDatabase(string databaseName);

        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing any open connection.
        /// </summary>
        /// <exception cref="DbException">
        /// The connection-level error that occurred while opening the connection.
        /// </exception>
        public abstract void Close();

        /// <summary>
        /// Opens a database connection with the settings specified by the <see cref="ConnectionString" />.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// An asynchronous version of <see cref="Open" />, which opens a database connection with the settings
        /// specified by the <see cref="ConnectionString" />. This method invokes
        /// <see cref="OpenAsync(CancellationToken)" /> with <c>CancellationToken.None</c>.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// After calling <see cref="OpenAsync()" />, <see cref="State" /> must return
        /// <see cref="ConnectionState.Connecting" /> until the returned Task is completed. Then, if the connection was
        /// successful, <see cref="State" /> must return <see cref="ConnectionState.Open" />. If the connection fails,
        /// <see cref="State" /> must return <see cref="ConnectionState.Closed" />.
        /// A call to <see cref="Close" /> will attempt to cancel or close the corresponding <see cref="OpenAsync()" />
        /// call.
        /// </remarks>
        public Task OpenAsync()
        {
            return OpenAsync(CancellationToken.None);
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="Open" />. Providers should override with an appropriate
        /// implementation. The cancellation token can optionally be honored.
        /// The default implementation invokes the synchronous <see cref="Open" /> call and returns a completed task.
        /// The default implementation will return a cancelled task if passed an already cancelled cancellationToken.
        /// Exceptions thrown by <see cref="Open" /> will be communicated via the returned Task Exception property.
        /// Do not invoke other methods and properties of the <c>DbConnection</c> object until the returned Task is
        /// complete.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <remarks>
        /// After calling <see cref="OpenAsync(CancellationToken)" />, <see cref="State" /> must return
        /// <see cref="ConnectionState.Connecting" /> until the returned Task is completed. Then, if the connection was
        /// successful, <see cref="State" /> must return <see cref="ConnectionState.Open" />. If the connection fails,
        /// <see cref="State" /> must return <see cref="ConnectionState.Closed" />.
        /// A call to <see cref="Close" /> will attempt to cancel or close the corresponding
        /// <see cref="OpenAsync(CancellationToken)" /> call.
        /// </remarks>
        public virtual Task OpenAsync(CancellationToken cancellationToken)
        {
            return TaskHelper.FromOperation(Open, null, cancellationToken);
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <c>DbConnection</c> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged and optionally the managed resources used by the current instance of the
        /// <c>DbConnection</c> class.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Gets the <see cref="DbProviderFactory" /> for this <see cref="DbConnection" />.
        /// </summary>
        /// <value>
        /// A set of methods for creating instances of a provider's implementation of the data source classes.
        /// </value>
        protected internal virtual DbProviderFactory DbProviderFactory
        {
            get { return null; }
        }

        /// <summary>
        /// Raises the <see cref="StateChange" /> event.
        /// </summary>
        /// <param name="stateChange">A <see cref="StateChangeEventArgs" /> that contains the event data.</param>
        protected void OnStateChange(StateChangeEventArgs stateChange)
        {
            if (StateChange != null)
            {
                StateChange(this, stateChange);
            }
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Creates and returns a <see cref="DbCommand" /> object associated with the current connection.
        /// </summary>
        /// <returns>A <see cref="DbCommand" /> object.</returns>
        protected abstract DbCommand CreateDbCommand();
    }
}

#endif