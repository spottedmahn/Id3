#region --- License & Copyright Notice ---
/*
Copyright (c) 2005-2019 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using Id3.Resources;
using System;
using System.Text.RegularExpressions;

namespace Id3.Frames
{
    public sealed class CopyrightFrame : TextFrame
    {
        public CopyrightFrame()
            : this(value: null) { }

        //todo review breaking change
        //want to throw exceptions instead of losing data
        public CopyrightFrame(string value)
            : this(value, throwExceptionsOnInValidValue: false) { }

        public CopyrightFrame(string value, bool throwExceptionsOnInValidValue) : base(value)
        {
            this.throwExceptionsOnInValidValue = throwExceptionsOnInValidValue;
        }

        public override string ToString()
        {
            return IsAssigned ? $"Copyright © {Value}" : string.Empty;
        }

        protected override void ValidateValue(string value)
        {
            if (!string.IsNullOrEmpty(value) && !CopyrightPrefixPattern.IsMatch(value))
                throw new ArgumentException(FrameMessages.Copyright_InvalidFormat, nameof(value));
        }

        internal override string TextValue
        {
            get => base.TextValue;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    base.TextValue = null;
                    return;
                }

                var isValid = CopyrightPrefixPattern.IsMatch(value);

                if (throwExceptionsOnInValidValue
                    && !isValid)
                    throw new InvalidCopyrightFrameException($"{value} does not conform to: The 'Copyright message' frame, in which the string must begin with a year and a space character(making five characters)");
                else if (isValid)
                    base.TextValue = value;
            }
        }

        private static readonly Regex CopyrightPrefixPattern = new Regex(@"^\d{4} ");
        private readonly bool throwExceptionsOnInValidValue;

        public static implicit operator CopyrightFrame(string value) => new CopyrightFrame(value);
    }
}