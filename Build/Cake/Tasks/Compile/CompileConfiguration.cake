public string Platform = "x86";
public FilePath CodeAnalysisRuleSetFile = File($"{BuildDirectory}/CodeAnalysis.ruleset");
public DirectoryPath CodeAnalysisOutputDirectory = Directory($"{StageDirectory}/CodeAnalysis");
public FilePath LogFile = File($"{StageDirectory}/msbuild.log");