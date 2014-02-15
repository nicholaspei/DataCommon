// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbCommand))]

#else

namespace System.Data.Common
{
    public abstract class DbCommand : IDisposable
    {
        ~DbCommand()
        {
            Dispose(false);
        }

        public DbConnection Connection { get; set; }
        public DbTransaction Transaction { get; set; }
        public abstract string CommandText { get; set; }
        public abstract int CommandTimeout { get; set; }
        public abstract CommandType CommandType { get; set; }
        public abstract bool DesignTimeVisible { get; set; }
        public abstract UpdateRowSource UpdatedRowSource { get; set; }
        protected abstract DbConnection DbConnection { get; set; }
        protected abstract DbParameterCollection DbParameterCollection { get; }
        protected abstract DbTransaction DbTransaction { get; set; }

        public abstract void Cancel();
        public abstract int ExecuteNonQuery();
        public abstract object ExecuteScalar();
        public abstract void Prepare();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected abstract DbParameter CreateDbParameter();
        protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);
    }
}

#endif
