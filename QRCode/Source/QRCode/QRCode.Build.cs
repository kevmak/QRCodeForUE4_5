using UnrealBuildTool;
using System;
using System.IO;

public class QRCode : ModuleRules
{
#if WITH_FORWARDED_MODULE_RULES_CTOR
	public QRCode(ReadOnlyTargetRules Target) : base(Target)
#else
    public QRCode(TargetInfo Target)
#endif
	{
        string QRCodeLibPath = string.Empty;
        string QRCodeInlucdePath = string.Empty;

        PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
				//"QRCodeLibrary",
				"Projects",
                "CoreUObject",
                "Engine"
				// ... add other public dependencies that you statically link with here ...
			}
			);
			
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				// ... add private dependencies that you statically link with here ...	
			}
			);
		
		
		DynamicallyLoadedModuleNames.AddRange(
			new string[]
			{
				// ... add any modules that your module loads dynamically here ...
			}
			);


       // string PluginPath = Utils.MakePathRelativeTo(ModuleDirectory, BuildConfiguration.RelativeEnginePath);
        string PluginPath = Utils.MakePathRelativeTo(ModuleDirectory, Target.RelativeEnginePath);
        string QRCodeThirdPartyDir = Path.GetFullPath(Path.Combine(ModuleDirectory, "../ThirdParty/QRCodeLibrary"));

        System.Console.WriteLine("-------------- PluginPath = " + PluginPath);

        

#if UE_5_0_OR_LATER
        if (Target.Platform == UnrealTargetPlatform.Win64)
#else
        if (Target.Platform == UnrealTargetPlatform.Win32 || Target.Platform == UnrealTargetPlatform.Win64)
#endif
        {
            
            QRCodeLibPath = Path.Combine(QRCodeThirdPartyDir, "lib");
            QRCodeInlucdePath = Path.Combine(QRCodeThirdPartyDir, "include");
            // PublicLibraryPaths.Add(QRCodeLibPath);
            //  System.Console.WriteLine("QRCodeLibPath:" + QRCodeLibPath);
#if UE_5_0_OR_LATER
            string OSVersion = "x64";
#else
            string OSVersion = (Target.Platform == UnrealTargetPlatform.Win32) ? "x86" : "x64";
#endif
            PublicIncludePaths.Add(Path.Combine(QRCodeInlucdePath));
            PublicAdditionalLibraries.Add(Path.Combine(QRCodeLibPath, OSVersion, "qrencode.lib"));
        }
        PublicDefinitions.Add(string.Format("WITH_QRCODE_LIB_BINDING={0}", 1));
    }


}