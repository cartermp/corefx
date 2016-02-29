// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.Versioning;
using System.Security;
using System.Text;
using System.Threading;

using Microsoft.Win32.SafeHandles;

namespace System.IO
{
    // Class for creating FileStream objects, and some basic file management
    // routines such as Delete, etc.
    /// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
    /// <filterpriority>1</filterpriority>
    public static class File
    {
        /// <summary>Opens an existing UTF-8 encoded text file for reading.</summary>
        /// <returns>A <see cref="T:System.IO.StreamReader" /> on the specified path.</returns>
        /// <param name="path">The file to be opened for reading. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static StreamReader OpenText(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalOpen(path);
            return new StreamReader(stream);
        }

        /// <summary>Creates or opens a file for writing UTF-8 encoded text.</summary>
        /// <returns>A <see cref="T:System.IO.StreamWriter" /> that writes to the specified file using UTF-8 encoding.</returns>
        /// <param name="path">The file to be opened for writing. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static StreamWriter CreateText(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalCreate(path);
            return new StreamWriter(stream);
        }

        /// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.</summary>
        /// <returns>A stream writer that appends UTF-8 encoded text to the specified file or to a new file.</returns>
        /// <param name="path">The path to the file to append to. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn’t exist or it is on an unmapped drive). </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static StreamWriter AppendText(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalAppend(path);
            return new StreamWriter(stream);
        }


        // Copies an existing file to a new file. An exception is raised if the
        // destination file already exists. Use the 
        // Copy(String, String, boolean) method to allow 
        // overwriting an existing file.
        //
        // The caller must have certain FileIOPermissions.  The caller must have
        // Read permission to sourceFileName and Create
        // and Write permissions to destFileName.
        // 
        /// <summary>Copies an existing file to a new file. Overwriting a file of the same name is not allowed.</summary>
        /// <param name="sourceFileName">The file to copy. </param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="sourceFileName" /> or <paramref name="destFileName" /> specifies a directory. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.FileNotFoundException"><paramref name="sourceFileName" /> was not found. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="destFileName" /> exists.-or- An I/O error has occurred. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void Copy(String sourceFileName, String destFileName)
        {
            if (sourceFileName == null)
                throw new ArgumentNullException(nameof(sourceFileName), SR.ArgumentNull_FileName);
            if (destFileName == null)
                throw new ArgumentNullException(nameof(destFileName), SR.ArgumentNull_FileName);
            if (sourceFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(sourceFileName));
            if (destFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destFileName));
            Contract.EndContractBlock();

            InternalCopy(sourceFileName, destFileName, false);
        }

        // Copies an existing file to a new file. If overwrite is 
        // false, then an IOException is thrown if the destination file 
        // already exists.  If overwrite is true, the file is 
        // overwritten.
        //
        // The caller must have certain FileIOPermissions.  The caller must have
        // Read permission to sourceFileName 
        // and Write permissions to destFileName.
        // 
        /// <summary>Copies an existing file to a new file. Overwriting a file of the same name is allowed.</summary>
        /// <param name="sourceFileName">The file to copy. </param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory. </param>
        /// <param name="overwrite">true if the destination file can be overwritten; otherwise, false. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. -or-<paramref name="destFileName" /> is read-only.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="sourceFileName" /> or <paramref name="destFileName" /> specifies a directory. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.FileNotFoundException"><paramref name="sourceFileName" /> was not found. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="destFileName" /> exists and <paramref name="overwrite" /> is false.-or- An I/O error has occurred. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void Copy(String sourceFileName, String destFileName, bool overwrite)
        {
            if (sourceFileName == null)
                throw new ArgumentNullException(nameof(sourceFileName), SR.ArgumentNull_FileName);
            if (destFileName == null)
                throw new ArgumentNullException(nameof(destFileName), SR.ArgumentNull_FileName);
            if (sourceFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(sourceFileName));
            if (destFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destFileName));
            Contract.EndContractBlock();

            InternalCopy(sourceFileName, destFileName, overwrite);
        }

        /// <devdoc>
        ///    Note: This returns the fully qualified name of the destination file.
        /// </devdoc>
        [System.Security.SecuritySafeCritical]
        internal static String InternalCopy(String sourceFileName, String destFileName, bool overwrite)
        {
            Contract.Requires(sourceFileName != null);
            Contract.Requires(destFileName != null);
            Contract.Requires(sourceFileName.Length > 0);
            Contract.Requires(destFileName.Length > 0);

            String fullSourceFileName = Path.GetFullPath(sourceFileName);
            String fullDestFileName = Path.GetFullPath(destFileName);

            FileSystem.Current.CopyFile(fullSourceFileName, fullDestFileName, overwrite);

            return fullDestFileName;
        }


        // Creates a file in a particular path.  If the file exists, it is replaced.
        // The file is opened with ReadWrite accessand cannot be opened by another 
        // application until it has been closed.  An IOException is thrown if the 
        // directory specified doesn't exist.
        //
        // Your application must have Create, Read, and Write permissions to
        // the file.
        // 
        /// <summary>Creates or overwrites a file in the specified path.</summary>
        /// <returns>A <see cref="T:System.IO.FileStream" /> that provides read/write access to the file specified in <paramref name="path" />.</returns>
        /// <param name="path">The path and name of the file to create. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static FileStream Create(String path)
        {
            return Create(path, FileStream.DefaultBufferSize);
        }

        // Creates a file in a particular path.  If the file exists, it is replaced.
        // The file is opened with ReadWrite access and cannot be opened by another 
        // application until it has been closed.  An IOException is thrown if the 
        // directory specified doesn't exist.
        //
        // Your application must have Create, Read, and Write permissions to
        // the file.
        // 
        /// <summary>Creates or overwrites the specified file.</summary>
        /// <returns>A <see cref="T:System.IO.FileStream" /> with the specified buffer size that provides read/write access to the file specified in <paramref name="path" />.</returns>
        /// <param name="path">The name of the file. </param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static FileStream Create(String path, int bufferSize)
        {
            return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
        }

        /// <summary>Creates or overwrites the specified file, specifying a buffer size and a <see cref="T:System.IO.FileOptions" /> value that describes how to create or overwrite the file.</summary>
        /// <returns>A new file with the specified buffer size.</returns>
        /// <param name="path">The name of the file. </param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <param name="options">One of the <see cref="T:System.IO.FileOptions" /> values that describes how to create or overwrite the file.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" /> and file encryption is not supported on the current platform.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a file that is read-only. </exception>
        public static FileStream Create(String path, int bufferSize, FileOptions options)
        {
            return new FileStream(path, FileMode.Create, FileAccess.ReadWrite,
                                  FileShare.None, bufferSize, options);
        }

        // Deletes a file. The file specified by the designated path is deleted.
        // If the file does not exist, Delete succeeds without throwing
        // an exception.
        // 
        // On NT, Delete will fail for a file that is open for normal I/O
        // or a file that is memory mapped.  
        // 
        // Your application must have Delete permission to the target file.
        // 
        /// <summary>Deletes the specified file. </summary>
        /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- The file is an executable file that is in use.-or- <paramref name="path" /> is a directory.-or- <paramref name="path" /> specified a read-only file. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static void Delete(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.EndContractBlock();

            String fullPath = Path.GetFullPath(path);

            FileSystem.Current.DeleteFile(fullPath);
        }


        // Tests if a file exists. The result is true if the file
        // given by the specified path exists; otherwise, the result is
        // false.  Note that if path describes a directory,
        // Exists will return true.
        //
        // Your application must have Read permission for the target directory.
        // 
        /// <summary>Determines whether the specified file exists.</summary>
        /// <returns>true if the caller has the required permissions and <paramref name="path" /> contains the name of an existing file; otherwise, false. This method also returns false if <paramref name="path" /> is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of <paramref name="path" />.</returns>
        /// <param name="path">The file to check. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static bool Exists(String path)
        {
            try {
                if (path == null)
                    return false;
                if (path.Length == 0)
                    return false;

                path = Path.GetFullPath(path);
                // After normalizing, check whether path ends in directory separator.
                // Otherwise, FillAttributeInfo removes it and we may return a false positive.
                // GetFullPath should never return null
                Debug.Assert(path != null, "File.Exists: GetFullPath returned null");
                if (path.Length > 0 && PathInternal.IsDirectorySeparator(path[path.Length - 1])) {
                    return false;
                }

                return InternalExists(path);
            }
            catch (ArgumentException) { }
            catch (NotSupportedException) { } // Security can throw this on ":"
            catch (SecurityException) { }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }

            return false;
        }

        [System.Security.SecurityCritical]  // auto-generated
        internal static bool InternalExists(String path)
        {
            return FileSystem.Current.FileExists(path);
        }

        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path with read/write access.</summary>
        /// <returns>A <see cref="T:System.IO.FileStream" /> opened in the specified mode and path, with read/write access and not shared.</returns>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="mode" /> specified an invalid value. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static FileStream Open(String path, FileMode mode)
        {
            return Open(path, mode, (mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite), FileShare.None);
        }

        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, with the specified mode and access.</summary>
        /// <returns>An unshared <see cref="T:System.IO.FileStream" /> that provides access to the specified file, with the specified mode and access.</returns>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="access" /> specified Read and <paramref name="mode" /> specified Create, CreateNew, Truncate, or Append. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only and <paramref name="access" /> is not Read.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="mode" /> or <paramref name="access" /> specified an invalid value. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static FileStream Open(String path, FileMode mode, FileAccess access)
        {
            return Open(path, mode, access, FileShare.None);
        }

        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</summary>
        /// <returns>A <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file. </param>
        /// <param name="share">A <see cref="T:System.IO.FileShare" /> value specifying the type of access other threads have to the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="access" /> specified Read and <paramref name="mode" /> specified Create, CreateNew, Truncate, or Append. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only and <paramref name="access" /> is not Read.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> specified an invalid value. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static FileStream Open(String path, FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStream(path, mode, access, share);
        }

        internal static DateTimeOffset GetUtcDateTimeOffset(DateTime dateTime)
        {
            // File and Directory UTC APIs treat a DateTimeKind.Unspecified as UTC whereas 
            // ToUniversalTime treats this as local.
            if (dateTime.Kind == DateTimeKind.Unspecified) {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            return dateTime.ToUniversalTime();
        }

        /// <summary>Sets the date and time the file was created.</summary>
        /// <param name="path">The file for which to set the creation date and time information. </param>
        /// <param name="creationTime">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in local time. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="creationTime" /> specifies a value outside the range of dates, times, or both permitted for this operation. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetCreationTime(String path, DateTime creationTimeUtc)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetCreationTime(fullPath, creationTimeUtc, asDirectory: false);
        }

        /// <summary>Sets the date and time, in coordinated universal time (UTC), that the file was created.</summary>
        /// <param name="path">The file for which to set the creation date and time information. </param>
        /// <param name="creationTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="creationTime" /> specifies a value outside the range of dates, times, or both permitted for this operation. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetCreationTimeUtc(String path, DateTime creationTime)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetCreationTime(fullPath, GetUtcDateTimeOffset(creationTime), asDirectory: false);
        }

        /// <summary>Returns the creation date and time of the specified file or directory.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain creation date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static DateTime GetCreationTime(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetCreationTime(fullPath).LocalDateTime;
        }

        /// <summary>Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain creation date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static DateTime GetCreationTimeUtc(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetCreationTime(fullPath).UtcDateTime;
        }

        /// <summary>Sets the date and time the specified file was last accessed.</summary>
        /// <param name="path">The file for which to set the access date and time information. </param>
        /// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in local time. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastAccessTime(String path, DateTime lastAccessTime)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastAccessTime(fullPath, lastAccessTime, asDirectory: false);
        }

        /// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.</summary>
        /// <param name="path">The file for which to set the access date and time information. </param>
        /// <param name="lastAccessTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastAccessTimeUtc(String path, DateTime lastAccessTimeUtc)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastAccessTime(fullPath, GetUtcDateTimeOffset(lastAccessTimeUtc), asDirectory: false);
        }

        /// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static DateTime GetLastAccessTime(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetLastAccessTime(fullPath).LocalDateTime;
        }

        /// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static DateTime GetLastAccessTimeUtc(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetLastAccessTime(fullPath).UtcDateTime;
        }

        /// <summary>Sets the date and time that the specified file was last written to.</summary>
        /// <param name="path">The file for which to set the date and time information. </param>
        /// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in local time. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastWriteTime(String path, DateTime lastWriteTime)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastWriteTime(fullPath, lastWriteTime, asDirectory: false);
        }

        /// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.</summary>
        /// <param name="path">The file for which to set the date and time information. </param>
        /// <param name="lastWriteTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastWriteTimeUtc(String path, DateTime lastWriteTimeUtc)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastWriteTime(fullPath, GetUtcDateTimeOffset(lastWriteTimeUtc), asDirectory: false);
        }

        /// <summary>Returns the date and time the specified file or directory was last written to.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain write date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static DateTime GetLastWriteTime(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetLastWriteTime(fullPath).LocalDateTime;
        }

        /// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain write date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static DateTime GetLastWriteTimeUtc(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetLastWriteTime(fullPath).UtcDateTime;
        }

        /// <summary>Gets the <see cref="T:System.IO.FileAttributes" /> of the file on the path.</summary>
        /// <returns>The <see cref="T:System.IO.FileAttributes" /> of the file on the path.</returns>
        /// <param name="path">The path to the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is empty, contains only white spaces, or contains invalid characters. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException"><paramref name="path" /> represents a file and is invalid, such as being on an unmapped drive, or the file cannot be found. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> represents a directory and is invalid, such as being on an unmapped drive, or the directory cannot be found.</exception>
        /// <exception cref="T:System.IO.IOException">This file is being used by another process.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static FileAttributes GetAttributes(String path)
        {
            String fullPath = Path.GetFullPath(path);
            return FileSystem.Current.GetAttributes(fullPath);
        }

        /// <summary>Sets the specified <see cref="T:System.IO.FileAttributes" /> of the file on the specified path.</summary>
        /// <param name="path">The path to the file. </param>
        /// <param name="fileAttributes">A bitwise combination of the enumeration values. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is empty, contains only white spaces, contains invalid characters, or the file attribute is invalid. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecurityCritical]
        public static void SetAttributes(String path, FileAttributes fileAttributes)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetAttributes(fullPath, fileAttributes);
        }

        /// <summary>Opens an existing file for reading.</summary>
        /// <returns>A read-only <see cref="T:System.IO.FileStream" /> on the specified path.</returns>
        /// <param name="path">The file to be opened for reading. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static FileStream OpenRead(String path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }


        /// <summary>Opens an existing file or creates a new file for writing.</summary>
        /// <returns>An unshared <see cref="T:System.IO.FileStream" /> object on the specified path with <see cref="F:System.IO.FileAccess.Write" /> access.</returns>
        /// <param name="path">The file to be opened for writing. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path" /> specified a read-only file or directory. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static FileStream OpenWrite(String path)
        {
            return new FileStream(path, FileMode.OpenOrCreate,
                                  FileAccess.Write, FileShare.None);
        }

        /// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
        /// <returns>A string containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static String ReadAllText(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            return InternalReadAllText(path, Encoding.UTF8);
        }

        /// <summary>Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
        /// <returns>A string containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static String ReadAllText(String path, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            return InternalReadAllText(path, encoding);
        }

        [System.Security.SecurityCritical]
        private static String InternalReadAllText(String path, Encoding encoding)
        {
            Contract.Requires(path != null);
            Contract.Requires(encoding != null);
            Contract.Requires(path.Length > 0);

            Stream stream = FileStream.InternalOpen(path, useAsync: false);

            using (StreamReader sr = new StreamReader(stream, encoding, true))
                return sr.ReadToEnd();
        }

        /// <summary>Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string to write to the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null or <paramref name="contents" /> is empty.  </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static void WriteAllText(String path, String contents)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            InternalWriteAllText(path, contents, UTF8NoBOM);
        }

        /// <summary>Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.</summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string to write to the file. </param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null or <paramref name="contents" /> is empty. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static void WriteAllText(String path, String contents, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            InternalWriteAllText(path, contents, encoding);
        }

        [System.Security.SecurityCritical]
        private static void InternalWriteAllText(String path, String contents, Encoding encoding)
        {
            Contract.Requires(path != null);
            Contract.Requires(encoding != null);
            Contract.Requires(path.Length > 0);

            Stream stream = FileStream.InternalCreate(path, useAsync: false);

            using (StreamWriter sw = new StreamWriter(stream, encoding))
                sw.Write(contents);
        }

        /// <summary>Opens a binary file, reads the contents of the file into a byte array, and then closes the file.</summary>
        /// <returns>A byte array containing the contents of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static byte[] ReadAllBytes(String path)
        {
            return InternalReadAllBytes(path);
        }

        [System.Security.SecurityCritical]
        private static byte[] InternalReadAllBytes(String path)
        {
            // bufferSize == 1 used to avoid unnecessary buffer in FileStream
            using (FileStream fs = FileStream.InternalOpen(path, bufferSize: 1, useAsync: false)) {
                long fileLength = fs.Length;
                if (fileLength > Int32.MaxValue)
                    throw new IOException(SR.IO_FileTooLong2GB);

                int index = 0;
                int count = (int)fileLength;
                byte[] bytes = new byte[count];
                while (count > 0) {
                    int n = fs.Read(bytes, index, count);
                    if (n == 0)
                        throw Error.GetEndOfFile();
                    index += n;
                    count -= n;
                }
                return bytes;
            }
        }

        /// <summary>Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="bytes">The bytes to write to the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null or the byte array is empty. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static void WriteAllBytes(String path, byte[] bytes)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path), SR.ArgumentNull_Path);
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            Contract.EndContractBlock();

            InternalWriteAllBytes(path, bytes);
        }

        [System.Security.SecurityCritical]
        private static void InternalWriteAllBytes(String path, byte[] bytes)
        {
            Contract.Requires(path != null);
            Contract.Requires(path.Length != 0);
            Contract.Requires(bytes != null);

            using (FileStream fs = FileStream.InternalCreate(path, useAsync: false)) {
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        /// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
        /// <returns>A string array containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] ReadAllLines(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            return InternalReadAllLines(path, Encoding.UTF8);
        }

        /// <summary>Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
        /// <returns>A string array containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] ReadAllLines(String path, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            return InternalReadAllLines(path, encoding);
        }

        private static String[] InternalReadAllLines(String path, Encoding encoding)
        {
            Contract.Requires(path != null);
            Contract.Requires(encoding != null);
            Contract.Requires(path.Length != 0);

            String line;
            List<String> lines = new List<String>();

            Stream stream = FileStream.InternalOpen(path, useAsync: false);

            using (StreamReader sr = new StreamReader(stream, encoding))
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);

            return lines.ToArray();
        }

        /// <summary>Reads the lines of a file.</summary>
        /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
        /// <param name="path">The file to read.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.IO.PathTooLongException"><paramref name="path" /> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path" /> is a directory.-or-The caller does not have the required permission.</exception>
        public static IEnumerable<String> ReadLines(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            return ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
        }

        /// <summary>Read the lines of a file that has a specified encoding.</summary>
        /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
        /// <param name="path">The file to read.</param>
        /// <param name="encoding">The encoding that is applied to the contents of the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.IO.PathTooLongException"><paramref name="path" /> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path" /> is a directory.-or-The caller does not have the required permission.</exception>
        public static IEnumerable<String> ReadLines(String path, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            return ReadLinesIterator.CreateIterator(path, encoding);
        }

        /// <summary>Creates a new file, writes a collection of strings to the file, and then closes the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The lines to write to the file.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">Either<paramref name=" path " />or <paramref name="contents" /> is null.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.IO.PathTooLongException"><paramref name="path" /> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path" /> is a directory.-or-The caller does not have the required permission.</exception>
        public static void WriteAllLines(String path, IEnumerable<String> contents)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (contents == null)
                throw new ArgumentNullException(nameof(contents));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalCreate(path, useAsync: false);

            InternalWriteAllLines(new StreamWriter(stream, UTF8NoBOM), contents);
        }

        /// <summary>Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The lines to write to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">Either<paramref name=" path" />,<paramref name=" contents" />, or <paramref name="encoding" /> is null.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.IO.PathTooLongException"><paramref name="path" /> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path" /> is a directory.-or-The caller does not have the required permission.</exception>
        public static void WriteAllLines(String path, IEnumerable<String> contents, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (contents == null)
                throw new ArgumentNullException(nameof(contents));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalCreate(path, useAsync: false);

            InternalWriteAllLines(new StreamWriter(stream, encoding), contents);
        }

        private static void InternalWriteAllLines(TextWriter writer, IEnumerable<String> contents)
        {
            Contract.Requires(writer != null);
            Contract.Requires(contents != null);

            using (writer) {
                foreach (String line in contents) {
                    writer.WriteLine(line);
                }
            }
        }


        /// <summary>Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.</summary>
        /// <param name="path">The file to append the specified string to. </param>
        /// <param name="contents">The string to append to the file. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn’t exist or it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void AppendAllText(String path, String contents)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            InternalAppendAllText(path, contents, UTF8NoBOM);
        }

        /// <summary>Appends the specified string to the file, creating the file if it does not already exist.</summary>
        /// <param name="path">The file to append the specified string to. </param>
        /// <param name="contents">The string to append to the file. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn’t exist or it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void AppendAllText(String path, String contents, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            InternalAppendAllText(path, contents, encoding);
        }

        private static void InternalAppendAllText(String path, String contents, Encoding encoding)
        {
            Contract.Requires(path != null);
            Contract.Requires(encoding != null);
            Contract.Requires(path.Length > 0);

            Stream stream = FileStream.InternalAppend(path, useAsync: false);

            using (StreamWriter sw = new StreamWriter(stream, encoding))
                sw.Write(contents);
        }

        /// <summary>Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.</summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">Either<paramref name=" path " />or <paramref name="contents" /> is null.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid (for example, the directory doesn’t exist or it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.IO.PathTooLongException"><paramref name="path" /> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have permission to write to the file.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path" /> is a directory.</exception>
        public static void AppendAllLines(String path, IEnumerable<String> contents)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (contents == null)
                throw new ArgumentNullException(nameof(contents));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalAppend(path, useAsync: false);

            InternalWriteAllLines(new StreamWriter(stream, UTF8NoBOM), contents);
        }

        /// <summary>Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.</summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">Either<paramref name=" path" />, <paramref name="contents" />, or <paramref name="encoding" /> is null.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid (for example, the directory doesn’t exist or it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.IO.PathTooLongException"><paramref name="path" /> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path" /> is a directory.-or-The caller does not have the required permission.</exception>
        public static void AppendAllLines(String path, IEnumerable<String> contents, Encoding encoding)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (contents == null)
                throw new ArgumentNullException(nameof(contents));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyPath, nameof(path));
            Contract.EndContractBlock();

            Stream stream = FileStream.InternalAppend(path, useAsync: false);

            InternalWriteAllLines(new StreamWriter(stream, encoding), contents);
        }

        // Moves a specified file to a new location and potentially a new file name.
        // This method does work across volumes.
        //
        // The caller must have certain FileIOPermissions.  The caller must
        // have Read and Write permission to 
        // sourceFileName and Write 
        // permissions to destFileName.
        // 
        /// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
        /// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path.</param>
        /// <param name="destFileName">The new path and name for the file.</param>
        /// <exception cref="T:System.IO.IOException">The destination file already exists.-or-<paramref name="sourceFileName" /> was not found. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains invalid characters as defined in <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid, (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static void Move(String sourceFileName, String destFileName)
        {
            if (sourceFileName == null)
                throw new ArgumentNullException(nameof(sourceFileName), SR.ArgumentNull_FileName);
            if (destFileName == null)
                throw new ArgumentNullException(nameof(destFileName), SR.ArgumentNull_FileName);
            if (sourceFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(sourceFileName));
            if (destFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destFileName));
            Contract.EndContractBlock();

            String fullSourceFileName = Path.GetFullPath(sourceFileName);
            String fullDestFileName = Path.GetFullPath(destFileName);

            if (!InternalExists(fullSourceFileName)) {
                throw new FileNotFoundException(SR.Format(SR.IO_FileNotFound_FileName, fullSourceFileName), fullSourceFileName);
            }

            FileSystem.Current.MoveFile(fullSourceFileName, fullDestFileName);
        }

        private static volatile Encoding _UTF8NoBOM;

        private static Encoding UTF8NoBOM
        {
            get {
                if (_UTF8NoBOM == null) {
                    // No need for double lock - we just want to avoid extra
                    // allocations in the common case.
                    UTF8Encoding noBOM = new UTF8Encoding(false, true);
                    Interlocked.MemoryBarrier();
                    _UTF8NoBOM = noBOM;
                }
                return _UTF8NoBOM;
            }
        }
    }
}
