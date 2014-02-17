// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbParameterCollection))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// The base class for a collection of parameters relevant to a <see cref="DbCommand" />.
    /// </summary>
    public abstract class DbParameterCollection
    {
    }
}

#endif
