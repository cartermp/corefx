// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
    // Issue 2499: Replace ad-hoc definitions of SafeHandleZeroOrMinusOneIsInvalid with a single definition
    //
    // Other definitions of this type should be removed in favor of this definition.
    internal abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
    {
        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleMinusOneIsInvalid" /> class.</summary>
        protected CriticalHandleMinusOneIsInvalid() : base(new IntPtr(-1))
        {
        }

        /// <summary>Gets a value that indicates whether the handle is invalid.</summary>
        /// <returns>true if the handle is not valid; otherwise, false.</returns>
        public override bool IsInvalid
        {
            get { return handle == new IntPtr(-1); }
        }
    }
}
