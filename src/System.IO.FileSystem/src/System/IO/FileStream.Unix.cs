// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Win32.SafeHandles;

namespace System.IO
{
    /// <summary>Provides a <see cref="T:System.IO.Stream" /> for a file, supporting both synchronous and asynchronous read and write operations.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
    /// <filterpriority>1</filterpriority>
    public partial class FileStream : Stream
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, and buffer size.</summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.-or-The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed.  </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
        public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) :
            this(handle, access, bufferSize, handle.IsAsync ?? false)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, buffer size, and synchronous or asynchronous state.</summary>
        /// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
        /// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
        /// <param name="isAsync">true if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
        /// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.-or-The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed.  </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
        public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
        {
            this._innerStream = new UnixFileStream(handle, access, bufferSize, isAsync, this);
        }
    }
}
