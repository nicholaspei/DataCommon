// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbTransaction))]

#else

namespace System.Data.Common
{
    public abstract class DbTransaction
    {
        public abstract IsolationLevel IsolationLevel { get; }
        protected abstract DbConnection DbConnection { get; }

        public abstract void Commit();
        public abstract void Rollback();
    }
}

#endif
