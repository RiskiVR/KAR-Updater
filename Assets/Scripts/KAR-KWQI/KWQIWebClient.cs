/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System;
using System.IO;
using System.Net;

//defines a common class for Web Client implementations for KWQI
class KWQIWebClient
{
    //downloads a Archive on Windows (async)

	//downloads a Archive on Windows (NOT ASYNC) || returns the Archive it downloaded
	static public FileInfo Download_Archive_Windows(DirectoryInfo outputFolder, string archiveURL, string archiveName)
	{
        FileInfo archive = new FileInfo($"{outputFolder.FullName}/{archiveName}.br");

        //attempts a download
		using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile(archiveURL, archive.FullName);
                Console.WriteLine($"Download completed: {archive.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new FileInfo("");
            }
        }
		return archive;
	}

    //downloads a gekko code file on Windows (NOT ASYNC) || returns the Gekko Code file it downloaded
	static public FileInfo Download_GekkoCodes_Windows(DirectoryInfo outputFolder, string URL, string gekkoFileGameID)
	{
        FileInfo gekkoCodeFile = new FileInfo($"{outputFolder.FullName}/{gekkoFileGameID}.ini");

        //attempts a download
        using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile(URL, gekkoCodeFile.FullName);
                Console.WriteLine("File downloaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new FileInfo("");
            }
        }

		return gekkoCodeFile;
	}
}