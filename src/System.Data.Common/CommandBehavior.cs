// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(CommandBehavior))]

#else

namespace System.Data
{
    /// <summary>
    /// Provides a description of the results of the query and its effect on the database.
    /// </summary>
    public enum CommandBehavior
    {
    }
}

#endif
