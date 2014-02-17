// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbDataReader))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// Reads a forward-only stream of rows from a data source.
    /// </summary>
    public abstract class DbDataReader
    {
    }
}

#endif
