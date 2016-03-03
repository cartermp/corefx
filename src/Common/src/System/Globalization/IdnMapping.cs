// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// This file contains the IDN functions and implementation.
//
// This allows encoding of non-ASCII domain names in a "punycode" form,
// for example:
//
//     \u5B89\u5BA4\u5948\u7F8E\u6075-with-SUPER-MONKEYS
//
// is encoded as:
//
//     xn---with-SUPER-MONKEYS-pc58ag80a8qai00g7n9n
//
// Additional options are provided to allow unassigned IDN characters and
// to validate according to the Std3ASCII Rules (like DNS names).
//
// There are also rules regarding bidirectionality of text and the length
// of segments.
//
// For additional rules see also:
//  RFC 3490 - Internationalizing Domain Names in Applications (IDNA)
//  RFC 3491 - Nameprep: A Stringprep Profile for Internationalized Domain Names (IDN)
//  RFC 3492 - Punycode: A Bootstring encoding of Unicode for Internationalized Domain Names in Applications (IDNA)

using System.Diagnostics.Contracts;

namespace System.Globalization
{
    // IdnMapping class used to map names to Punycode
#if INTERNAL_GLOBALIZATION_EXTENSIONS
    internal 
#else
    /// <summary>Supports the use of non-ASCII characters for Internet domain names. This class cannot be inherited.</summary>
    public
#endif
    sealed partial class IdnMapping
    {
        private bool _allowUnassigned;
        private bool _useStd3AsciiRules;

        /// <summary>Initializes a new instance of the <see cref="T:System.Globalization.IdnMapping" /> class. </summary>
        public IdnMapping()
        {
        }

        /// <summary>Gets or sets a value that indicates whether unassigned Unicode code points are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
        /// <returns>true if unassigned code points are used in operations; otherwise, false.</returns>
        public bool AllowUnassigned
        {
            get { return _allowUnassigned; }
            set { _allowUnassigned = value; }
        }

        /// <summary>Gets or sets a value that indicates whether standard or relaxed naming conventions are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
        /// <returns>true if standard naming conventions are used in operations; otherwise, false.</returns>
        public bool UseStd3AsciiRules
        {
            get { return _useStd3AsciiRules; }
            set { _useStd3AsciiRules = value; }
        }

        // Gets ASCII (Punycode) version of the string
        /// <summary>Encodes a string of domain name labels that consist of Unicode characters to a string of displayable Unicode characters in the US-ASCII character range. The string is formatted according to the IDNA standard.</summary>
        /// <returns>The equivalent of the string specified by the <paramref name="unicode" /> parameter, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
        /// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="unicode" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
        public string GetAscii(string unicode)
        {
            return GetAscii(unicode, 0);
        }

        /// <summary>Encodes a substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard.  </summary>
        /// <returns>The equivalent of the substring specified by the <paramref name="unicode" /> and <paramref name="index" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
        /// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
        /// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring to convert. The conversion operation continues to the end of the <paramref name="unicode" /> string.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="unicode" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
        public string GetAscii(string unicode, int index)
        {
            if (unicode == null)
                throw new ArgumentNullException(nameof(unicode));
            Contract.EndContractBlock();
            return GetAscii(unicode, index, unicode.Length - index);
        }

        /// <summary>Encodes the specified number of characters in a  substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard. </summary>
        /// <returns>The equivalent of the substring specified by the <paramref name="unicode" />, <paramref name="index" />, and <paramref name="count" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
        /// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
        /// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring.</param>
        /// <param name="count">The number of characters to convert in the substring that starts at the position specified by  <paramref name="index" /> in the <paramref name="unicode" /> string. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="unicode" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" /> minus <paramref name="count" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
        public string GetAscii(string unicode, int index, int count)
        {
            if (unicode == null)
                throw new ArgumentNullException(nameof(unicode));
            if (index < 0 || count < 0)
                throw new ArgumentOutOfRangeException((index < 0) ? nameof(index) : nameof(count), SR.ArgumentOutOfRange_NeedNonNegNum);
            if (index > unicode.Length)
                throw new ArgumentOutOfRangeException("byteIndex", SR.ArgumentOutOfRange_Index);
            if (index > unicode.Length - count)
                throw new ArgumentOutOfRangeException(nameof(unicode), SR.ArgumentOutOfRange_IndexCountBuffer);
            Contract.EndContractBlock();

            // We're only using part of the string
            unicode = unicode.Substring(index, count);

            if (unicode.Length == 0)
            {
                throw new ArgumentException(SR.Argument_IdnBadLabelSize, nameof(unicode));
            }
            if (unicode[unicode.Length - 1] == 0)
            {
                throw new ArgumentException(SR.Format(SR.Argument_InvalidCharSequence, unicode.Length - 1), nameof(unicode));
            }

            return GetAsciiCore(unicode);
        }

        // Gets Unicode version of the string.  Normalized and limited to IDNA characters.
        /// <summary>Decodes a string of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
        /// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> parameter.</returns>
        /// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="ascii" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
        public string GetUnicode(string ascii)
        {
            return GetUnicode(ascii, 0);
        }

        /// <summary>Decodes a substring of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
        /// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> and <paramref name="index" /> parameters.</returns>
        /// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
        /// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring to decode. The decoding operation continues to the end of the <paramref name="ascii" /> string.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="ascii" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
        public string GetUnicode(string ascii, int index)
        {
            if (ascii == null)
                throw new ArgumentNullException(nameof(ascii));
            Contract.EndContractBlock();
            return GetUnicode(ascii, index, ascii.Length - index);
        }

        /// <summary>Decodes a substring of a specified length that contains one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
        /// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" />, <paramref name="index" />, and <paramref name="count" /> parameters.</returns>
        /// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
        /// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring. </param>
        /// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="index" /> in the <paramref name="ascii" /> string. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="ascii" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" /> minus <paramref name="count" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
        public string GetUnicode(string ascii, int index, int count)
        {
            if (ascii == null)
                throw new ArgumentNullException(nameof(ascii));
            if (index < 0 || count < 0)
                throw new ArgumentOutOfRangeException((index < 0) ? nameof(index) : nameof(count), SR.ArgumentOutOfRange_NeedNonNegNum);
            if (index > ascii.Length)
                throw new ArgumentOutOfRangeException("byteIndex", SR.ArgumentOutOfRange_Index);
            if (index > ascii.Length - count)
                throw new ArgumentOutOfRangeException(nameof(ascii), SR.ArgumentOutOfRange_IndexCountBuffer);

            // This is a case (i.e. explicitly null-terminated input) where behavior in .NET and Win32 intentionally differ.
            // The .NET APIs should (and did in v4.0 and earlier) throw an ArgumentException on input that includes a terminating null.
            // The Win32 APIs fail on an embedded null, but not on a terminating null.
            if (count > 0 && ascii[index + count - 1] == (char)0)
                throw new ArgumentException(SR.Argument_IdnBadPunycode, nameof(ascii));
            Contract.EndContractBlock();

            // We're only using part of the string
            ascii = ascii.Substring(index, count);

            return GetUnicodeCore(ascii);
        }

        /// <summary>Indicates whether a specified object and the current <see cref="T:System.Globalization.IdnMapping" /> object are equal.</summary>
        /// <returns>true if the object specified by the <paramref name="obj" /> parameter is derived from <see cref="T:System.Globalization.IdnMapping" /> and its <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties are equal; otherwise, false. </returns>
        /// <param name="obj">The object to compare to the current object.</param>
        public override bool Equals(object obj)
        {
            IdnMapping that = obj as IdnMapping;
            return
                that != null &&
                _allowUnassigned == that._allowUnassigned &&
                _useStd3AsciiRules == that._useStd3AsciiRules;
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
        /// <returns>One of four 32-bit signed constants derived from the properties of an <see cref="T:System.Globalization.IdnMapping" /> object.  The return value has no special meaning and is not suitable for use in a hash code algorithm.</returns>
        public override int GetHashCode()
        {
            return (_allowUnassigned ? 100 : 200) + (_useStd3AsciiRules ? 1000 : 2000);
        }
    }
}
