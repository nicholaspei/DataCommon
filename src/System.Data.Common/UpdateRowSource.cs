// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(UpdateRowSource))]

#else

namespace System.Data
{
    /// <summary>
    /// Specifies how query command results are applied to the row being updated.
    /// </summary>
    public enum UpdateRowSource
    {
    }
}

#endif
