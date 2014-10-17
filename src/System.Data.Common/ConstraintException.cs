// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451 || ASPNET50

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(ConstraintException))]

#else

using Microsoft.Data.Common;

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