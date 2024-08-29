using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using UnityEngine;
using Debug = UnityEngine.Debug;

//downloads the latest KARphin Dev build
public class DownloadKARphinDev : MonoBehaviour
{
	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading KARphin Dev Build...";
		try
		{
			string installDir = "Content";
			string toolsDir =  KWStructure.GenerateKWStructure_Directory_Tools(installDir) + "/Windows/";

			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/KARphinDev.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "KARphinDev";
				content.ContentDownloadURL_Windows = "https://github.com/SeanMott/KARphin_Modern/releases/download/latest-dev/KARphinDev.br";
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
			KWQIPackaging.CopyAllDirContents(installDir + "/" + content.internalName,
				KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir));
			if(Directory.Exists(installDir + "/" + content.internalName))
				Directory.Delete(installDir + "/" + content.internalName, true);

			//runs Dolphin
			var dolphin = new Process();
			dolphin.StartInfo.FileName = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir) + "/KARphinDev";
			dolphin.StartInfo.WorkingDirectory = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);
			dolphin.Start();
			
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