// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET451

using System;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DBNull))]

#else

namespace System
{
    /// <summary>
    /// Represents a nonexistent value. This class cannot be inherited.
    /// </summary>
    public sealed class DBNull
    {
        /// <summary>
        /// Represents the sole instance of the <see cref="DBNull" /> class.
        /// </summary>
        public static readonly DBNull Value = new DBNull();

        private DBNull()
        {
        }

        /// <summary>
        /// Returns an empty string (<see cref="String.Empty" />).
        /// </summary>
        /// <returns>An empty string (<see cref="String.Empty" />).</returns>
        public override string ToString()
        {
            return string.Empty;
        }
    }
}

#endif