// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(CommandBehavior))]

#else

namespace System.Data
{
    /// <summary>
    /// Provides a description of the results of the query and its effect on the database.
    /// </summary>
    [Flags]
    public enum CommandBehavior
    {
        /// <summary>
        /// The query may return multiple result sets. Execution of the query may affect the database state.
        /// <c>Default</c> sets no CommandBehavior flags, so calling <c>ExecuteReader(CommandBehavior.Default)</c>
        /// is functionally equivalent to calling <c>ExecuteReader()</c>.
        /// </summary>
        Default = 0,

        /// <summary>
        /// The query returns a single result set.
        /// </summary>
        SingleResult = 1,

        /// <summary>
        /// The query returns column information only. When using SchemaOnly, the .NET Framework Data Provider for
        /// SQL Server precedes the statement being executed with SET FMTONLY ON.
        /// </summary>
        SchemaOnly = 2,

        /// <summary>
        /// The query returns column and primary key information.
        /// When KeyInfo is used for command execution, the provider will append extra columns to the result set for
        /// existing primary key and timestamp columns.
        /// When using KeyInfo, the .NET Framework Data Provider for SQL Server precedes the statement being executed
        /// with SET FMTONLY OFF and SET NO_BROWSETABLE ON. The user should be aware of potential side effects, such as
        /// interference with the use of SET FMTONLY ON statements. See SQL Server Books Online for more information.
        /// </summary>
        KeyInfo = 4,

        /// <summary>
        /// The query is expected to return a single row of the first result set. Execution of the query may affect the
        /// database state. Some .NET Framework data providers may, but are not required to, use this information to
        /// optimize the performance of the command. If your SQL statement is expected to return only a single row,
        /// specifying SingleRow can also improve application performance. It is possible to specify <c>SingleRow</c>
        /// when executing queries that are expected to return multiple result sets. In that case, where both a
        /// multi-result set SQL query and single row are specified, the result returned will contain only the first
        /// row of the first result set. The other result sets of the query will not be returned.
        /// </summary>
        SingleRow = 8,

        /// <summary>
        /// Provides a way for the <c>DataReader</c> to handle rows that contain columns with large binary values.
        /// Rather than loading the entire row, <c>SequentialAccess</c> enables the <c>DataReader</c> to load data as a
        /// stream. You can then use the <c>GetBytes</c> or <c>GetChars</c> method to specify a byte location to start
        /// the read operation, and a limited buffer size for the data being returned.
        /// When you specify <c>SequentialAccess</c>, you are required to read from the columns in the order they are
        /// returned, although you are not required to read each column. Once you have read past a location in the
        /// returned stream of data, data at or before that location can no longer be read from the <c>DataReader</c>.
        /// </summary>
        SequentialAccess = 16,

        /// <summary>
        /// When the command is executed, the associated <c>Connection</c> object is closed when the associated
        /// <c>DataReader</c> object is closed.
        /// </summary>
        CloseConnection = 32,
    }
}

#endif