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

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(ConstraintException))]

#else

using System.Data.Common;

namespace System.Data
{
    /// <summary>
    /// Represents the exception that is thrown when attempting an action that violates a constraint.
    /// </summary>
    public class ConstraintException : DataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintException" /> class. This is the default
        /// constructor.
        /// </summary>
        public ConstraintException()
            : base(Strings.DefaultConstraintException)
        {
            HResult = HResults.DataConstraint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintException" /> class with the specified string.
        /// </summary>
        /// <param name="message">The string to display when the exception is thrown.</param>
        public ConstraintException(string message)
            : base(message)
        {
            HResult = HResults.DataConstraint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintException" /> class using the specified string and
        /// inner exception.
        /// </summary>
        /// <param name="message">The string to display when the exception is thrown. </param>
        /// <param name="innerException">A reference to an inner exception. </param>
        public ConstraintException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = HResults.DataConstraint;
        }
    }
}

#endif