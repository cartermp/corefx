// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Text
{
    // This is the enumeration for Normalization Forms
#if INTERNAL_GLOBALIZATION_EXTENSIONS
    internal
#else
    /// <summary>Defines the type of normalization to perform.</summary>
    /// <filterpriority>2</filterpriority>
    public
#endif
    enum NormalizationForm
    {
        /// <summary>Indicates that a Unicode string is normalized using full canonical decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
        FormC = 1,
        /// <summary>Indicates that a Unicode string is normalized using full canonical decomposition.</summary>
        FormD = 2,
        /// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
        FormKC = 5,
        /// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition.</summary>
        FormKD = 6
    }
}
