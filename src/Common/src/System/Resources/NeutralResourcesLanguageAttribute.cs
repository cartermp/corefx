// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Resources
{
    // Intentionally excluding visibility so it defaults to internal except for
    // the one public version in System.Resources.ResourceManager which defines
    // another version of this partial class with the public visibility 
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    sealed partial class NeutralResourcesLanguageAttribute : Attribute
    {
        private readonly string _culture;

        /// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class.</summary>
        /// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in. </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="cultureName" /> parameter is null. </exception>
        public NeutralResourcesLanguageAttribute(string cultureName)
        {
            if (cultureName == null)
                throw new ArgumentNullException(nameof(cultureName));

            _culture = cultureName;
        }

        /// <summary>Gets the culture name.</summary>
        /// <returns>The name of the default culture for the main assembly.</returns>
        public string CultureName
        {
            get { return _culture; }
        }
    }
}
