// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

#if NET451

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
        /// The pending changes from more highly isolated transactions cannot be overwritten.
        /// </summary>
        Chaos = 0x10,

        /// <summary>
        /// A dirty read is possible, meaning that no shared locks are issued and no exclusive locks are honored.
        /// </summary>
        ReadUncommitted = 0x100,

        /// <summary>
        /// Shared locks are held while the data is being read to avoid dirty reads, but the data can be changed before
        /// the end of the transaction, resulting in non-repeatable reads or phantom data.
        /// </summary>
        ReadCommitted = 0x1000,

        /// <summary>
        /// Locks are placed on all data that is used in a query, preventing other users from updating the data.
        /// Prevents non-repeatable reads but phantom rows are still possible.
        /// </summary>
        RepeatableRead = 0x10000,

        /// <summary>
        /// A range lock is placed on the <see cref="DataSet" />, preventing other users from updating or inserting
        /// rows into the dataset until the transaction is complete.
        /// </summary>
        Serializable = 0x100000,

        /// <summary>
        /// Reduces blocking by storing a version of data that one application can read while another is modifying the
        /// same data. Indicates that from one transaction you cannot see changes made in other transactions, even if
        /// you requery.
        /// </summary>
        Snapshot = 0x1000000
    }
}

#endif