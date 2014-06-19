// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NET451

using System.Data;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbType))]

#else

namespace System.Data
{
    /// <summary>
    /// Specifies the data type of a field, a property, or a <c>Parameter</c> object of a .NET Framework data
    /// provider.
    /// </summary>
    /// <remarks>
    /// The type of a parameter is specific to the .NET Framework data provider. Specifying the type converts the
    /// value of the <c>Parameter</c> to the data provider Type before passing the value to the data source. If the
    /// type is not specified, ADO.NET infers the data provider Type of the <c>Parameter</c> from the <c>Value</c>
    /// property of the <c>Parameter</c> object.
    /// You can also generically specify the type of a <c>Parameter</c> by setting <c>DbType</c> property of a
    /// <c>Parameter</c> object.
    /// ADO.NET cannot correctly infer the type if a byte array is larger than 8,000 bytes. Explicitly specify the
    /// DbType when working with byte arrays larger than 8,000 bytes.
    /// </remarks>
    public enum DbType
    {
        /// <summary>
        /// A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters.
        /// </summary>
        AnsiString = 0,

        /// <summary>
        /// A variable-length stream of binary data ranging between 1 and 8,000 bytes.
        /// </summary>
        Binary = 1,

        /// <summary>
        /// An 8-bit unsigned integer ranging in value from 0 to 255.
        /// </summary>
        Byte = 2,

        /// <summary>
        /// A simple type representing Boolean values of true or false.
        /// </summary>
        Boolean = 3,

        /// <summary>
        /// A currency value ranging from -2^63 (or -922,337,203,685,477.5808) to 2^63 -1 (or
        /// +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit.
        /// </summary>
        Currency = 4,

        /// <summary>
        /// A type representing a date value.
        /// </summary>
        Date = 5,

        /// <summary>
        /// A type representing a date and time value.
        /// </summary>
        DateTime = 6,

        /// <summary>
        /// A simple type representing values ranging from 1.0 x 10_-28 to approximately 7.9 x 10_28 with 28-29
        /// significant digits.
        /// </summary>
        Decimal = 7,

        /// <summary>
        /// A floating point type representing values ranging from approximately 5.0 x 10_-324 to 1.7 x 10_308 with a
        /// precision of 15-16 digits.
        /// </summary>
        Double = 8,

        /// <summary>
        /// A globally unique identifier (or GUID).
        /// </summary>
        Guid = 9,

        /// <summary>
        /// An integral type representing signed 16-bit integers with values between -32768 and 32767.
        /// </summary>
        Int16 = 10,

        /// <summary>
        /// An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.
        /// </summary>
        Int32 = 11,

        /// <summary>
        /// An integral type representing signed 64-bit integers with values between -9223372036854775808 and
        /// 9223372036854775807.
        /// </summary>
        Int64 = 12,

        /// <summary>
        /// A general type representing any reference or value type not explicitly represented by another <c>DbType</c>
        /// value.
        /// </summary>
        Object = 13,

        /// <summary>
        /// An integral type representing signed 8-bit integers with values between -128 and 127.
        /// </summary>
        SByte = 14,

        /// <summary>
        /// A floating point type representing values ranging from approximately 1.5 x 10^-45 to 3.4 x 10^38 with a
        /// precision of 7 digits.
        /// </summary>
        Single = 15,

        /// <summary>
        /// A type representing Unicode character strings.
        /// </summary>
        String = 16,

        /// <summary>
        /// A type representing a SQL Server <c>DateTime</c> value.
        /// </summary>
        Time = 17,

        /// <summary>
        /// An integral type representing unsigned 16-bit integers with values between 0 and 65535.
        /// </summary>
        UInt16 = 18,

        /// <summary>
        /// An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.
        /// </summary>
        UInt32 = 19,

        /// <summary>
        /// An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.
        /// </summary>
        UInt64 = 20,

        /// <summary>
        /// A variable-length numeric value.
        /// </summary>
        VarNumeric = 21,

        /// <summary>
        /// A fixed-length stream of non-Unicode characters.
        /// </summary>
        AnsiStringFixedLength = 22,

        /// <summary>
        /// A fixed-length string of Unicode characters.
        /// </summary>
        StringFixedLength = 23,

        /// <summary>
        /// A parsed representation of an XML document or fragment.
        /// </summary>
        Xml = 25,

        /// <summary>
        /// Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range
        /// is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
        /// </summary>
        DateTime2 = 26,

        /// <summary>
        /// Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31,
        /// 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
        /// Time zone value range is -14:00 through +14:00.
        /// </summary>
        DateTimeOffset = 27,
    }
}

#endif