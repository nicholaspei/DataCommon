// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
    // TODO: Double check throw statements
    internal class DbConnectionOptions
    {
        private readonly static Regex ConnectionStringValidKeyRegex = new Regex(@"^(?![;\s])[^\p{Cc}]+(?<!\s)$");
        private readonly static Regex ConnectionStringQuoteValueRegex = new Regex(@"^[^""'=;\s\p{Cc}]*$");

        private readonly NameValuePair _keyChain;

        public DbConnectionOptions(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
                _keyChain = ParseInternal(connectionString);
        }

        public NameValuePair KeyChain
        {
            get { return _keyChain; }
        }

        public static void AppendKeyValuePairBuilder(StringBuilder builder, string keyword, string value)
        {
            Debug.Assert(builder != null, "builder is null.");
            Debug.Assert(!string.IsNullOrEmpty(keyword), "keyword is null or empty.");

            if (!ConnectionStringValidKeyRegex.IsMatch(keyword))
                throw new ArgumentException(Strings.InvalidKey, keyword);
            if (value != null && value.IndexOf('\0') != -1)
                throw new ArgumentException(Strings.InvalidValue, value);

            if (builder.Length > 0 && builder[builder.Length - 1] != ';')
                builder.Append(";");

            builder.Append(keyword.Replace("=", "=="))
                .Append("=");

            if (value == null)
                return;

            if (ConnectionStringQuoteValueRegex.IsMatch(value))
            {
                builder.Append(value);
            }
            else if (value.IndexOfAny(new[] { '"', '\'' }) != -1)
            {
                builder.Append('\'')
                    .Append(value)
                    .Append('\'');
            }
            else
            {
                builder.Append('\"')
                    .Append(value.Replace("\"", "\"\""))
                    .Append('\"');
            }
        }

        private static int GetKeyValuePair(
            string connectionString,
            int currentPosition,
            out string keyword,
            out string value)
        {
            var buffer = new StringBuilder();
            var useOdbcRules = false;

            // TODO: Yikes.
            throw new NotImplementedException();
        }

        private static NameValuePair ParseInternal(string connectionString)
        {
            var currentPosition = 0;
            NameValuePair first = null;
            NameValuePair current = null;

            while (currentPosition < connectionString.Length)
            {
                string keyword;
                string value;
                currentPosition = GetKeyValuePair(connectionString, currentPosition, out keyword, out value);

                if (string.IsNullOrEmpty(keyword))
                    break;

                if (string.IsNullOrEmpty(keyword)
                    || keyword[0] == ';'
                    || char.IsWhiteSpace(keyword[0])
                    || keyword.IndexOf('\0') != -1)
                    throw new ArgumentException(Strings.KeywordNotSupported(keyword));

                if (first == null)
                {
                    first = new NameValuePair(keyword, value);
                    current = first;
                }
                else
                {
                    current.Next = new NameValuePair(keyword, value);
                    current = current.Next;
                }
            }

            return first;
        }
    }
}
