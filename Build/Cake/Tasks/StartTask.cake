public IList<string> RequiredDirectories { get; set; } = new List<string>
{
    StageDirectory.ToString()
};

Task("Start")
    .Description("Starts the build.")
    .IsDependentOn("GenerateVersion")
    .Does(() =>
    {
        if (Solution == null)
        {
            Error("Could not find a solution (.sln) for the project!");
            return;
        }

        BuildConfiguration = GetBuildConfigFromBuildTargets();
        Information($"Build configuration: {BuildConfiguration}");

        if (IsOnBuildMachine)
        {
            TFBuild.Commands.SetVariable("Build.Configuration", BuildConfiguration.ToString());
        }

        foreach(var directory in RequiredDirectories)
        {
            Verbose($"Ensuring {directory} exists...");
            EnsureDirectoryExists(directory);
        }

        CreateAssemblyInfo(File($"{StageDirectory}/AssemblyInfo.cs"), new AssemblyInfoSettings 
        {
            Version = Version.Short,
            FileVersion = Version.Short,
            InformationalVersion = Version.Short,
            Copyright = $"Copyright © Simon Cicek 2019 - {DateTime.Now.Year}",
            CLSCompliant = false
        });

        foreach (var target in BuildTargets)
        {
            Verbose($"Running target: {target}");
            RunTarget(target);
        }
    });

public Configuration GetBuildConfigFromBuildTargets()
{
    foreach (var target in BuildTargets)
    {
        Verbose($"Checking if target: '{target}' matches a build configuration...");

        Configuration config;
        if (Enum.TryParse(target, true, out config))
        {
            Verbose($"Found matching build configuration: {config}.");
            return config;
        }
    }

    var defaultConfig = default(Configuration);

    Verbose($"Found no matching build configuration, defaulting to: {defaultConfig}.");
    return defaultConfig;
}