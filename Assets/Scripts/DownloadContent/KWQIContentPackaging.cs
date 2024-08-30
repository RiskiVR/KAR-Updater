/*
0.2.0 implementation of how the KAR Workshop Quick Install format should be packaged and unpackaged after downloading.
On all platforms.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;

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
	public static void UnpackArchive_Windows(string archivePackageDir, string archivePackageName, bool shouldDeletePackedROMFileAfterUnpacking)
	{
		string tarPackageFP = $"{archivePackageDir}\\{archivePackageName}.tar.gz";
		string contentFP = $"{System.Environment.CurrentDirectory}\\Content";

		var hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
		{
			FileName = "tar",
			Arguments = $"-xvf {tarPackageFP}",
			RedirectStandardOutput = false,
			RedirectStandardError = false,
			UseShellExecute = true,
			CreateNoWindow = false,
			WorkingDirectory = contentFP
		};
		hp.Start();
		hp.WaitForExit();
		
		if (shouldDeletePackedROMFileAfterUnpacking && File.Exists(tarPackageFP)) File.Delete(tarPackageFP);
	}
}