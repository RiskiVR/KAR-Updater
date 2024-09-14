using System;
using System.IO;
using UnityEngine;

//downloads the latest KAR Launcher
public class DownloadLauncher : MonoBehaviour
{
	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading KAR Launcher...";
		
		//downloads
		DirectoryInfo installDir = new DirectoryInfo(System.Environment.CurrentDirectory);

		try
		{
			//checks for old Updater with a space, delete it
			FileInfo oldLauncher = new FileInfo(installDir.FullName + "/KAR Launcher.exe");
			if (oldLauncher.Exists)
			{
				oldLauncher.Delete();
				DirectoryInfo oldData = new DirectoryInfo(installDir.FullName + "/KAR Launcher_Data");
				if (oldData.Exists) oldData.Delete(true);
			}

			//downloads
			KWQICommonInstalls.GetLatest_KARLauncher();

			//runs the Updater
			var launcher = new System.Diagnostics.Process();
			launcher.StartInfo.FileName = installDir.FullName + "/KAR Launcher.exe";
			launcher.StartInfo.WorkingDirectory = installDir.FullName;
			launcher.Start();

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