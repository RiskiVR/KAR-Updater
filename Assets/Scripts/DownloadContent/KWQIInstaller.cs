/*
0.2.0 implementation of how the content from KWQI Archive content will be installed

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;

//defines a file

//defines a structure for installing content
public class KWQIInstaller
{
    //installs Archive content into Netplay Clients
    public static void InstallArchiveContent_NetplayClient(string rootDir, string contentDir)
    {
        string dir = KWStructure.GenerateKWStructure_Directory_NetplayClients(rootDir);

        //copies the content into the clients folder
        KWQIPackaging.CopyAllDirContents(contentDir, dir);
    }
}