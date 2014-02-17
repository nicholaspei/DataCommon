// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbParameter))]

#else

namespace System.Data.Common
{
    /// <summary>
    /// Represents a parameter to a <see cref="DbCommand" /> and optionally, its mapping to a <see cref="DataSet" />
    /// column. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).
    /// </summary>
    public abstract class DbParameter
    {
    }
}

#endif
