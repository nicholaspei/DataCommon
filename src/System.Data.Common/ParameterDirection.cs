// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(ParameterDirection))]

#else

namespace System.Data.Common
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