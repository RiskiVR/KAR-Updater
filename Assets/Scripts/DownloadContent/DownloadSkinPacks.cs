using System;
using System.IO;
using System.Net;
using UnityEngine;

//downloads the latest skin packs
public class DownloadSkinPacks : MonoBehaviour
{
	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading Skin Packs...";
		try
		{
			string installDir = "Content";
			string fileExt = ".tar.gz";

			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/SkinPacks.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "SkinPacks";
				content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/SkinPacks{fileExt}";
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
				KWStructure.GenerateKWStructure_SubDirectory_Mod_SkinPacks(installDir));
			KWQIPackaging.CopyAllDirContents(KWStructure.GenerateKWStructure_SubDirectory_Mod_SkinPacks(installDir) + "/[L] B2 Non Outline Skins",
				KWStructure.GenerateKWStructure_SubDirectory_Clients_User(installDir) + "/Load/Textures/KBSE01/[L] B2 Non Outline Skins");
			KWQIPackaging.CopyAllDirContents(KWStructure.GenerateKWStructure_SubDirectory_Mod_SkinPacks(installDir) + "/[R] B2 Outline Skins",
				KWStructure.GenerateKWStructure_SubDirectory_Clients_User(installDir) + "/Load/Textures/KBSE01/[R] B2 Outline Skins");
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