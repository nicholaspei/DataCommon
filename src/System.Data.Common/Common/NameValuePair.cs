// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;

namespace System.Data.Common
{
    internal sealed class NameValuePair
    {
        private readonly string _name;
        private readonly string _value;
        private NameValuePair _next;

        public NameValuePair(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }

        public NameValuePair Next
        {
            get { return _next; }
            set
            {
                Debug.Assert(value != null, "value is null.");
                Debug.Assert(_next == null, "_next is not null.");

                _next = value;
            }
        }
    }
}
