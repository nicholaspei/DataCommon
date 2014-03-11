// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if NET45

using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(DbParameter))]

#else

using System.ComponentModel;

namespace System.Data.Common
{
    /// <summary>
    /// Represents a parameter to a <see cref="DbCommand" />.
    /// </summary>
    public abstract class DbParameter
    {
        /// <summary>
        /// Gets or sets the DbType of the parameter.
        /// </summary>
        /// <value>
        /// One of the <see cref="Common.DbType" /> values. The default is <see cref="Common.DbType.String" />.
        /// </value>
        public abstract DbType DbType { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a
        /// stored procedure return value parameter.
        /// </summary>
        /// <value>
        /// One of the <see cref="ParameterDirection" /> values. The default is
        /// <see cref="ParameterDirection.Input" />.
        /// </value>
        /// <remarks>
        /// If the <c>Direction</c> is <see cref="ParameterDirection.Output" />, and execution of the associated
        /// <see cref="DbCommand" /> does not return a value, the <see cref="DbParameter" /> will contain a null value.
        /// <see cref="ParameterDirection.Output" />, <see cref="ParameterDirection.InputOutput" />, and
        /// <see cref="ParameterDirection.ReturnValue" /> parameters returned by calling
        /// <see cref="DbCommand.ExecuteReader()" /> cannot be accessed until you call
        /// <see cref="DbDataReader.Close" /> or <see cref="DbDataReader.Dispose" /> on the
        /// <see cref="DbDataReader" />.
        /// </remarks>
        [DefaultValue(ParameterDirection.Input)]
        public abstract ParameterDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the parameter accepts null values.
        /// </summary>
        /// <value>
        /// <c>true</c> if null values are accepted; otherwise false. The default is <c>false</c>.
        /// </value>
        /// <remarks>Null values are handled using the <see cref="DBNull" /> class.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="DbParameter" />.
        /// </summary>
        /// <value>
        /// The name of the <see cref="DbParameter" />. The default is an empty string ("").
        /// </value>
        [DefaultValue("")]
        public abstract string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of digits used to represent the <see cref="Value" /> property.
        /// </summary>
        /// <value>
        /// The maximum number of digits used to represent the <see cref="Value" /> property.
        /// </value>
        public virtual byte Precision
        {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Gets or sets the number of decimal places to which <see cref="Value" /> is resolved.
        /// </summary>
        /// <value>
        /// The number of decimal places to which <see cref="Value" /> is resolved.
        /// </value>
        public virtual byte Scale
        {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Gets or sets the maximum size, in bytes, of the data within the column.
        /// </summary>
        /// <value>
        /// The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter
        /// value.
        /// </value>
        /// <remarks>
        /// The <c>Size</c> property is used for binary and string types.
        /// For nonstring data types and ANSI string data, the <c>Size</c> property refers to the number of bytes. For
        /// Unicode string data, <c>Size</c> refers to the number of characters. The count for strings does not include
        /// the terminating character.
        /// For variable-length data types, <c>Size</c> describes the maximum amount of data to transmit to the server.
        /// For example, for a Unicode string value, <c>Size</c> could be used to limit the amount of data sent to the
        /// server to the first one hundred characters.
        /// For bidirectional and output parameters, and return values, you must set the value of <c>Size</c>. This is
        /// not required for input parameters, and if not explicitly set, the value is inferred from the actual size of
        /// the specified parameter when a parameterized statement is executed.
        /// The <c>DbType</c> and <c>Size</c> properties of a parameter can be inferred by setting <c>Value</c>.
        /// Therefore, you are not required to specify them. However, they are not exposed in <c>DbParameter</c>
        /// property settings. For example, if the size of the parameter has been inferred, <c>Size</c> does not
        /// contain inferred value after statement execution.
        /// For fixed length data types, the value of <c>Size</c> is ignored. It can be retrieved for informational
        /// purposes, and returns the maximum amount of bytes the provider uses when transmitting the value of the
        /// parameter to the server.
        /// If the size of the value supplied for a <c>DbParameter</c> exceeds the specified <c>Size</c>, the
        /// <c>Value</c> of the <c>DbParameter</c> will contain the specified value, truncated to <c>Size</c> of the
        /// <c>DbParameter</c>.
        /// For parameters of type <see cref="Common.DbType.String" />, the value of <c>Size</c> is length in Unicode
        /// characters. For parameters of type <see cref="DbType.Xml" />, <c>Size</c> is ignored.
        /// </remarks>
        public abstract int Size { get; set; }

        /// <summary>
        /// Gets or sets the name of the source column mapped to the DataSet and used for loading or returning the
        /// <see cref="Value" />.
        /// </summary>
        /// <value>
        /// The name of the source column mapped to the DataSet. The default is an empty string.
        /// </value>
        [DefaultValue("")]
        public abstract string SourceColumn { get; set; }

        /// <summary>
        /// Sets or gets a value which indicates whether the source column is nullable. This allows
        /// <see cref="DbCommandBuilder" /> to correctly generate update statements for nullable columns.
        /// </summary>
        /// <value>
        /// <c>true</c>if the source column is nullable; <c>false</c> if it is not.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(false)]
        public abstract bool SourceColumnNullMapping { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>
        /// An <see cref="Object" /> that is the value of the parameter. The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// For input parameters, the value is bound to the <see cref="DbCommand" /> that is sent to the server. For
        /// output and return value parameters, the value is set on completion of the <see cref="DbCommand" /> and
        /// after the <see cref="DbDataReader" /> is closed.
        /// When you send a null parameter value to the server, you must specify <see cref="DBNull" />, not null. The
        /// null value in the system is an empty object that has no value. <see cref="DBNull" /> is used to represent
        /// null values.
        /// If the application specifies the database type, the bound value is converted to that type when the provider
        /// sends the data to the server. The provider tries to convert any type of value if it supports the
        /// <see cref="IConvertible" /> interface. Conversion errors may result if the specified type is not compatible
        /// with the value.
        /// The <see cref="DbType" /> property can be inferred by setting the <c>Value</c>.
        /// </remarks>
        [DefaultValue(null)]
        public abstract object Value { get; set; }

        /// <summary>
        /// Resets the <see cref="DbType" /> property to its original settings.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public abstract void ResetDbType();
    }
}

#endif