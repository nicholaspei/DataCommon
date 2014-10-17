// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(UpdateRowSource))]

#else

namespace System.Data
{
    /// <summary>
    /// Specifies how query command results are applied to the row being updated.
    /// </summary>
    public enum UpdateRowSource
    {
        /// <summary>
        /// Any returned parameters or rows are ignored.
        /// </summary>
        None = 0,

        /// <summary>
        /// Output parameters are mapped to the changed row.
        /// </summary>
        OutputParameters = 1,

        /// <summary>
        /// The data in the first returned row is mapped to the changed row.
        /// </summary>
        FirstReturnedRecord = 2,

        /// <summary>
        /// Both the output parameters and the first returned row are mapped to the changed row.
        /// </summary>
        Both = 3,
    }
}

#endif