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