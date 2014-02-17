// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbConnection))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// Represents a connection to a database.
    /// </summary>
    public abstract class DbConnection : IDisposable
    {
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
        /// <see cref="ServerVersion" /> was called while the returned Task was not completed and the connection was not
        /// opened after a call to <see cref="OpenAsync" />.
        /// </exception>
        public abstract string ServerVersion { get; }

        // TODO: Documentation wrong
        /// <summary>
        /// Gets a string that describes the state of the connection.
        /// </summary>
        /// <value>
        /// The state of the connection. The format of the string returned depends on the specific type of connection
        /// you are using.
        /// </value>
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
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
