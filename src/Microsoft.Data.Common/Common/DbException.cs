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

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbException))]

#else

using System.Globalization;
using System.Text;
using Microsoft.Data.Common;

namespace System.Data.Common
{
    /// <summary>
    /// The base class for all exceptions thrown on behalf of the data source.
    /// </summary>
    public abstract class DbException : Exception
    {
        private const int DefaultHResult = unchecked((int)0x80004005);

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException" /> class.
        /// </summary>
        protected DbException()
            : base(Strings.ExternalException)
        {
            HResult = DefaultHResult;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException" /> class with the specified error message.
        /// </summary>
        /// <param name="message">The message to display for this exception.</param>
        protected DbException(string message)
            : base(message)
        {
            HResult = DefaultHResult;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException" /> class with the specified error message and a
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message to display for this exception.</param>
        /// <param name="innerException">The inner exception reference.</param>
        protected DbException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = DefaultHResult;
        }

        /// <summary>
        /// Initializes a new instance of the ExternalException class with a specified error message and the HRESULT of
        /// the error.
        /// </summary>
        /// <param name="message">The error message that specifies the reason for the exception.</param>
        /// <param name="errorCode">The HRESULT of the error.</param>
        protected DbException(string message, int errorCode)
            : base(message)
        {
            HResult = errorCode;
        }

        /// <summary>
        /// Gets the HRESULT of the error.
        /// </summary>
        /// <value>The HRESULT of the error.</value>
        public virtual int ErrorCode
        {
            get { return HResult; }
        }

        /// <summary>
        /// Returns a string that contains the HRESULT of the error.
        /// </summary>
        /// <returns>A string that represents the HRESULT.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType())
                .Append(" (0x")
                .Append(HResult.ToString("X8", CultureInfo.InvariantCulture))
                .Append(")");

            if (!string.IsNullOrEmpty(Message))
            {
                builder.Append(": ")
                    .Append(Message);
            }

            if (InnerException != null)
            {
                builder.Append(" ---> ")
                    .Append(InnerException);
            }

            if (StackTrace != null)
            {
                builder.Append(Environment.NewLine)
                    .Append(StackTrace);
            }

            return builder.ToString();
        }
    }
}

#endif