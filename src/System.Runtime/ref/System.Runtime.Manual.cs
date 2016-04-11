// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// ------------------------------------------------------------------------------
// Changes to this file must follow the http://aka.ms/api-review process.
// ------------------------------------------------------------------------------


using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

// This is required for ProjectN to extend reflection. Once we make extensibility via contracts work on desktop, this can be removed.
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("System.Private.Reflection.Extensibility, PublicKey=002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293")]

namespace System
{
    /// <summary>Represents a single-precision floating-point number.</summary>
    /// <filterpriority>1</filterpriority>
    public partial struct Single
    {
        /// <summary>Represents the smallest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const float MinValue = (float)-3.40282346638528859e+38;
        /// <summary>Represents the smallest positive <see cref="T:System.Single" /> value that is greater than zero. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const float Epsilon = (float)1.4e-45;
        /// <summary>Represents the largest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const float MaxValue = (float)3.40282346638528859e+38;
        /// <summary>Represents positive infinity. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const float PositiveInfinity = (float)1.0 / (float)0.0;
        /// <summary>Represents negative infinity. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const float NegativeInfinity = (float)-1.0 / (float)0.0;
        /// <summary>Represents not a number (NaN). This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const float NaN = (float)0.0 / (float)0.0;
    }
    /// <summary>Represents a double-precision floating-point number.</summary>
    /// <filterpriority>1</filterpriority>
    public partial struct Double
    {
        /// <summary>Represents the smallest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const double MinValue = -1.7976931348623157E+308;
        /// <summary>Represents the largest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const double MaxValue = 1.7976931348623157E+308;

        // Note Epsilon should be a double whose hex representation is 0x1
        // on little endian machines.
        /// <summary>Represents the smallest positive <see cref="T:System.Double" /> value that is greater than zero. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const double Epsilon = 4.9406564584124654E-324;
        /// <summary>Represents negative infinity. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const double NegativeInfinity = (double)-1.0 / (double)(0.0);
        /// <summary>Represents positive infinity. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const double PositiveInfinity = (double)1.0 / (double)(0.0);
        /// <summary>Represents a value that is not a number (NaN). This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const double NaN = (double)0.0 / (double)0.0;
    }
    /// <summary>Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
    /// <filterpriority>1</filterpriority>
    public partial class Type
    {
        // Members promoted from MemberInfo
        /// <summary>Gets the type that declares the current nested type or generic type parameter.</summary>
        /// <returns>A <see cref="T:System.Type" /> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, null.</returns>
        /// <filterpriority>1</filterpriority>
        public abstract Type DeclaringType { get; }
        public abstract string Name { get; }
    }
}

namespace System.Runtime.InteropServices
{
    /// <summary>Provides a way to access a managed object from unmanaged memory.</summary>
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public partial struct GCHandle
    {
        /// <summary>Gets a value indicating whether the handle is allocated.</summary>
        /// <returns>true if the handle is allocated; otherwise, false.</returns>
        public bool IsAllocated { get { return default(bool); } }
        /// <summary>Gets or sets the object this handle represents.</summary>
        /// <returns>The object this handle represents.</returns>
        /// <exception cref="T:System.InvalidOperationException">The handle was freed, or never initialized. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        public object Target { [System.Security.SecurityCriticalAttribute]get { return default(object); } [System.Security.SecurityCriticalAttribute]set { } }
        /// <summary>Retrieves the address of an object in a <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" /> handle.</summary>
        /// <returns>The address of the pinned object as an <see cref="T:System.IntPtr" />. </returns>
        /// <exception cref="T:System.InvalidOperationException">The handle is any type other than <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" />. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        [System.Security.SecurityCriticalAttribute]
        public System.IntPtr AddrOfPinnedObject() { return default(System.IntPtr); }
        /// <summary>Allocates a <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" /> handle for the specified object.</summary>
        /// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> that protects the object from garbage collection. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
        /// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />. </param>
        /// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        [System.Security.SecurityCriticalAttribute]
        public static System.Runtime.InteropServices.GCHandle Alloc(object value) { return default(System.Runtime.InteropServices.GCHandle); }
        /// <summary>Allocates a handle of the specified type for the specified object.</summary>
        /// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> of the specified type. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
        /// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />. </param>
        /// <param name="type">One of the <see cref="T:System.Runtime.InteropServices.GCHandleType" /> values, indicating the type of <see cref="T:System.Runtime.InteropServices.GCHandle" /> to create. </param>
        /// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        [System.Security.SecurityCriticalAttribute]
        public static System.Runtime.InteropServices.GCHandle Alloc(object value, System.Runtime.InteropServices.GCHandleType type) { return default(System.Runtime.InteropServices.GCHandle); }
        /// <summary>Determines whether the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
        /// <returns>true if the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object; otherwise, false.</returns>
        /// <param name="o">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</param>
        public override bool Equals(object o) { return default(bool); }
        /// <summary>Releases a <see cref="T:System.Runtime.InteropServices.GCHandle" />.</summary>
        /// <exception cref="T:System.InvalidOperationException">The handle was freed or never initialized. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        [System.Security.SecurityCriticalAttribute]
        public void Free() { }
        /// <summary>Returns a new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object created from a handle to a managed object.</summary>
        /// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object that corresponds to the value parameter.  </returns>
        /// <param name="value">An <see cref="T:System.IntPtr" /> handle to a managed object to create a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object from.</param>
        /// <exception cref="T:System.InvalidOperationException">The value of the <paramref name="value" /> parameter is <see cref="F:System.IntPtr.Zero" />.</exception>
        [System.Security.SecurityCriticalAttribute]
        public static System.Runtime.InteropServices.GCHandle FromIntPtr(System.IntPtr value) { return default(System.Runtime.InteropServices.GCHandle); }
        /// <summary>Returns an identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
        /// <returns>An identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</returns>
        public override int GetHashCode() { return default(int); }
        /// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are equal.</summary>
        /// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters are equal; otherwise, false.</returns>
        /// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter. </param>
        /// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.  </param>
        public static bool operator ==(System.Runtime.InteropServices.GCHandle a, System.Runtime.InteropServices.GCHandle b) { return default(bool); }
        /// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
        /// <returns>The stored <see cref="T:System.Runtime.InteropServices.GCHandle" /> object using an internal integer representation.</returns>
        /// <param name="value">An <see cref="T:System.IntPtr" /> that indicates the handle for which the conversion is required. </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        [System.Security.SecurityCriticalAttribute]
        public static explicit operator System.Runtime.InteropServices.GCHandle(System.IntPtr value) { return default(System.Runtime.InteropServices.GCHandle); }
        /// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
        /// <returns>The integer value.</returns>
        /// <param name="value">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> for which the integer is required. </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        public static explicit operator System.IntPtr(System.Runtime.InteropServices.GCHandle value) { return default(System.IntPtr); }
        /// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are not equal.</summary>
        /// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters are not equal; otherwise, false.</returns>
        /// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter. </param>
        /// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.  </param>
        public static bool operator !=(System.Runtime.InteropServices.GCHandle a, System.Runtime.InteropServices.GCHandle b) { return default(bool); }
        /// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
        /// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object. </returns>
        /// <param name="value">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to retrieve an internal integer representation from.</param>
        public static System.IntPtr ToIntPtr(System.Runtime.InteropServices.GCHandle value) { return default(System.IntPtr); }
    }
    /// <summary>Represents the types of handles the <see cref="T:System.Runtime.InteropServices.GCHandle" /> class can allocate.</summary>
    public enum GCHandleType
    {
        /// <summary>This handle type represents an opaque handle, meaning you cannot resolve the address of the pinned object through the handle. You can use this type to track an object and prevent its collection by the garbage collector. This enumeration member is useful when an unmanaged client holds the only reference, which is undetectable from the garbage collector, to a managed object.</summary>
        Normal = 2,
        /// <summary>This handle type is similar to <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" />, but allows the address of the pinned object to be taken. This prevents the garbage collector from moving the object and hence undermines the efficiency of the garbage collector. Use the <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> method to free the allocated handle as soon as possible.</summary>
        Pinned = 3,
        /// <summary>This handle type is used to track an object, but allow it to be collected. When an object is collected, the contents of the <see cref="T:System.Runtime.InteropServices.GCHandle" /> are zeroed. Weak references are zeroed before the finalizer runs, so even if the finalizer resurrects the object, the Weak reference is still zeroed.</summary>
        Weak = 0,
        /// <summary>This handle type is similar to <see cref="F:System.Runtime.InteropServices.GCHandleType.Weak" />, but the handle is not zeroed if the object is resurrected during finalization.</summary>
        WeakTrackResurrection = 1,
    }
}
