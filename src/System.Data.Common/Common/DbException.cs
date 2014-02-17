// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbException))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// The base class for all exceptions thrown on behalf of the data source.
    /// </summary>
    public abstract class DbException : Exception
    {
        private readonly int _errorCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException" /> class with the specified error message and
        /// error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code for the exception.</param>
        protected DbException(string message, int errorCode)
            : base(message)
        {
            _errorCode = errorCode;
        }

        public virtual int ErrorCode
        {
            get { return _errorCode; }
        }
    }
}

#endif
