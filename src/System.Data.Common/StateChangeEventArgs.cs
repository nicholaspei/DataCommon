// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(StateChangeEventArgs))]

#else


namespace System.Data
{
    public sealed class StateChangeEventArgs : EventArgs
    {
        public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
        {
        }
    }
}

#endif
