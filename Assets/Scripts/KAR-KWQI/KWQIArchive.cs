/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System;
using System.IO;

//defines a common class for handling KWQI Archives
class KWQIArchive
{
	//compresses a folder into a Tar Package on windows || returns the compressed Tar Package
	static public FileInfo TarPackage_Compress_Windows(DirectoryInfo folderFilePathToCompress, DirectoryInfo outputFolderForTarPackage,
	string nameTheCompressedTarShouldHave)
	{
		FileInfo package = new FileInfo($"{outputFolderForTarPackage.FullName}/{nameTheCompressedTarShouldHave}.tar.gz");

		var hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
		{
			FileName = "tar",
			Arguments = $"-czvf \"{package.FullName}\" \"{folderFilePathToCompress.FullName}\"",
			RedirectStandardOutput = false,
			RedirectStandardError = false,
			UseShellExecute = true,
			CreateNoWindow = false
		};
		hp.Start();
		hp.WaitForExit();

		return package;
	}

	//compresses a Tar Package into a Brotli Package on windows || returns the final brotli package
	static public FileInfo BrotliPackage_CompressFromTar_Windows(FileInfo brotliEXEFP, FileInfo tarPackageFP, DirectoryInfo outputFolderForBrotliPackage,
	string nameTheBrotliPackageShouldHave, UInt16 compresstionLevel = 5)
	{
		FileInfo package = new FileInfo($"{outputFolderForBrotliPackage}/{nameTheBrotliPackageShouldHave}.br");

		var hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
		{
			FileName = brotliEXEFP.FullName,
			Arguments = $"-q {compresstionLevel} -f \"{tarPackageFP.FullName}\" -o \"{package.FullName}\"",
			RedirectStandardOutput = false,
			RedirectStandardError = false,
			UseShellExecute = true,
			CreateNoWindow = false
		};
		hp.Start();
		hp.WaitForExit();

		return package;
	}

	//uncompresses a Brotli Package into a Tar Package on windows || returns the compressed tar package
	static public FileInfo BrotliPackage_UnpackToTar_Windows(FileInfo brotliEXEFP, FileInfo brotliPackageFP, DirectoryInfo outputFolderForTARPackage)
	{
		FileInfo package = new FileInfo($"{outputFolderForTARPackage.FullName}/{Path.GetFileNameWithoutExtension(brotliPackageFP.FullName)}.tar.gz");

		var hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
		{
			FileName = brotliEXEFP.FullName,
			Arguments = $"--decompress \"{brotliPackageFP.FullName}\" -o \"{package.FullName}\"",
			RedirectStandardOutput = false,
			RedirectStandardError = false,
			UseShellExecute = true,
			CreateNoWindow = false
		};
		hp.Start();
		hp.WaitForExit();

		System.Console.WriteLine($"Archive unpacked: {package.FullName}");
		return package;
	}

	//uncompresses a Tar Package into it's raw folder on windows || returns the uncompressed folder
	static public DirectoryInfo TarPackage_Unpack_Windows(FileInfo tarPackageFP, DirectoryInfo outputFolderForRawFolder)
	{
		var hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
		{
			FileName = "tar",
			Arguments = $"-xzvf \"{tarPackageFP.ToString()}\"",
			WorkingDirectory = outputFolderForRawFolder.FullName,
			RedirectStandardOutput = false,
			RedirectStandardError = false,
			UseShellExecute = true,
			CreateNoWindow = false
		};
		hp.Start();
		hp.WaitForExit();

		DirectoryInfo package = new DirectoryInfo(outputFolderForRawFolder.FullName);
		System.Console.WriteLine($"Tar Package unpacked: {package.FullName}");
		return package;
	}

	//takes a raw folder and packages it for KWQI || returns the final brotli package
	static public FileInfo Pack_Windows(FileInfo brotliEXEFP, DirectoryInfo folderToPack, DirectoryInfo outputFolder, string packageName)
	{
		FileInfo tarPackage = TarPackage_Compress_Windows(folderToPack, outputFolder, packageName);
		return BrotliPackage_CompressFromTar_Windows(brotliEXEFP, tarPackage, outputFolder, packageName);
	}

	//takes a KWQI Archive and unpacks it to the raw folder || returns the final unpackaged folder
	static public DirectoryInfo Unpack_Windows(FileInfo brotliEXEFP, FileInfo archiveFP, DirectoryInfo outputFolder, string uncompressedFolderName)
	{
		FileInfo tarPackage = BrotliPackage_UnpackToTar_Windows(brotliEXEFP, archiveFP, outputFolder);
		DirectoryInfo uncompressedInfo = TarPackage_Unpack_Windows(tarPackage, outputFolder);
		tarPackage.Delete();
		return uncompressedInfo;
	}
}
