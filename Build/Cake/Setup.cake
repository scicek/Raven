public string[] BuildTargets => Argument("targets", "Void").Split(' ');

public Configuration BuildConfiguration { get; set; } = default(Configuration);

public Configuration? BuiltConfiguration { get; set; }

public FilePath Solution => GetFiles($"{RootDirectory}/*.sln").FirstOrDefault();

public bool IsOnBuildMachine => BuildSystem.IsRunningOnAzurePipelinesHosted || BuildSystem.IsRunningOnAzurePipelines;

public VersionNumber Version { get; set; }

public DirectoryPath RootDirectory => MakeAbsolute(Directory(@"..\.."));

public DirectoryPath BuildDirectory => GetPathRelativeToRoot("Build");

public DirectoryPath InstallerSourceDirectory => GetPathRelativeToRoot("Installer");

public DirectoryPath InstallerOutputDirectory => StageDirectory + Directory("Installer");

public DirectoryPath NuGetOutputDirectory => StageDirectory + Directory("NuGet");

public DirectoryPath SourceDirectory => GetPathRelativeToRoot("Source");

public DirectoryPath StageDirectory => GetPathRelativeToRoot("Stage");

public DirectoryPath ConfigurationOutputDirectory => StageDirectory + Directory($"/{BuildConfiguration.ToString()}");

private DirectoryPath GetPathRelativeToRoot(string directory)
{
	return RootDirectory + Directory($"/{directory}/");
}

#load local:?path=LoadScripts.cake