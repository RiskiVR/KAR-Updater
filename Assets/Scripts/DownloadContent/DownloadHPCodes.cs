using System;
using System.IO;
using System.Net;
using UnityEngine;

//downloads the latest Hack Pack Gekko Codes
public class DownloadHPCodes : MonoBehaviour
{

	//gets the HP codes
	static public void GetHPCodes(string installDir, string toolsDir)
	{
		//attempt to load KWQI data, if not found use a baked in URL
        string KWQIFilePath = "KWQI/HPGekkoCodes.KWQI";
        KWQI content = new KWQI();
        if(!File.Exists(KWQIFilePath))
        {
            content.internalName = "KHPE01";
		    content.ContentDownloadURL_Windows = "https://github.com/SeanMott/KARphin_Modern/releases/download/gekko/KHPE01.ini";
		    KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
        }
        else
        {
            content = KWQI.LoadKWQI(KWQIFilePath);
        }

        //generates the directories as needed
        string clientsFolder = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);
        DirectoryInfo gekkoCodeDstFolder = Directory.CreateDirectory(clientsFolder + "/User/GameSettings");

		//downloads
		WebClient w = new WebClient();
		w.DownloadFile(content.ContentDownloadURL_Windows, gekkoCodeDstFolder + "/" + content.internalName + ".ini");
	}

	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading Hack Pack Codes...";
		try
		{
			string installDir = "Content";
			string toolsDir = KWStructure.GenerateKWStructure_Directory_Tools(installDir) + "/Windows/";
			GetHPCodes(installDir, toolsDir);	
			
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