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
#if UNITY_EDITOR
            DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay"); //for editor
#else
        DirectoryInfo installDir = new DirectoryInfo(Environment.CurrentDirectory);//for standalone release

#endif

			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/KARDont.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "KARDont";
				content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KARDont/releases/download/latest/KARDont_Test.br";
				KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
			}
			else
			{
				content = KWQI.LoadKWQI(KWQIFilePath);
			}

            //downloads
            KWQICommonInstalls.GetLatest_KARDont(KWStructure.GenerateKWStructure_SubDirectory_Mod_Hombrew(installDir));

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