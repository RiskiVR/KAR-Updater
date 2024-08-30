using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using UnityEngine;
using Debug = UnityEngine.Debug;

//downloads the latest KARphin
public class DownloadKARphin : MonoBehaviour
{
	//gets the latest KARphin build
	static public void GetKARphin(string installDir)
	{
		string fileExt = ".tar.gz";
		
		//attempt to load KWQI data, if not found use a baked in URL
        string KWQIFilePath = "KWQI/KARphin.KWQI";
        KWQI content = new KWQI();
        if(!File.Exists(KWQIFilePath))
        {
            content.internalName = "KARphin";
		    content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KARphin_Modern/releases/download/latest/KARphin{fileExt}";
		    KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
        }
        else
        {
            content = KWQI.LoadKWQI(KWQIFilePath);
        }

		//downloads
		WebClient w = new WebClient();
		w.DownloadFile(content.ContentDownloadURL_Windows, $"{installDir}\\{content.internalName}{fileExt}");

		//extracts
		KWQIPackaging.UnpackArchive_Windows(installDir, content.internalName, true);
		
		//installs the new content into the netplay client directory
		KWQIPackaging.CopyAllDirContents(installDir + "/" + content.internalName,
		KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir));
		if(Directory.Exists(installDir + "/" + content.internalName))
			Directory.Delete(installDir + "/" + content.internalName, true);
	}

	//runs KARphin
	static public void RunKARphin(string installDir)
	{
		var dolphin = new Process();
		dolphin.StartInfo.FileName = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir) + "/KARphin";
		dolphin.StartInfo.WorkingDirectory = KWStructure.GenerateKWStructure_Directory_NetplayClients(installDir);
		dolphin.Start();
	}

	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading KARphin...";
		try
		{
			string installDir = "Content";
			GetKARphin(installDir);
			RunKARphin(installDir);	
			
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[6]);
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[2]);
			MainUI.instance.headerText.text = "<color=green>Download Complete!";
		}
		catch (Exception e)
		{
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[4]);
			MainUI.instance.headerText.text = "<color=red>Download Failed!";
			Debug.LogError(e);
			MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
		}
	}
}