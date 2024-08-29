using System;
using System.IO;
using System.Net;
using UnityEngine;

//downloads the latest KAR Don't
public class DownloadKARDont : MonoBehaviour
{
	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading KARDon't...";
		try
		{
			string installDir = "Content";
			string toolsDir =  KWStructure.GenerateKWStructure_Directory_Tools(installDir) + "/Windows/";

			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/KARDont.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "KARDont";
				content.ContentDownloadURL_Windows = "https://github.com/SeanMott/KARDont/releases/download/latest/KARDont.br";
				KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
			}
			else
			{
				content = KWQI.LoadKWQI(KWQIFilePath);
			}

			//downloads
			WebClient w = new WebClient();
			w.DownloadFile(content.ContentDownloadURL_Windows, installDir + "/" + content.internalName + ".br");

			//extracts
			KWQIPackaging.UnpackArchive_Windows(installDir, content.internalName, installDir, true, toolsDir + "brotli.exe");

			//installs the new content into the netplay client directory
			KWQIPackaging.CopyAllDirContents(installDir + "/UncompressedPackages/" + content.internalName,
				KWStructure.GenerateKWStructure_SubDirectory_Mod_Hombrew(installDir) + "/KARDont");
			if(Directory.Exists(installDir + "/UncompressedPackages"))
				Directory.Delete(installDir + "/UncompressedPackages", true);
			
			MainUI.instance.headerText.text = "<color=green>Download Complete!";
		}
		catch (Exception e)
		{
			MainUI.instance.headerText.text = "<color=red>Download Failed!";
			MainUI.instance.errorText.text = e.ToString();
			Debug.LogError(e);
		}
	}
}