// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace System.IO
{
    // Class for creating FileStream objects, and some basic file management
    // routines such as Delete, etc.
    /// <summary>Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects. This class cannot be inherited.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
    /// <filterpriority>1</filterpriority>
    public sealed partial class FileInfo : FileSystemInfo
    {
        private String _name;

        [System.Security.SecurityCritical]
        private FileInfo() { }

        /// <summary>Initializes a new instance of the <see cref="T:System.IO.FileInfo" /> class, which acts as a wrapper for a file path.</summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="fileName" /> is null. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="fileName" /> is denied. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="fileName" /> contains a colon (:) in the middle of the string. </exception>
        [System.Security.SecuritySafeCritical]
        public FileInfo(String fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));
            Contract.EndContractBlock();

            Init(fileName);
        }

        [System.Security.SecurityCritical]
        private void Init(String fileName)
        {
            OriginalPath = fileName;
            // Must fully qualify the path for the security check
            String fullPath = Path.GetFullPath(fileName);

            _name = Path.GetFileName(fileName);
            FullPath = fullPath;
            DisplayPath = GetDisplayPath(fileName);
        }

        private String GetDisplayPath(String originalPath)
        {
            return originalPath;
        }

        [System.Security.SecuritySafeCritical]
        internal FileInfo(String fullPath, String originalPath)
        {
            Debug.Assert(Path.IsPathRooted(fullPath), "fullPath must be fully qualified!");
            _name = originalPath ?? Path.GetFileName(fullPath);
            OriginalPath = _name;
            FullPath = fullPath;
            DisplayPath = _name;
        }

        /// <summary>Gets the name of the file.</summary>
        /// <returns>The name of the file.</returns>
        /// <filterpriority>1</filterpriority>
        public override String Name
        {
            get { return _name; }
        }


        /// <summary>Gets the size, in bytes, of the current file.</summary>
        /// <returns>The size of the current file in bytes.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot update the state of the file or directory. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.-or- The Length property is called for a directory. </exception>
        /// <filterpriority>1</filterpriority>
        public long Length
        {
            [System.Security.SecuritySafeCritical]  // auto-generated
            get {
                if ((FileSystemObject.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    throw new FileNotFoundException(SR.Format(SR.IO_FileNotFound_FileName, DisplayPath), DisplayPath);
                }
                return FileSystemObject.Length;
            }
        }

        /* Returns the name of the directory that the file is in */
        /// <summary>Gets a string representing the directory's full path.</summary>
        /// <returns>A string representing the directory's full path.</returns>
        /// <exception cref="T:System.ArgumentNullException">null was passed in for the directory name. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public String DirectoryName
        {
            [System.Security.SecuritySafeCritical]
            get {
                return Path.GetDirectoryName(FullPath);
            }
        }

        /* Creates an instance of the the parent directory */
        /// <summary>Gets an instance of the parent directory.</summary>
        /// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object representing the parent directory of this file.</returns>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DirectoryInfo Directory
        {
            get {
                String dirName = DirectoryName;
                if (dirName == null)
                    return null;
                return new DirectoryInfo(dirName);
            }
        }

        /// <summary>Gets or sets a value that determines if the current file is read only.</summary>
        /// <returns>true if the current file is read only; otherwise, false.</returns>
        /// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
        /// <exception cref="T:System.ArgumentException">The user does not have write permission, but attempted to set this property to false.</exception>
        /// <filterpriority>1</filterpriority>
        public bool IsReadOnly
        {
            get {
                return (Attributes & FileAttributes.ReadOnly) != 0;
            }
            set {
                if (value)
                    Attributes |= FileAttributes.ReadOnly;
                else
                    Attributes &= ~FileAttributes.ReadOnly;
            }
        }

        /// <summary>Creates a <see cref="T:System.IO.StreamReader" /> with UTF8 encoding that reads from an existing text file.</summary>
        /// <returns>A new StreamReader with UTF8 encoding.</returns>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> is read-only or is a directory. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public StreamReader OpenText()
        {
            Stream stream = FileStream.InternalOpen(FullPath);
            return new StreamReader(stream, Encoding.UTF8, true);
        }

        /// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that writes a new text file.</summary>
        /// <returns>A new StreamWriter.</returns>
        /// <exception cref="T:System.UnauthorizedAccessException">The file name is a directory. </exception>
        /// <exception cref="T:System.IO.IOException">The disk is read-only. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public StreamWriter CreateText()
        {
            Stream stream = FileStream.InternalCreate(FullPath);
            return new StreamWriter(stream);
        }

        /// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that appends text to the file represented by this instance of the <see cref="T:System.IO.FileInfo" />.</summary>
        /// <returns>A new StreamWriter.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public StreamWriter AppendText()
        {
            Stream stream = FileStream.InternalAppend(FullPath);
            return new StreamWriter(stream);
        }


        // Copies an existing file to a new file. An exception is raised if the
        // destination file already exists. Use the 
        // Copy(String, String, boolean) method to allow 
        // overwriting an existing file.
        //
        // The caller must have certain FileIOPermissions.  The caller must have
        // Read permission to sourceFileName 
        // and Write permissions to destFileName.
        // 
        /// <summary>Copies an existing file to a new file, disallowing the overwriting of an existing file.</summary>
        /// <returns>A new file with a fully qualified path.</returns>
        /// <param name="destFileName">The name of the new file to copy to. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters. </exception>
        /// <exception cref="T:System.IO.IOException">An error occurs, or the destination file already exists. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="destFileName" /> is null. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="destFileName" /> contains a colon (:) within the string but does not specify the volume. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileInfo CopyTo(String destFileName)
        {
            if (destFileName == null)
                throw new ArgumentNullException(nameof(destFileName), SR.ArgumentNull_FileName);
            if (destFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destFileName));
            Contract.EndContractBlock();

            destFileName = File.InternalCopy(FullPath, destFileName, false);
            return new FileInfo(destFileName, null);
        }


        // Copies an existing file to a new file. If overwrite is 
        // false, then an IOException is thrown if the destination file 
        // already exists.  If overwrite is true, the file is 
        // overwritten.
        //
        // The caller must have certain FileIOPermissions.  The caller must have
        // Read permission to sourceFileName and Create
        // and Write permissions to destFileName.
        // 
        /// <summary>Copies an existing file to a new file, allowing the overwriting of an existing file.</summary>
        /// <returns>A new file, or an overwrite of an existing file if <paramref name="overwrite" /> is true. If the file exists and <paramref name="overwrite" /> is false, an <see cref="T:System.IO.IOException" /> is thrown.</returns>
        /// <param name="destFileName">The name of the new file to copy to. </param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters. </exception>
        /// <exception cref="T:System.IO.IOException">An error occurs, or the destination file already exists and <paramref name="overwrite" /> is false. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="destFileName" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="destFileName" /> contains a colon (:) in the middle of the string. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileInfo CopyTo(String destFileName, bool overwrite)
        {
            if (destFileName == null)
                throw new ArgumentNullException(nameof(destFileName), SR.ArgumentNull_FileName);
            if (destFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destFileName));
            Contract.EndContractBlock();

            destFileName = File.InternalCopy(FullPath, destFileName, overwrite);
            return new FileInfo(destFileName, null);
        }

        /// <summary>Creates a file.</summary>
        /// <returns>A new file.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileStream Create()
        {
            return File.Create(FullPath);
        }

        // Deletes a file. The file specified by the designated path is deleted. 
        // If the file does not exist, Delete succeeds without throwing
        // an exception.
        // 
        // On NT, Delete will fail for a file that is open for normal I/O
        // or a file that is memory mapped.  On Win95, the file will be 
        // deleted irregardless of whether the file is being used.
        // 
        // Your application must have Delete permission to the target file.
        // 
        /// <summary>Permanently deletes a file.</summary>
        /// <exception cref="T:System.IO.IOException">The target file is open or memory-mapped on a computer running Microsoft Windows NT.-or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The path is a directory. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public override void Delete()
        {
            FileSystem.Current.DeleteFile(FullPath);
        }

        // Tests if the given file exists. The result is true if the file
        // given by the specified path exists; otherwise, the result is
        // false.  
        //
        // Your application must have Read permission for the target directory.
        /// <summary>Gets a value indicating whether a file exists.</summary>
        /// <returns>true if the file exists; false if the file does not exist or if the file is a directory.</returns>
        /// <filterpriority>1</filterpriority>
        public override bool Exists
        {
            [System.Security.SecuritySafeCritical]  // auto-generated
            get {
                try {
                    return FileSystemObject.Exists;
                }
                catch {
                    return false;
                }
            }
        }

        // User must explicitly specify opening a new file or appending to one.
        /// <summary>Opens a file in the specified mode.</summary>
        /// <returns>A file opened in the specified mode, with read/write access and unshared.</returns>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, Open or Append) in which to open the file. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The file is read-only or is a directory. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file is already open. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileStream Open(FileMode mode)
        {
            return Open(mode, (mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite), FileShare.None);
        }

        /// <summary>Opens a file in the specified mode with read, write, or read/write access.</summary>
        /// <returns>A <see cref="T:System.IO.FileStream" /> object opened in the specified mode and access, and unshared.</returns>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, Open or Append) in which to open the file. </param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant specifying whether to open the file with Read, Write, or ReadWrite file access. </param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> is read-only or is a directory. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file is already open. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileStream Open(FileMode mode, FileAccess access)
        {
            return Open(mode, access, FileShare.None);
        }

        /// <summary>Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.</summary>
        /// <returns>A <see cref="T:System.IO.FileStream" /> object opened with the specified mode, access, and sharing options.</returns>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, Open or Append) in which to open the file. </param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant specifying whether to open the file with Read, Write, or ReadWrite file access. </param>
        /// <param name="share">A <see cref="T:System.IO.FileShare" /> constant specifying the type of access other FileStream objects have to this file. </param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> is read-only or is a directory. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file is already open. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStream(FullPath, mode, access, share);
        }


        /// <summary>Creates a read-only <see cref="T:System.IO.FileStream" />.</summary>
        /// <returns>A new read-only <see cref="T:System.IO.FileStream" /> object.</returns>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> is read-only or is a directory. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file is already open. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public FileStream OpenRead()
        {
            return new FileStream(FullPath, FileMode.Open, FileAccess.Read,
                                  FileShare.Read, 4096, false);
        }


        /// <summary>Creates a write-only <see cref="T:System.IO.FileStream" />.</summary>
        /// <returns>A write-only unshared <see cref="T:System.IO.FileStream" /> object for a new or existing file.</returns>
        /// <exception cref="T:System.UnauthorizedAccessException">The path specified when creating an instance of the <see cref="T:System.IO.FileInfo" /> object is read-only or is a directory.  </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified when creating an instance of the <see cref="T:System.IO.FileInfo" /> object is invalid, such as being on an unmapped drive. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileStream OpenWrite()
        {
            return new FileStream(FullPath, FileMode.OpenOrCreate,
                                  FileAccess.Write, FileShare.None);
        }

        // Moves a given file to a new location and potentially a new file name.
        // This method does work across volumes.
        //
        // The caller must have certain FileIOPermissions.  The caller must
        // have Read and Write permission to 
        // sourceFileName and Write 
        // permissions to destFileName.
        // 
        /// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
        /// <param name="destFileName">The path to move the file to, which can specify a different file name. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the destination file already exists or the destination device is not ready. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="destFileName" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="destFileName" /> is read-only or is a directory. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="destFileName" /> contains a colon (:) in the middle of the string. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public void MoveTo(String destFileName)
        {
            if (destFileName == null)
                throw new ArgumentNullException(nameof(destFileName));
            if (destFileName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destFileName));
            Contract.EndContractBlock();

            String fullDestFileName = Path.GetFullPath(destFileName);

            // These checks are in place to ensure Unix error throwing happens the same way
            // as it does on Windows.These checks can be removed if a solution to #2460 is
            // found that doesn't require validity checks before making an API call.
            if (!new DirectoryInfo(Path.GetDirectoryName(FullName)).Exists) {
                throw new DirectoryNotFoundException(SR.Format(SR.IO_PathNotFound_Path, FullName));
            }
            if (!Exists) {
                throw new FileNotFoundException(SR.Format(SR.IO_FileNotFound_FileName, FullName), FullName);
            }

            FileSystem.Current.MoveFile(FullPath, fullDestFileName);

            FullPath = fullDestFileName;
            OriginalPath = destFileName;
            _name = Path.GetFileName(fullDestFileName);
            DisplayPath = GetDisplayPath(destFileName);
            // Flush any cached information about the file.
            Invalidate();
        }

        // Returns the display path
        /// <summary>Returns the path as a string.</summary>
        /// <returns>A string representing the path.</returns>
        /// <filterpriority>1</filterpriority>
        public override String ToString()
        {
            return DisplayPath;
        }
    }
}
