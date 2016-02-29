// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Runtime.Versioning;
using System.Diagnostics.Contracts;
using System.Threading;

namespace System.IO
{
    /// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
    /// <filterpriority>1</filterpriority>
    public static class Directory
    {
        /// <summary>Retrieves the parent directory of the specified path, including both absolute and relative paths.</summary>
        /// <returns>The parent directory, or null if <paramref name="path" /> is the root directory, including the root of a UNC server or share name.</returns>
        /// <param name="path">The path for which to retrieve the parent directory. </param>
        /// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path was not found. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DirectoryInfo GetParent(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_PathEmpty, nameof(path));
            Contract.EndContractBlock();

            String fullPath = Path.GetFullPath(path);

            String s = Path.GetDirectoryName(fullPath);
            if (s == null)
                return null;
            return new DirectoryInfo(s);
        }

        /// <summary>Creates all directories and subdirectories in the specified path unless they already exist.</summary>
        /// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</returns>
        /// <param name="path">The directory to create. </param>
        /// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.-or-The network name is not known.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.-or-<paramref name="path" /> is prefixed with, or contains, only a colon character (:).</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static DirectoryInfo CreateDirectory(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_PathEmpty, nameof(path));
            Contract.EndContractBlock();

            String fullPath = Path.GetFullPath(path);

            FileSystem.Current.CreateDirectory(fullPath);

            return new DirectoryInfo(fullPath, null);
        }

        // Input to this method should already be fullpath. This method will ensure that we append 
        // the trailing slash only when appropriate.
        internal static String EnsureTrailingDirectorySeparator(string fullPath)
        {
            String fullPathWithTrailingDirectorySeparator;

            if (!PathHelpers.EndsInDirectorySeparator(fullPath))
                fullPathWithTrailingDirectorySeparator = fullPath + PathHelpers.DirectorySeparatorCharAsString;
            else
                fullPathWithTrailingDirectorySeparator = fullPath;

            return fullPathWithTrailingDirectorySeparator;
        }


        // Tests if the given path refers to an existing DirectoryInfo on disk.
        // 
        // Your application must have Read permission to the directory's
        // contents.
        //
        /// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
        /// <returns>true if <paramref name="path" /> refers to an existing directory; false if the directory does not exist or an error occurs when trying to determine if the specified file exists.</returns>
        /// <param name="path">The path to test. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]  // auto-generated
        public static bool Exists(String path)
        {
            try {
                if (path == null)
                    return false;
                if (path.Length == 0)
                    return false;

                String fullPath = Path.GetFullPath(path);

                return FileSystem.Current.DirectoryExists(fullPath);
            }
            catch (ArgumentException) { }
            catch (NotSupportedException) { }  // Security can throw this on ":"
            catch (SecurityException) { }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }

            return false;
        }

        /// <summary>Sets the creation date and time for the specified file or directory.</summary>
        /// <param name="path">The file or directory for which to set the creation date and time information. </param>
        /// <param name="creationTime">The date and time the file or directory was last written to. This value is expressed in local time.</param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetCreationTime(String path, DateTime creationTime)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetCreationTime(fullPath, creationTime, asDirectory: true);
        }

        /// <summary>Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.</summary>
        /// <param name="path">The file or directory for which to set the creation date and time information. </param>
        /// <param name="creationTimeUtc">The date and time the directory or file was created. This value is expressed in local time.</param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetCreationTimeUtc(String path, DateTime creationTimeUtc)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetCreationTime(fullPath, File.GetUtcDateTimeOffset(creationTimeUtc), asDirectory: true);
        }

        /// <summary>Gets the creation date and time of a directory.</summary>
        /// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in local time.</returns>
        /// <param name="path">The path of the directory. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DateTime GetCreationTime(String path)
        {
            return File.GetCreationTime(path);
        }

        /// <summary>Gets the creation date and time, in Coordinated Universal Time (UTC) format, of a directory.</summary>
        /// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in UTC time.</returns>
        /// <param name="path">The path of the directory. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DateTime GetCreationTimeUtc(String path)
        {
            return File.GetCreationTimeUtc(path);
        }

        /// <summary>Sets the date and time a directory was last written to.</summary>
        /// <param name="path">The path of the directory. </param>
        /// <param name="lastWriteTime">The date and time the directory was last written to. This value is expressed in local time.  </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastWriteTime(String path, DateTime lastWriteTime)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastWriteTime(fullPath, lastWriteTime, asDirectory: true);
        }

        /// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.</summary>
        /// <param name="path">The path of the directory. </param>
        /// <param name="lastWriteTimeUtc">The date and time the directory was last written to. This value is expressed in UTC time. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastWriteTimeUtc(String path, DateTime lastWriteTimeUtc)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastWriteTime(fullPath, File.GetUtcDateTimeOffset(lastWriteTimeUtc), asDirectory: true);
        }

        /// <summary>Returns the date and time the specified file or directory was last written to.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain modification date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DateTime GetLastWriteTime(String path)
        {
            return File.GetLastWriteTime(path);
        }

        /// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last written to.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain modification date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DateTime GetLastWriteTimeUtc(String path)
        {
            return File.GetLastWriteTimeUtc(path);
        }

        /// <summary>Sets the date and time the specified file or directory was last accessed.</summary>
        /// <param name="path">The file or directory for which to set the access date and time information. </param>
        /// <param name="lastAccessTime">An object that contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in local time. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastAccessTime(String path, DateTime lastAccessTime)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastAccessTime(fullPath, lastAccessTime, asDirectory: true);
        }

        /// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
        /// <param name="path">The file or directory for which to set the access date and time information. </param>
        /// <param name="lastAccessTimeUtc">An object that  contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static void SetLastAccessTimeUtc(String path, DateTime lastAccessTimeUtc)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.SetLastAccessTime(fullPath, File.GetUtcDateTimeOffset(lastAccessTimeUtc), asDirectory: true);
        }

        /// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DateTime GetLastAccessTime(String path)
        {
            return File.GetLastAccessTime(path);
        }

        /// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static DateTime GetLastAccessTimeUtc(String path)
        {
            return File.GetLastAccessTimeUtc(path);
        }

        // Returns an array of filenames in the DirectoryInfo specified by path
        /// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) for the files in the specified directory, or an empty array if no files are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] GetFiles(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetFiles(path, "*", SearchOption.TopDirectoryOnly);
        }

        // Returns an array of Files in the current DirectoryInfo matching the 
        // given search pattern (ie, "*.txt").
        /// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern, or an empty array if no files are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="M:System.IO.Path.GetInvalidPathChars" />.-or- <paramref name="searchPattern" /> doesn't contain a valid pattern. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] GetFiles(String path, String searchPattern)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        // Returns an array of Files in the current DirectoryInfo matching the 
        // given search pattern (ie, "*.txt") and search option
        /// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option, or an empty array if no files are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. -or- <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> or <paramref name="searchpattern" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive). </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
        public static String[] GetFiles(String path, String searchPattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new ArgumentOutOfRangeException(nameof(searchOption), SR.ArgumentOutOfRange_Enum);
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetFiles(path, searchPattern, searchOption);
        }

        // Returns an array of Files in the current DirectoryInfo matching the 
        // given search pattern (ie, "*.txt") and search option
        private static String[] InternalGetFiles(String path, String searchPattern, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);

            return InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption);
        }

        // Returns an array of Directories in the current directory.
        /// <summary>Returns the names of subdirectories (including their paths) in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) of subdirectories in the specified path, or an empty array if no directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] GetDirectories(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetDirectories(path, "*", SearchOption.TopDirectoryOnly);
        }

        // Returns an array of Directories in the current DirectoryInfo matching the 
        // given search criteria (ie, "*.txt").
        /// <summary>Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern in the specified directory, or an empty array if no directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters (see Remarks), but doesn't support regular expressions. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="M:System.IO.Path.GetInvalidPathChars" />.-or- <paramref name="searchPattern" /> doesn't contain a valid pattern. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] GetDirectories(String path, String searchPattern)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        // Returns an array of Directories in the current DirectoryInfo matching the 
        // given search criteria (ie, "*.txt").
        /// <summary>Returns the names of the subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.</summary>
        /// <returns>An array of the full names (including paths) of the subdirectories that match the specified criteria, or an empty array if no directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        public static String[] GetDirectories(String path, String searchPattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new ArgumentOutOfRangeException(nameof(searchOption), SR.ArgumentOutOfRange_Enum);
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetDirectories(path, searchPattern, searchOption);
        }

        // Returns an array of Directories in the current DirectoryInfo matching the 
        // given search criteria (ie, "*.txt").
        private static String[] InternalGetDirectories(String path, String searchPattern, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
            Contract.Ensures(Contract.Result<String[]>() != null);

            return InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption);
        }

        // Returns an array of strongly typed FileSystemInfo entries in the path
        /// <summary>Returns the names of all files and subdirectories in a specified path.</summary>
        /// <returns>An array of the names of files and subdirectories in the specified directory, or an empty array if no files or subdirectories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] GetFileSystemEntries(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
        }

        // Returns an array of strongly typed FileSystemInfo entries in the path with the
        // given search criteria (ie, "*.txt"). We disallow .. as a part of the search criteria
        /// <summary>Returns an array of file names and directory names that that match a search pattern in a specified path.</summary>
        /// <returns>An array of file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of file and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public static String[] GetFileSystemEntries(String path, String searchPattern)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        // Returns an array of strongly typed FileSystemInfo entries in the path with the
        // given search criteria (ie, "*.txt"). We disallow .. as a part of the search criteria
        /// <summary>Returns an array of all the file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An array of file the file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static String[] GetFileSystemEntries(String path, String searchPattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new ArgumentOutOfRangeException(nameof(searchOption), SR.ArgumentOutOfRange_Enum);
            Contract.Ensures(Contract.Result<String[]>() != null);
            Contract.EndContractBlock();

            return InternalGetFileSystemEntries(path, searchPattern, searchOption);
        }

        private static String[] InternalGetFileSystemEntries(String path, String searchPattern, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);

            return InternalGetFileDirectoryNames(path, path, searchPattern, true, true, searchOption);
        }

        // Returns fully qualified user path of dirs/files that matches the search parameters. 
        // For recursive search this method will search through all the sub dirs  and execute 
        // the given search criteria against every dir.
        // For all the dirs/files returned, it will then demand path discovery permission for 
        // their parent folders (it will avoid duplicate permission checks)
        internal static String[] InternalGetFileDirectoryNames(String path, String userPathOriginal, String searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(userPathOriginal != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);

            IEnumerable<String> enumerable = FileSystem.Current.EnumeratePaths(path, searchPattern, searchOption,
                (includeFiles ? SearchTarget.Files : 0) | (includeDirs ? SearchTarget.Directories : 0));
            return EnumerableHelpers.ToArray(enumerable);
        }

        /// <summary>Returns an enumerable collection of directory names in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" />.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateDirectories(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.EndContractBlock();

            return InternalEnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateDirectories(String path, String searchPattern)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            Contract.EndContractBlock();

            return InternalEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateDirectories(String path, String searchPattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new ArgumentOutOfRangeException(nameof(searchOption), SR.ArgumentOutOfRange_Enum);
            Contract.EndContractBlock();

            return InternalEnumerateDirectories(path, searchPattern, searchOption);
        }

        private static IEnumerable<String> InternalEnumerateDirectories(String path, String searchPattern, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);

            return EnumerateFileSystemNames(path, searchPattern, searchOption, false, true);
        }

        /// <summary>Returns an enumerable collection of file names in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" />.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateFiles(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);
            Contract.EndContractBlock();

            return InternalEnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateFiles(String path, String searchPattern)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);
            Contract.EndContractBlock();

            return InternalEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.  </param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateFiles(String path, String searchPattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new ArgumentOutOfRangeException(nameof(searchOption), SR.ArgumentOutOfRange_Enum);
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);
            Contract.EndContractBlock();

            return InternalEnumerateFiles(path, searchPattern, searchOption);
        }

        private static IEnumerable<String> InternalEnumerateFiles(String path, String searchPattern, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);

            return EnumerateFileSystemNames(path, searchPattern, searchOption, true, false);
        }

        /// <summary>Returns an enumerable collection of file names and directory names in a specified path. </summary>
        /// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" />.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateFileSystemEntries(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);
            Contract.EndContractBlock();

            return InternalEnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>Returns an enumerable collection of file names and directory names that  match a search pattern in a specified path.</summary>
        /// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive. </param>
        /// <param name="searchPattern">The search string to match against the names of file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.  </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateFileSystemEntries(String path, String searchPattern)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);
            Contract.EndContractBlock();

            return InternalEnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values  that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is a file name.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<String> EnumerateFileSystemEntries(String path, String searchPattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));
            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
                throw new ArgumentOutOfRangeException(nameof(searchOption), SR.ArgumentOutOfRange_Enum);
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);
            Contract.EndContractBlock();

            return InternalEnumerateFileSystemEntries(path, searchPattern, searchOption);
        }

        private static IEnumerable<String> InternalEnumerateFileSystemEntries(String path, String searchPattern, SearchOption searchOption)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);

            return EnumerateFileSystemNames(path, searchPattern, searchOption, true, true);
        }

        private static IEnumerable<String> EnumerateFileSystemNames(String path, String searchPattern, SearchOption searchOption,
                                                            bool includeFiles, bool includeDirs)
        {
            Contract.Requires(path != null);
            Contract.Requires(searchPattern != null);
            Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
            Contract.Ensures(Contract.Result<IEnumerable<String>>() != null);

            return FileSystem.Current.EnumeratePaths(path, searchPattern, searchOption,
                (includeFiles ? SearchTarget.Files : 0) | (includeDirs ? SearchTarget.Directories : 0));
        }

        /// <summary>Returns the volume information, root information, or both for the specified path.</summary>
        /// <returns>A string that contains the volume information, root information, or both for the specified path.</returns>
        /// <param name="path">The path of a file or directory. </param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static String GetDirectoryRoot(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            Contract.EndContractBlock();

            String fullPath = Path.GetFullPath(path);
            String root = fullPath.Substring(0, PathInternal.GetRootLength(fullPath));

            return root;
        }

        internal static String InternalGetDirectoryRoot(String path)
        {
            if (path == null) return null;
            return path.Substring(0, PathInternal.GetRootLength(path));
        }

        /*===============================CurrentDirectory===============================
       **Action:  Provides a getter and setter for the current directory.  The original
       **         current DirectoryInfo is the one from which the process was started.  
       **Returns: The current DirectoryInfo (from the getter).  Void from the setter.
       **Arguments: The current DirectoryInfo to which to switch to the setter.
       **Exceptions: 
       ==============================================================================*/
        /// <summary>Gets the current working directory of the application.</summary>
        /// <returns>A string that contains the path of the current working directory, and does not end with a backslash (\).</returns>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.NotSupportedException">The operating system is Windows CE, which does not have current directory functionality.This method is available in the .NET Compact Framework, but is not currently supported.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static String GetCurrentDirectory()
        {
            return FileSystem.Current.GetCurrentDirectory();
        }


        /// <summary>Sets the application's current working directory to the specified directory.</summary>
        /// <param name="path">The path to which the current working directory is set. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to access unmanaged code. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory was not found.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />      </PermissionSet>
        [System.Security.SecurityCritical] // auto-generated
        public static void SetCurrentDirectory(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException(SR.Argument_PathEmpty, nameof(path));
            Contract.EndContractBlock();
            if (PathInternal.IsPathTooLong(path))
                throw new PathTooLongException(SR.IO_PathTooLong);

            String fulldestDirName = Path.GetFullPath(path);

            FileSystem.Current.SetCurrentDirectory(fulldestDirName);
        }

        /// <summary>Moves a file or a directory and its contents to a new location.</summary>
        /// <param name="sourceDirName">The path of the file or directory to move. </param>
        /// <param name="destDirName">The path to the new location for <paramref name="sourceDirName" />. If <paramref name="sourceDirName" /> is a file, then <paramref name="destDirName" /> must also be a file name.</param>
        /// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume. -or- <paramref name="destDirName" /> already exists. -or- The <paramref name="sourceDirName" /> and <paramref name="destDirName" /> parameters refer to the same file or directory. -or-The directory or a file within it is being used by another process.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="sourceDirName" /> or <paramref name="destDirName" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="sourceDirName" /> or <paramref name="destDirName" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified by <paramref name="sourceDirName" /> is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static void Move(String sourceDirName, String destDirName)
        {
            if (sourceDirName == null)
                throw new ArgumentNullException(nameof(sourceDirName));
            if (sourceDirName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(sourceDirName));

            if (destDirName == null)
                throw new ArgumentNullException(nameof(destDirName));
            if (destDirName.Length == 0)
                throw new ArgumentException(SR.Argument_EmptyFileName, nameof(destDirName));
            Contract.EndContractBlock();

            String fullsourceDirName = Path.GetFullPath(sourceDirName);
            String sourcePath = EnsureTrailingDirectorySeparator(fullsourceDirName);

            if (PathInternal.IsDirectoryTooLong(sourcePath))
                throw new PathTooLongException(SR.IO_PathTooLong);

            String fulldestDirName = Path.GetFullPath(destDirName);
            String destPath = EnsureTrailingDirectorySeparator(fulldestDirName);
            if (PathInternal.IsDirectoryTooLong(destPath))
                throw new PathTooLongException(SR.IO_PathTooLong);

            StringComparison pathComparison = PathInternal.StringComparison;

            if (String.Equals(sourcePath, destPath, pathComparison))
                throw new IOException(SR.IO_SourceDestMustBeDifferent);

            String sourceRoot = Path.GetPathRoot(sourcePath);
            String destinationRoot = Path.GetPathRoot(destPath);
            if (!String.Equals(sourceRoot, destinationRoot, pathComparison))
                throw new IOException(SR.IO_SourceDestMustHaveSameRoot);

            FileSystem.Current.MoveDirectory(fullsourceDirName, fulldestDirName);
        }

        /// <summary>Deletes an empty directory from a specified path.</summary>
        /// <param name="path">The name of the empty directory to remove. This directory must be writable and empty. </param>
        /// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.-or-The directory is the application's current working directory.-or-The directory specified by <paramref name="path" /> is not empty.-or-The directory is read-only or contains a read-only file.-or-The directory is being used by another process.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> does not exist or could not be found.-or-The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static void Delete(String path)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.RemoveDirectory(fullPath, false);
        }

        /// <summary>Deletes the specified directory and, if indicated, any subdirectories and files in the directory. </summary>
        /// <param name="path">The name of the directory to remove. </param>
        /// <param name="recursive">true to remove directories, subdirectories, and files in <paramref name="path" />; otherwise, false. </param>
        /// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.-or-The directory specified by <paramref name="path" /> is read-only, or <paramref name="recursive" /> is false and <paramref name="path" /> is not an empty directory. -or-The directory is the application's current working directory. -or-The directory contains a read-only file.-or-The directory is being used by another process.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path" /> does not exist or could not be found.-or-The specified path is invalid (for example, it is on an unmapped drive). </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        [System.Security.SecuritySafeCritical]
        public static void Delete(String path, bool recursive)
        {
            String fullPath = Path.GetFullPath(path);
            FileSystem.Current.RemoveDirectory(fullPath, recursive);
        }
    }
}

