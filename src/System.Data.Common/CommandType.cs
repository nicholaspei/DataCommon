// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(CommandType))]

#else

namespace System.Data
{
    /// <summary>
    /// Specifies how a command string is interpreted.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// An SQL text command. (Default.)
        /// </summary>
        Text = 1,

        /// <summary>
        /// The name of a stored procedure.
        /// </summary>
        StoredProcedure = 4,

        /// <summary>
        /// The name of a table.
        /// </summary>
        TableDirect = 512,
    }
}

#endif