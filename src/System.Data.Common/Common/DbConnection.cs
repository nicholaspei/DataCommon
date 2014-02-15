// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbConnection))]

#else

namespace System.Data.Common
{
    public abstract class DbConnection : IDisposable
    {
        ~DbConnection()
        {
            Dispose(false);
        }

        public virtual event StateChangeEventHandler StateChange;

        public abstract string ConnectionString { get; set; }
        public abstract string DataSource { get; }
        public abstract string Database { get; }
        public abstract string ServerVersion { get; }
        public abstract ConnectionState State { get; }

        public DbTransaction BeginTransaction()
        {
            return BeginDbTransaction(IsolationLevel.Unspecified);
        }

        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginDbTransaction(isolationLevel);
        }

        public DbCommand CreateCommand()
        {
            return CreateDbCommand();
        }

        public abstract void ChangeDatabase(string databaseName);
        public abstract void Close();
        public abstract void Open();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected void OnStateChange(StateChangeEventArgs stateChange)
        {
            if (StateChange != null)
            {
                StateChange(this, stateChange);
            }
        }

        protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);
        protected abstract DbCommand CreateDbCommand();
    }
}

#endif
