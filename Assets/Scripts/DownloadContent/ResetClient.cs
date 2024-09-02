using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using UnityEngine;

//resets the client folder, gets the latest client deps, latest KARphin, and clears the users cache
public class ResetClient : MonoBehaviour
{
    //gets KARphin
    private void GetKARphin(DirectoryInfo installDir)
    {
        //attempt to load KWQI data, if not found use a baked in URL
        string KWQIFilePath = "KWQI/KARphin.KWQI";
        KWQI content = new KWQI();
        if (!File.Exists(KWQIFilePath))
        {
            content.internalName = "KARphin";
            content.ContentDownloadURL_Windows = "https://github.com/SeanMott/KARphin_Modern/releases/download/latest/KARphin_Test.br";
            KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
        }
        else
        {
            content = KWQI.LoadKWQI(KWQIFilePath);
        }

        //downloads || returns the file it downloaded
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installDir, content.ContentDownloadURL_Windows, "KARphin");

        //extracts || returns the final extracted folder
        DirectoryInfo uncompressedData = KWQIArchive.Unpack_Windows(KWStructure.GetSupportTool_Brotli_Windows(installDir), archive, installDir);

        //installs the content
        KWInstaller.NetplayClients_AllContent(new DirectoryInfo(uncompressedData.FullName + "/KARphin"), installDir);

        //deletes the uncompressed and packaged data
        uncompressedData.Delete(true);
        archive.Delete();
    }

	//gets the latest content
	public void GetLatest()
	{
#if UNITY_EDITOR
        DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay"); //for editor
#else
        DirectoryInfo installDir = new DirectoryInfo(Environment.CurrentDirectory); //for standalone release

#endif

        //nukes the whole User folder
        DirectoryInfo netplay = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);
		if (netplay.Exists)
			netplay.Delete(true);

        //gets the client deps
		//attempt to load KWQI data, if not found use a baked in URL
        string KWQIFilePath = "KWQI/ClientDeps.KWQI";
        KWQI content = new KWQI();
        if(!File.Exists(KWQIFilePath))
        {
            content.internalName = "ClientDeps";
            content.ContentDownloadURL_Windows = "https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/ClientDeps_Test.br";
            //https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/ClientDeps.br";
            KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
        }
        else
        {
            content = KWQI.LoadKWQI(KWQIFilePath);
        }

        //downloads || returns the file it downloaded
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installDir, content.ContentDownloadURL_Windows, "ClientDeps");

        //extracts || returns the final extracted folder
        DirectoryInfo uncompressedData = KWQIArchive.Unpack_Windows(KWStructure.GetSupportTool_Brotli_Windows(installDir), archive, installDir);

        //installs the content
        KWInstaller.NetplayClients_AllContent(new DirectoryInfo(uncompressedData.FullName + "/UncompressedPackages/" + "ClientDeps"), installDir);

        //deletes the uncompressed and packaged data
       uncompressedData.Delete(true);
       archive.Delete();

        //gets the Gekko Codes
        DownloadBSCodes.GetBSCodes(installDir);
        DownloadHPCodes.GetHPCodes(installDir);

        //generate Dolphin config
        string config = "[General]ISOPaths = 1\nRecursiveISOPaths = True\nISOPath0 = " +
            installDir + "/ROMs\n";

        //generates the folder structure
        DirectoryInfo configFolder = Directory.CreateDirectory(KWStructure.GenerateKWStructure_SubDirectory_Clients_User(installDir) + "/Config");

		System.IO.StreamWriter file = new System.IO.StreamWriter(configFolder + "/Dolphin.ini");
		file.Write(config);
		file.Close();

        //gets KARphin
        GetKARphin(installDir);

        MainUI.instance.headerText.text = "<color=green>Download Complete!"; //nofiy the build worked

        var dolphin = new Process();
        dolphin.StartInfo.FileName = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir) + "/KARphin";
        dolphin.StartInfo.WorkingDirectory = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir).FullName;
        dolphin.Start();
    }
}