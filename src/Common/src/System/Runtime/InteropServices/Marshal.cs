// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Runtime.InteropServices
{
    // Stand-in type for low-level assemblies to obtain Win32 errors
    internal static class Marshal
    {
        /// <summary>Returns the error code returned by the last unmanaged function that was called using platform invoke that has the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.SetLastError" /> flag set.</summary>
        /// <returns>The last error code set by a call to the Win32 SetLastError function.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        public static int GetLastWin32Error()
        {
            return Interop.mincore.GetLastError();
        }
    }
}
