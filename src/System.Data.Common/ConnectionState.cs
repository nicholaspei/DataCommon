// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(ConnectionState))]

#else

namespace System.Data
{
    /// <summary>
    /// Describes the current state of the connection to a data source.
    /// </summary>
    public enum ConnectionState
    {
        /// <summary>
        /// The connection is closed.
        /// </summary>
        Closed,

        /// <summary>
        /// The connection is open.
        /// </summary>
        Open,

        /// <summary>
        /// The connection object is connecting to the data source.
        /// </summary>
        Connecting = 2
    }
}

#endif