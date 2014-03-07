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
        /// Initializes a new instance of the <see cref="DbException" /> class.
        /// </summary>
        protected DbException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException" /> class with the specified error message.
        /// </summary>
        /// <param name="message">The message to display for this exception.</param>
        protected DbException(string message)
            : base(message)
        {
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
        }

        protected DbException(string message, int errorCode)
            : base(message)
        {
            _errorCode = errorCode;
        }

        // TODO: Determine if this forwards correctly
        public int ErrorCode
        {
            get { return _errorCode; }
        }
    }
}

#endif