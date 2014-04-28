// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET451

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(StateChangeEventArgs))]

#else

namespace System.Data
{
    /// <summary>
    /// Provides data for the state change event of a .NET Framework data provider.
    /// </summary>
    public sealed class StateChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangeEventArgs" /> class, when given the original state
        /// and the current state of the object.
        /// </summary>
        /// <param name="originalState">One of the <see cref="ConnectionState" /> values.</param>
        /// <param name="currentState">One of the <see cref="ConnectionState" /> values.</param>
        public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
        {
            OriginalState = originalState;
            CurrentState = currentState;
        }

        /// <summary>
        /// Gets the original state of the connection.
        /// </summary>
        /// <value>One of the <see cref="ConnectionState" /> values.</value>
        public ConnectionState OriginalState { get; private set; }

        /// <summary>
        /// Gets the new state of the connection. The connection object will be in the new state already when the event
        /// is fired.
        /// </summary>
        /// <value>One of the <see cref="ConnectionState" /> values.</value>
        public ConnectionState CurrentState { get; private set; }
    }
}

#endif
