// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Security;
using Microsoft.Win32;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Diagnostics.Contracts;

namespace System.IO
{
    /// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
    /// <filterpriority>2</filterpriority>
    public abstract partial class FileSystemInfo
    {
        /// <summary>Represents the fully qualified path of the directory or file.</summary>
        /// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
        protected String FullPath;          // fully qualified path of the file or directory
                                            /// <summary>The path originally specified by the user, whether relative or absolute.</summary>
        protected String OriginalPath;      // path passed in by the user
        private String _displayPath = "";   // path that can be displayed to the user

        /// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class.</summary>
        [System.Security.SecurityCritical]
        protected FileSystemInfo()
        {
        }

        // Full path of the directory/file
        /// <summary>Gets the full path of the directory or file.</summary>
        /// <returns>A string containing the full path.</returns>
        /// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name is 260 or more characters.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public virtual String FullName
        {
            [System.Security.SecuritySafeCritical]
            get {
                return FullPath;
            }
        }

        /// <summary>Gets the string representing the extension part of the file.</summary>
        /// <returns>A string containing the <see cref="T:System.IO.FileSystemInfo" /> extension.</returns>
        /// <filterpriority>1</filterpriority>
        public String Extension
        {
            get {
                // GetFullPathInternal would have already stripped out the terminating "." if present.
                int length = FullPath.Length;
                for (int i = length; --i >= 0;) {
                    char ch = FullPath[i];
                    if (ch == '.')
                        return FullPath.Substring(i, length - i);
                    if (PathInternal.IsDirectorySeparator(ch) || ch == Path.VolumeSeparatorChar)
                        break;
                }
                return String.Empty;
            }
        }

        // For files name of the file is returned, for directories the last directory in hierarchy is returned if possible,
        // otherwise the fully qualified name s returned
        /// <summary>For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists. Otherwise, the Name property gets the name of the directory.</summary>
        /// <returns>A string that is the name of the parent directory, the name of the last directory in the hierarchy, or the name of a file, including the file name extension.</returns>
        /// <filterpriority>1</filterpriority>
        public abstract String Name
        {
            get;
        }

        // Whether a file/directory exists
        /// <summary>Gets a value indicating whether the file or directory exists.</summary>
        /// <returns>true if the file or directory exists; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public abstract bool Exists
        {
            get;
        }

        // Delete a file/directory
        /// <summary>Deletes a file or directory.</summary>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="T:System.IO.IOException">There is an open handle on the file or directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <filterpriority>2</filterpriority>
        public abstract void Delete();

        /// <summary>Gets or sets the creation time of the current file or directory.</summary>
        /// <returns>The creation date and time of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DateTime CreationTime
        {
            get {
                // depends on the security check in get_CreationTimeUtc
                return CreationTimeUtc.ToLocalTime();
            }
            set {
                CreationTimeUtc = value.ToUniversalTime();
            }
        }

        /// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
        /// <returns>The creation date and time in UTC format of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DateTime CreationTimeUtc
        {
            [System.Security.SecuritySafeCritical]
            get {
                return FileSystemObject.CreationTime.UtcDateTime;
            }

            set {
                FileSystemObject.CreationTime = File.GetUtcDateTimeOffset(value);
            }
        }


        /// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
        /// <returns>The time that the current file or directory was last accessed.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DateTime LastAccessTime
        {
            get {
                // depends on the security check in get_LastAccessTimeUtc
                return LastAccessTimeUtc.ToLocalTime();
            }
            set {
                LastAccessTimeUtc = value.ToUniversalTime();
            }
        }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
        /// <returns>The UTC time that the current file or directory was last accessed.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DateTime LastAccessTimeUtc
        {
            [System.Security.SecuritySafeCritical]
            get {
                return FileSystemObject.LastAccessTime.UtcDateTime;
            }

            set {
                FileSystemObject.LastAccessTime = File.GetUtcDateTimeOffset(value);
            }
        }

        /// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
        /// <returns>The time the current file was last written.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DateTime LastWriteTime
        {
            get {
                // depends on the security check in get_LastWriteTimeUtc
                return LastWriteTimeUtc.ToLocalTime();
            }

            set {
                LastWriteTimeUtc = value.ToUniversalTime();
            }
        }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
        /// <returns>The UTC time when the current file was last written to.</returns>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public DateTime LastWriteTimeUtc
        {
            [System.Security.SecuritySafeCritical]
            get {
                return FileSystemObject.LastWriteTime.UtcDateTime;
            }

            set {
                FileSystemObject.LastWriteTime = File.GetUtcDateTimeOffset(value);
            }
        }

        /// <summary>Refreshes the state of the object.</summary>
        /// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready. </exception>
        /// <filterpriority>1</filterpriority>
        public void Refresh()
        {
            FileSystemObject.Refresh();
        }

        /// <summary>Gets or sets the attributes for the current file or directory.</summary>
        /// <returns><see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:System.IO.FileSystemInfo" />.</returns>
        /// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentException">The caller attempts to set an invalid file attribute. -or-The user attempts to set an attribute value but does not have write permission.</exception>
        /// <exception cref="T:System.IO.IOException"><see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />      </PermissionSet>
        public FileAttributes Attributes
        {
            [System.Security.SecuritySafeCritical]
            get {
                return FileSystemObject.Attributes;
            }
            [System.Security.SecurityCritical] // auto-generated
            set {
                FileSystemObject.Attributes = value;
            }
        }

        internal String DisplayPath
        {
            get {
                return _displayPath;
            }
            set {
                _displayPath = value;
            }
        }
    }
}
