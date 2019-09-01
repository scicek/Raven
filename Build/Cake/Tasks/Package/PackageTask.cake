Task("Package")
    .Description("Packages the output.")
    .Does(() => 
    {
		var packageStage = StageDirectory + Directory("/PackageStage");    	
		EnsureDirectoryExists(packageStage);
    	CopyFiles($"{ConfigurationOutputDirectory}/**/*", packageStage, true);

		DeleteFiles($"{packageStage}/*.pdb");

	    var fileName = $"{PackageOutputDirectory}/Raven_{Version.Short}.zip";
		EnsureDirectoryExists(PackageOutputDirectory);
	    Zip(packageStage, fileName);
	    Information($"Created zip file \"{fileName}\".");

	    DeleteDirectory(packageStage, new DeleteDirectorySettings { Force = true, Recursive = true });
	});