// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbProviderFactories))]

#else

using System.Data.Utilities;
using JetBrains.Annotations;

namespace System.Data.Common
{
    /// <summary>
    /// Represents a set of static methods for creating one or more instances of <see cref="DbProviderFactory"/> classes.
    /// </summary>
    public static class DbProviderFactories
    {
        /// <summary>
        /// Returns the <see cref="DbProviderFactory"/> used by the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns>An instance of a <see cref="DbProviderFactory"/> for a specified connection.</returns>
        public static DbProviderFactory GetFactory([NotNull] DbConnection connection)
        {
            Check.NotNull(connection, "connection");

            return connection.DbProviderFactory;
        }
    }
}

#endif