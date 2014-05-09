// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.Common;

namespace System.Data.Common
{
    internal class DbConnectionOptions
    {
        private static readonly Regex ConnectionStringValidKeywordRegex = new Regex(@"^(?![;\s])[^\p{Cc}]+(?<!\s)$");
        private static readonly Regex ConnectionStringQuoteValueRegex = new Regex(@"^[^""'=;\s\p{Cc}]*$");

        private readonly NameValuePair _keyChain;

        public DbConnectionOptions(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                _keyChain = ParseInternal(connectionString);
            }
        }

        public NameValuePair KeyChain
        {
            get { return _keyChain; }
        }

        public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
        {
            Debug.Assert(builder != null, "builder is null.");
            Debug.Assert(!string.IsNullOrEmpty(keyword), "keyword is null or empty.");

            if (!ConnectionStringValidKeywordRegex.IsMatch(keyword))
            {
                throw new ArgumentException(Strings.InvalidKey, keyword);
            }
            if (value != null && value.IndexOf('\0') != -1)
            {
                throw new ArgumentException(Strings.InvalidValue, value);
            }

            if (builder.Length > 0 && builder[builder.Length - 1] != ';')
            {
                builder.Append(";");
            }

            // Escape =
            builder.Append(keyword.Replace("=", "=="))
                .Append("=");

            if (value == null)
            {
                return;
            }

            if (ConnectionStringQuoteValueRegex.IsMatch(value))
            {
                builder.Append(value);
            }
            else if (value.IndexOf('"') != -1 && value.IndexOf('\'') == -1)
            {
                // If possible use single quotes to avoid escaping double qoutes
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

        private static int GetNextKeywordValuePair(
            string connectionString,
            int currentPosition,
            out string keyword,
            out string value)
        {
            var buffer = new StringBuilder();
            ParserState parserState;
            int index;
            // Extracted method to be able to use 'return' to break from the outer loop
            currentPosition = GetNextKeywordValuePairBody(
                connectionString,
                currentPosition,
                buffer,
                out keyword,
                out value,
                out parserState,
                out index);

            switch (parserState)
            {
                case ParserState.NothingYet:
                case ParserState.KeywordEnd:
                case ParserState.NullTermination:
                    break;
                case ParserState.Keyword:
                case ParserState.DoubleQuoteValue:
                case ParserState.SingleQuoteValue:
                    // Unbalanced quote
                    throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(index));
                case ParserState.KeywordEqual:
                    keyword = GetKeyword(buffer);
                    if (string.IsNullOrEmpty(keyword))
                    {
                        throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(index));
                    }

                    break;
                case ParserState.UnquotedValue:
                    value = GetValue(buffer, trimWhitespace: true);
                    var lastChar = value[value.Length - 1];
                    if ('\'' == lastChar || '"' == lastChar)
                    {
                        throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(index));
                    }

                    break;
                case ParserState.DoubleQuoteValueDoubleQuote:
                case ParserState.SingleQuoteValueSingleQuote:
                case ParserState.QuotedValueEnd:
                    value = GetValue(buffer, trimWhitespace: false);
                    break;
                default:
                    throw new InvalidOperationException(Strings.InternalError);
            }

            if (currentPosition < connectionString.Length
                && ';' == connectionString[currentPosition])
            {
                ++currentPosition;
            }

            return currentPosition;
        }

        private static int GetNextKeywordValuePairBody(
            string connectionString,
            int currentPosition,
            StringBuilder buffer,
            out string keyword,
            out string value,
            out ParserState parserState,
            out int tokenStart)
        {
            keyword = null;
            value = null;
            parserState = ParserState.NothingYet;
            tokenStart = currentPosition;
            for (; currentPosition < connectionString.Length; ++currentPosition)
            {
                var currentChar = connectionString[currentPosition];
                switch (parserState)
                {
                    case ParserState.NothingYet:
                        if (char.IsWhiteSpace(currentChar)
                            || ';' == currentChar)
                        {
                            continue;
                        }
                        else if ('\0' == currentChar)
                        {
                            parserState = ParserState.NullTermination;
                            continue;
                        }
                        else if (char.IsControl(currentChar))
                        {
                            throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                        }
                        tokenStart = currentPosition;
                        if ('=' != currentChar)
                        {
                            parserState = ParserState.Keyword;
                            break;
                        }
                        else
                        {
                            parserState = ParserState.KeywordEqual;
                            continue;
                        }
                    case ParserState.Keyword:
                        if ('=' == currentChar)
                        {
                            parserState = ParserState.KeywordEqual;
                            continue;
                        }
                        else if (!char.IsWhiteSpace(currentChar)
                                 && char.IsControl(currentChar))
                        {
                            throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                        }

                        break;
                    case ParserState.KeywordEqual:
                        if ('=' == currentChar)
                        {
                            // Escaped =
                            parserState = ParserState.Keyword;
                            break;
                        }
                        else
                        {
                            keyword = GetKeyword(buffer);
                            if (string.IsNullOrEmpty(keyword))
                            {
                                throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                            }
                            buffer.Length = 0;
                            parserState = ParserState.KeywordEnd;
                            goto case ParserState.KeywordEnd;
                        }
                    case ParserState.KeywordEnd:
                        if (char.IsWhiteSpace(currentChar))
                        {
                            continue;
                        }
                        else if ('\'' == currentChar)
                        {
                            parserState = ParserState.SingleQuoteValue;
                            continue;
                        }
                        else if ('"' == currentChar)
                        {
                            parserState = ParserState.DoubleQuoteValue;
                            continue;
                        }
                        else if (';' == currentChar
                                 || '\0' == currentChar)
                        {
                            return currentPosition;
                        }
                        else if (char.IsControl(currentChar))
                        {
                            throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                        }

                        parserState = ParserState.UnquotedValue;
                        break;
                    case ParserState.UnquotedValue:
                        if (';' == currentChar
                            || (!char.IsWhiteSpace(currentChar)
                                && char.IsControl(currentChar)))
                        {
                            return currentPosition;
                        }

                        break;
                    case ParserState.DoubleQuoteValue:
                        if ('"' == currentChar)
                        {
                            parserState = ParserState.DoubleQuoteValueDoubleQuote;
                            continue;
                        }
                        else if ('\0' == currentChar)
                        {
                            throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                        }

                        break;
                    case ParserState.DoubleQuoteValueDoubleQuote:
                        if ('"' == currentChar)
                        {
                            // Escaped "
                            parserState = ParserState.DoubleQuoteValue;
                            break;
                        }
                        else
                        {
                            value = GetValue(buffer, false);
                            parserState = ParserState.QuotedValueEnd;
                            goto case ParserState.QuotedValueEnd;
                        }
                    case ParserState.SingleQuoteValue:
                        if ('\'' == currentChar)
                        {
                            parserState = ParserState.SingleQuoteValueSingleQuote;
                            continue;
                        }
                        else if ('\0' == currentChar)
                        {
                            throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                        }

                        break;
                    case ParserState.SingleQuoteValueSingleQuote:
                        if ('\'' == currentChar)
                        {
                            // Escaped '
                            parserState = ParserState.SingleQuoteValue;
                            break;
                        }
                        else
                        {
                            value = GetValue(buffer, false);
                            parserState = ParserState.QuotedValueEnd;
                            goto case ParserState.QuotedValueEnd;
                        }
                    case ParserState.QuotedValueEnd:
                        if (!char.IsWhiteSpace(currentChar))
                        {
                            if (';' != currentChar)
                            {
                                if ('\0' != currentChar)
                                {
                                    throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                                }
                                parserState = ParserState.NullTermination;
                                continue;
                            }

                            return currentPosition;
                        }

                        continue;
                    case ParserState.NullTermination:
                        if ('\0' != currentChar
                            && !char.IsWhiteSpace(currentChar))
                        {
                            throw new ArgumentException(Strings.FormatConnectionStringInvalidSyntax(tokenStart));
                        }

                        continue;
                    default:
                        throw new InvalidOperationException(Strings.InternalError);
                }

                buffer.Append(currentChar);
            }
            return currentPosition;
        }

        private static string GetKeyword(StringBuilder buffer)
        {
            var length = buffer.Length;
            while (0 < length && char.IsWhiteSpace(buffer[length - 1]))
            {
                --length;
            }
            return buffer.ToString(0, length).ToLower();
        }

        private static string GetValue(StringBuilder buffer, bool trimWhitespace)
        {
            var length = buffer.Length;
            var startIndex = 0;
            if (trimWhitespace)
            {
                while (startIndex < length && char.IsWhiteSpace(buffer[startIndex]))
                {
                    ++startIndex;
                }
                while (0 < length && char.IsWhiteSpace(buffer[length - 1]))
                {
                    --length;
                }
            }
            return buffer.ToString(startIndex, length - startIndex);
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
                currentPosition = GetNextKeywordValuePair(
                    connectionString,
                    currentPosition,
                    out keyword,
                    out value);

                if (string.IsNullOrEmpty(keyword))
                {
                    break;
                }

                if (string.IsNullOrEmpty(keyword)
                    || keyword[0] == ';'
                    || char.IsWhiteSpace(keyword[0])
                    || keyword.IndexOf('\0') != -1)
                    throw new ArgumentException(Strings.FormatKeywordNotSupported(keyword));

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

        private enum ParserState
        {
            NothingYet,
            Keyword,
            KeywordEqual,
            KeywordEnd,
            UnquotedValue,
            DoubleQuoteValue,
            DoubleQuoteValueDoubleQuote,
            SingleQuoteValue,
            SingleQuoteValueSingleQuote,
            QuotedValueEnd,
            NullTermination,
        }
    }
}