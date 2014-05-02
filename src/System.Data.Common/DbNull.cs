// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

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