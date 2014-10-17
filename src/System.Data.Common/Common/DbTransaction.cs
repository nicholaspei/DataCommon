// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

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