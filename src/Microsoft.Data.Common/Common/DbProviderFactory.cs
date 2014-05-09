// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbProviderFactory))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// Represents a set of methods for creating instances of a provider's implementation of the data source
    /// classes.
    /// </summary>
    public abstract class DbProviderFactory
    {
        /// <summary>
        /// Specifies whether the specific <see cref="DbProviderFactory" /> supports the <c>DbDataSourceEnumerator</c>
        /// class.
        /// </summary>
        /// <value>
        /// <c>true</c> if the instance of the <see cref="DbProviderFactory" /> supports the
        /// <c>DbDataSourceEnumerator</c> class; otherwise <c>false</c>.
        /// </value>
        public virtual bool CanCreateDataSourceEnumerator
        {
            get { return false; }
        }

        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="DbCommand" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="DbCommand" />.</returns>
        public virtual DbCommand CreateCommand()
        {
            return null;
        }

        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="DbConnection" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="DbConnection" />.</returns>
        public virtual DbConnection CreateConnection()
        {
            return null;
        }

        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="DbConnectionStringBuilder" />
        /// class.
        /// </summary>
        /// <returns>A new instance of <see cref="DbConnectionStringBuilder" />.</returns>
        public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return null;
        }

        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="DbParameter" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="DbParameter" />.</returns>
        public virtual DbParameter CreateParameter()
        {
            return null;
        }
    }
}

#endif