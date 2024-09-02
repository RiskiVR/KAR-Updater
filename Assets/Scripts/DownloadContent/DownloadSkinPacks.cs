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
#if UNITY_EDITOR
            DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay"); //for editor
#else
        DirectoryInfo installDir = new DirectoryInfo(Environment.CurrentDirectory); //for standalone release

#endif

			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/SkinPacks.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "SkinPacks";
				content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/SkinPacks_Test.br";
				KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
			}
			else
			{
				content = KWQI.LoadKWQI(KWQIFilePath);
			}

			//downloads
			DirectoryInfo skinPackDir = KWStructure.GenerateKWStructure_SubDirectory_Mod_SkinPacks(installDir);
            KWQICommonInstalls.GetLatest_SkinPacks(KWStructure.GetSupportTool_Brotli_Windows(installDir), skinPackDir);

			//installs the new content into the netplay client directory
			DirectoryInfo user = new DirectoryInfo(KWStructure.GenerateKWStructure_SubDirectory_Clients_User(installDir) + "/Load/Textures/KBSE01/");

            KWInstaller.CopyAllDirContents(new DirectoryInfo(skinPackDir + "/[L] B2 Non Outline Skins"),
                    new DirectoryInfo(user + "[L] B2 Non Outline Skins"));
            KWInstaller.CopyAllDirContents(new DirectoryInfo(skinPackDir + "/[R] B2 Outline Skins"),
                new DirectoryInfo(user + "[R] B2 Outline Skins"));

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