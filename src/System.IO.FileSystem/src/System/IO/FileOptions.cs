// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace System.IO
{
    // Maps to FILE_FLAG_DELETE_ON_CLOSE and similar values from winbase.h.
    // We didn't expose a number of these values because we didn't believe 
    // a number of them made sense in managed code, at least not yet.

    /// <summary>Represents advanced options for creating a <see cref="T:System.IO.FileStream" /> object.</summary>
    /// <filterpriority>1</filterpriority>
    [Flags]
    /// <devdoc>
    ///   Additional options to how to create a FileStream.
    /// </devdoc>
    public enum FileOptions
    {
        // NOTE: any change to FileOptions enum needs to be 
        // matched in the FileStream ctor for error validation
        /// <summary>Indicates that no additional options should be used when creating a <see cref="T:System.IO.FileStream" /> object.</summary>
        None = 0,
        /// <summary>Indicates that the system should write through any intermediate cache and go directly to disk.</summary>
        WriteThrough = unchecked((int)0x80000000),
        /// <summary>Indicates that a file can be used for asynchronous reading and writing. </summary>
        Asynchronous = unchecked((int)0x40000000), // FILE_FLAG_OVERLAPPED
                                                   // NoBuffering = 0x20000000,
                                                   /// <summary>Indicates that the file is accessed randomly. The system can use this as a hint to optimize file caching.</summary>
        RandomAccess = 0x10000000,
        /// <summary>Indicates that a file is automatically deleted when it is no longer in use.</summary>
        DeleteOnClose = 0x04000000,
        /// <summary>Indicates that the file is to be accessed sequentially from beginning to end. The system can use this as a hint to optimize file caching. If an application moves the file pointer for random access, optimum caching may not occur; however, correct operation is still guaranteed. </summary>
        SequentialScan = 0x08000000,
        // AllowPosix = 0x01000000,  // FILE_FLAG_POSIX_SEMANTICS
        // BackupOrRestore,
        // DisallowReparsePoint = 0x00200000, // FILE_FLAG_OPEN_REPARSE_POINT
        // NoRemoteRecall = 0x00100000, // FILE_FLAG_OPEN_NO_RECALL
        // FirstPipeInstance = 0x00080000, // FILE_FLAG_FIRST_PIPE_INSTANCE
        /// <summary>Indicates that a file is encrypted and can be decrypted only by using the same user account used for encryption.</summary>
        Encrypted = 0x00004000, // FILE_ATTRIBUTE_ENCRYPTED
    }
}

