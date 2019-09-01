public string VersionFile = MakeAbsolute(File($"{BuildDirectory}/Version.xml")).FullPath;
public uint DefaultBuildNumber = 65534;

Task("GenerateVersion")
    .Description("Generates the version number.")
    .Does(() => 
    {
	    VersionNumber version = null;

	    try
	    {
	        version = GetVersionFromFile(VersionFile);
	    }
	    catch(Exception e)
	    {
	        Error($"Failed to get version from file: {VersionFile}");
	        Error(e.Message);

	        version = new VersionNumber(string.Empty, 0, 0, 0, DefaultBuildNumber, string.Empty);
	    }

	    if (IsOnBuildMachine)
	    {
	        version.Build =  (uint) TFBuild.Environment.Build.Id;
	    }

        Version = version;
        Information($"Version: {Version}");

        if (IsOnBuildMachine)
	    {
	        TFBuild.Commands.UpdateBuildNumber(Version.Short);

	        TFBuild.Commands.SetVariable("Version.Prefix", Version.Prefix);
	        TFBuild.Commands.SetVariable("Version.Major", Version.Major.ToString());
	        TFBuild.Commands.SetVariable("Version.Minor", Version.Minor.ToString());
	        TFBuild.Commands.SetVariable("Version.Patch", Version.Patch.ToString());
	        TFBuild.Commands.SetVariable("Version.Build", Version.Build.ToString());
	        TFBuild.Commands.SetVariable("Version.Suffix", Version.Suffix);
	        TFBuild.Commands.SetVariable("Version.Short", Version.Short);
	        TFBuild.Commands.SetVariable("Version.SemVer", Version.SemVer);
	        TFBuild.Commands.SetVariable("Version.Full", Version.Full);
	    }
    });

public VersionNumber GetVersionFromFile(string file)
{
    var version = new VersionNumber(string.Empty, 0, 0, 0, DefaultBuildNumber, string.Empty);
    var xmlPeekSettings = new XmlPeekSettings { SuppressWarning = true };

    version.Prefix = XmlPeek(file, $"/Version/{nameof(VersionNumber.Prefix)}/text()", xmlPeekSettings);

    var majorAsString = XmlPeek(file, $"/Version/{nameof(VersionNumber.Major)}/text()", xmlPeekSettings);

    if (string.IsNullOrWhiteSpace(majorAsString))
    {
        throw new Exception($"The version file: {file} is missing a 'Major'-component!");
    }

    if (uint.TryParse(majorAsString, out uint major))
    {
        version.Major = major;
    }
    else
    {
        throw new Exception($"The version file: {file} has an invalid (not an uint) 'Major'-component!");
    }

    var minorAsString = XmlPeek(file, $"/Version/{nameof(VersionNumber.Minor)}/text()", xmlPeekSettings);

    if (string.IsNullOrWhiteSpace(minorAsString))
    {
        throw new Exception($"The version file: {file} is missing a 'Minor'-component!");
    }

    if (uint.TryParse(minorAsString, out uint minor))
    {
        version.Minor = minor;
    }
    else
    {
        throw new Exception($"The version file: {file} has an invalid (not an uint) 'Minor'-component!");
    }

    var patchAsString = XmlPeek(file, $"/Version/{nameof(VersionNumber.Patch)}/text()", xmlPeekSettings);

    if (string.IsNullOrWhiteSpace(patchAsString))
    {
        throw new Exception($"The version file: {file} is missing a 'Patch'-component!");
    }

    if (uint.TryParse(patchAsString, out uint patch))
    {
        version.Patch = patch;
    }
    else
    {
        throw new Exception($"The version file: {file} has an invalid (not an uint) 'Patch'-component!");
    }

    version.Suffix = XmlPeek(file, $"/Version/{nameof(VersionNumber.Suffix)}/text()", xmlPeekSettings);

    return version;
}