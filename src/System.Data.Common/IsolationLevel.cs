// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(IsolationLevel))]

#else

namespace System.Data
{
    /// <summary>
    /// Specifies the transaction locking behavior for the connection.
    /// </summary>
    public enum IsolationLevel
    {
        /// <summary>
        /// A different isolation level than the one specified is being used, but the level cannot be determined.
        /// </summary>
        Unspecified = -1,

        /// <summary>
        /// A dirty read is possible, meaning that no shared locks are issued and no exclusive locks are honored.
        /// </summary>
        ReadUncommitted = 0x100,

        /// <summary>
        /// A range lock is placed on the <see cref="DataSet" />, preventing other users from updating or inserting rows
        /// into the dataset until the transaction is complete.
        /// </summary>
        Serializable = 0x100000
    }
}

#endif
