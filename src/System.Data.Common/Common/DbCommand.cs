// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbCommand))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for
    /// database-specific classes that represent commands.
    /// </summary>
    public abstract class DbCommand : IDisposable
    {
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
        /// is a null reference (Nothing in Visual Basic).
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
        /// <value>One of the <see cref="CommandType" /> values. The default is Text.</value>
        public abstract CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the command object should be visible in a customized interface
        /// control.
        /// </summary>
        /// <value>
        /// true, if the command object should be visible in a control; otherwise false. The default is true.
        /// </value>
        public abstract bool DesignTimeVisible { get; set; }

        /// <summary>
        /// Gets or sets how command results are applied to the <see cref="DataRow" /> when used by the Update method of
        /// a <see cref="DbDataAdapter" />.
        /// </summary>
        public abstract UpdateRowSource UpdatedRowSource { get; set; }

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
        /// is a null reference (Nothing in Visual Basic).
        /// </value>
        protected abstract DbTransaction DbTransaction { get; set; }

        /// <summary>
        /// Attempts to cancels the execution of a <see cref="DbCommand" />.
        /// </summary>
        public abstract void Cancel();

        /// <summary>
        /// Executes a SQL statement against a connection object.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public abstract int ExecuteNonQuery();

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// All other columns and rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set.</returns>
        public abstract object ExecuteScalar();

        /// <summary>
        /// Creates a prepared (or compiled) version of the command on the data source.
        /// </summary>
        public abstract void Prepare();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
    }
}

#endif
