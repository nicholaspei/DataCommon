// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(StateChangeEventHandler))]

#else

namespace System.Data
{
    /// <summary>
    /// Represents the method that will handle the <see cref="DbConnection.StateChange" /> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="StateChangeEventArgs" /> that contains the event data.</param>
    public delegate void StateChangeEventHandler(object sender, StateChangeEventArgs e);
}

#endif
