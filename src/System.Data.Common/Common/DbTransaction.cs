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

[assembly: TypeForwardedTo(typeof(DbTransaction))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// The base class for a transaction.
    /// </summary>
    public abstract class DbTransaction : IDisposable
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="DbTransaction" /> class.
        /// </summary>
        ~DbTransaction()
        {
            Dispose(false);
        }

        /// <summary>
        /// Specifies the <see cref="IsolationLevel" /> for this transaction.
        /// </summary>
        /// <value>The <see cref="IsolationLevel" /> for this transaction.</value>
        public abstract IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Specifies the <see cref="DbConnection" /> object associated with the transaction.
        /// </summary>
        /// <value>The <see cref="DbConnection" /> object associated with the transaction.</value>
        public DbConnection Connection
        {
            get { return DbConnection; }
        }

        /// <summary>
        /// Specifies the <see cref="DbConnection" /> object associated with the transaction.
        /// </summary>
        /// <value>The <see cref="DbConnection" /> object associated with the transaction.</value>
        protected abstract DbConnection DbConnection { get; }

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Rolls back a transaction from a pending state.
        /// </summary>
        public abstract void Rollback();

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="DbTransaction" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="DbTransaction" /> and optionally releases the
        /// managed resources.
        /// </summary>
        /// <param name="disposing">
        /// If true, this method releases all resources held by any managed objects that this
        /// <see cref="DbTransaction" /> references.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

#endif