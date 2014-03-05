// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DataRowVersion))]

#else

namespace System.Data
{
    /// <summary>
    /// Describes the version of a DataRow.
    /// </summary>
    public enum DataRowVersion
    {
        /// <summary>
        /// The original
        /// </summary>
        Original = 0x0100,

        /// <summary>
        /// The row contains current values.
        /// </summary>
        Current = 0x0200,

        /// <summary>
        /// The proposed
        /// </summary>
        Proposed = 0x0400,

        /// <summary>
        /// The default version of <c>DataRowState</c>. For a <c>DataRowState</c> value of <c>Added</c>,
        /// <c>Modified</c> or <c>Deleted</c>, the default version is <c>Current</c>. For a <c>DataRowState</c> value
        /// of <c>Detached</c>, the version is <c>Proposed</c>.
        /// </summary>
        Default = Proposed | Current,
    }
}

#endif