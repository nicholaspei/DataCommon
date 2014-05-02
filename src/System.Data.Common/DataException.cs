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

[assembly: TypeForwardedTo(typeof(DataException))]

#else

using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data
{
    /// <summary>
    /// Represents the exception that is thrown when errors are generated using ADO.NET components.
    /// </summary>
    public class DataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataException" /> class. This is the default constructor.
        /// </summary>
        public DataException()
            : base(Strings.DefaultDataException)
        {
            HResult = HResults.Data;
        }

        /// <summary>
        /// Initializes a new instance of the <<see cref="DataException" /> class with the specified string.
        /// </summary>
        /// <param name="message">The string to display when the exception is thrown.</param>
        public DataException(string message)
            : base(message)
        {
            HResult = HResults.Data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException" /> class using the specified string and inner
        /// exception.
        /// </summary>
        /// <remarks>
        /// You can create a new exception that catches an earlier exception. The code that handles the second
        /// exception can make use of the additional information from the earlier exception, also called an inner
        /// exception, to examine the cause of the initial error.
        /// </remarks>
        /// <param name="message">The string to display when the exception is thrown. </param>
        /// <param name="innerException">A reference to an inner exception. </param>
        public DataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        internal struct HResults
        {
            internal const int Data = -2146232032;
            internal const int DataConstraint = -2146232022;
        }
    }
}

#endif