using System;

namespace ImaginaryCompany.Web.Data.Models
{
    /// <summary>
    /// This class stores a version number in semver format [see semver.org for more info].
    /// If the whole or parts of the version can't be parsed from the string, all zeros are returned instead of something invalid.
    /// </summary>
    public class SoftwareVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }

        public SoftwareVersion() { }
        public SoftwareVersion(string version)
        {
            //we can't process an empty string, so we'll just leave all the values zero
            if (string.IsNullOrEmpty(version)) return;

            //if there's no period, then we will assume we only received major value
            if (!version.Contains(".", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(version, out int maj))
                {
                    Major = maj;
                }
            }
            else
            {
                //the version contains at least one period, so we can parse it
                var versionParts = version.Split(".");
                if (versionParts.Length == 0) return;

                for (var i = 0; i < versionParts.Length; i++)
                {
                    if(int.TryParse(versionParts[i], out int part))
                    {
                        switch(i)
                        {
                            case 0: 
                                Major = part;
                                break;
                            case 1:
                                Minor = part;
                                break;
                            case 2:
                                Patch = part;
                                break;
                        }
                    }
                    else
                    {
                        //one of the parts didn't parse to a number, so we need to clear all and return
                        Major = 0;
                        Minor = 0;
                        Patch = 0;
                        return;
                    }
                }
            }
        }

        public string ToFormattedString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        public bool IsGreater(SoftwareVersion v)
        {
            if(Major == v.Major)
            {
                if(Minor == v.Minor)
                {
                    return Patch > v.Patch;
                }
                else
                {
                    return Minor > v.Minor;
                }
            }
            else
            {
                return Major > v.Major;
            }
        }
    }
}
