// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

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
        Open
    }
}

#endif
