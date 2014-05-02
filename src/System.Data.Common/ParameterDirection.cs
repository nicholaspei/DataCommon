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

[assembly: TypeForwardedTo(typeof(ParameterDirection))]

#else

namespace System.Data
{
    /// <summary>
    /// Specifies the type of a parameter within a query relative to the DataSet.
    /// </summary>
    public enum ParameterDirection
    {
        /// <summary>
        /// The parameter is an input parameter.
        /// </summary>
        Input = 1,

        /// <summary>
        /// The parameter is an output parameter.
        /// </summary>
        Output = 2,

        /// <summary>
        /// The parameter is capable of both input and output.
        /// </summary>
        InputOutput = 3,

        /// <summary>
        /// The parameter represents a return value from an operation such as a stored procedure, built-in function, or
        /// user-defined function.
        /// </summary>
        ReturnValue = 6,
    }
}

#endif