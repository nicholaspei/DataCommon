// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(IsolationLevel))]

#else

namespace System.Data
{
    public enum IsolationLevel
    {
        Unspecified = -1,
        ReadUncommitted = 0x100,
        Serializable = 0x100000
    }
}

#endif
