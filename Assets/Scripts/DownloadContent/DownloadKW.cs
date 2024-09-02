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
#if UNITY_EDITOR
            DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay"); //for editor
#else
        DirectoryInfo installDir = new DirectoryInfo(Environment.CurrentDirectory); //for standalone release

#endif
			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/KARWorkshop.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "KARWorkshop";
				content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KARphin_Modern/releases/download/kw/KARWorkshop.tar.gz.br";
				KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
			}
			else
			{
				content = KWQI.LoadKWQI(KWQIFilePath);
			}

			//downloads
			KWQICommonInstalls.GetLatest_KARWorkshop(KWStructure.GetSupportTool_Brotli_Windows(installDir), installDir);

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