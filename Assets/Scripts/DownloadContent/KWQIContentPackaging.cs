/*
0.2.0 implementation of how the KAR Workshop Quick Install format should be packaged and unpackaged after downloading.
On all platforms.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;
using UnityEngine;

//defines a main class for handling the package
public class KWQIPackaging
{
	//adds double quotes around string literals if needed
	static public string AddQuotesIfRequired(string path)
	{
    	return !string.IsNullOrWhiteSpace(path) ? 
        	path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\"")) ? 
            	"\"" + path + "\"" : path : 
            	string.Empty;
	}

	//copies files/folders from one directory into another
	public static void CopyAllDirContents(DirectoryInfo source, DirectoryInfo target)
    {
		if(!Directory.Exists(target.FullName))
        	Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            System.Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAllDirContents(diSourceSubDir, nextTargetSubDir);
        }
    }

	//copies all the files/folders from one directory into another
	public static void CopyAllDirContents(string sourceDirectory, string targetDirectory)
    {
        DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
        DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

        CopyAllDirContents(diSource, diTarget);
    }

	//unpacks a directory archive on Windows for KWQI
	//the first layer is a brotile
	//the second is a Tar
	static public bool UnpackArchive_Windows(string _archivePackageDir, string _archivePackageName, string _outputDir,
	bool shouldDeletePackedROMFileAfterUnpacking, string _brotilProgFilepath)
	{
		string brotilPackageFP = $"{_archivePackageDir}\\{_archivePackageName}.br";
		string tarPackageFP = $"{_archivePackageDir}\\{_archivePackageName}.tar";

		//decompress the brotile
		var hp = new System.Diagnostics.Process();
		hp.StartInfo.UseShellExecute = true;
		hp.StartInfo.FileName = "U:\\RiskiVR\\Documents\\Unity Projects\\KAR Util\\Content\\Tools\\Windows\\brotli.exe";
		hp.StartInfo.Arguments = $"--decompress -o {tarPackageFP} {brotilPackageFP}";
		hp.StartInfo.WorkingDirectory = _brotilProgFilepath;
		hp.Start();
		hp.WaitForExit();

		//extract the Tar ball
		hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "tar",
            Arguments = $"-xvf {tarPackageFP} -C {_outputDir}",
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = true,
            CreateNoWindow = true
        };
		hp.Start();
		hp.WaitForExit();
		
		//clean up the brotile and the tar ball
		if (shouldDeletePackedROMFileAfterUnpacking)
		{
			if(File.Exists(brotilPackageFP)) File.Delete(brotilPackageFP);
			if(File.Exists(tarPackageFP)) File.Delete(tarPackageFP);
		}

		return true;
	}

	//downloads gekko codes on Windows
	static public bool DownloadContent_GekkoCodes_Windows(out System.Diagnostics.Process p, string _dumaProgFilepath,
	 string displayName, string URL, string outputDir)
	{
		string workingDir = "\"" + outputDir + "\"";
		string codeFP = "\"" + outputDir + "/" + displayName + ".ini\"";

		string dumaProgFilepath = "\"" + _dumaProgFilepath + "\"";

		p = new System.Diagnostics.Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.FileName = dumaProgFilepath;
		p.StartInfo.Arguments = URL + " -O " + codeFP;
		p.StartInfo.WorkingDirectory = workingDir;
		p.Start();

		return true;
	}
}
