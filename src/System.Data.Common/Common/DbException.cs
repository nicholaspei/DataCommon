// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbException))]

#else

namespace System.Data.Common
{
    public abstract class DbException : Exception
    {
        private readonly int _errorCode;

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
