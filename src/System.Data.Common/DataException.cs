// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DataException))]

#else

using System.Runtime.InteropServices;
using Microsoft.Data.Common;

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