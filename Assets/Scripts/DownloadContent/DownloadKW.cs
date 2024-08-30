using System;
using System.IO;
using System.Net;
using UnityEngine;

//downloads the latest skin packs
public class DownloadKW : MonoBehaviour
{
	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading KAR Workshop...";
		try
		{
			string installDir = "Content";
			string fileExt = ".tar.gz";

			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/KARWorkshop.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "KARWorkshop";
				content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/KARWorkshop{fileExt}";
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
			KWQIPackaging.CopyAllDirContents(installDir + "/UncompressedPackages/" + content.internalName,
				installDir);
			if(Directory.Exists(installDir + "/UncompressedPackages"))
				Directory.Delete(installDir + "/UncompressedPackages", true);
			
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