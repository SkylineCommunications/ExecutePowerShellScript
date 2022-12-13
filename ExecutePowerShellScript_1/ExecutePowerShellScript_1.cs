/*
****************************************************************************
*  Copyright (c) 2022,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

08/12/2022	1.0.0.1		MOB, Skyline	Initial version
****************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Net.Helper;

/// <summary>
/// DataMiner Script Class.
/// </summary>
public class Script
{
	private const string POWERSHELL_FILE_PATH = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";

	/// <summary>
	/// The Script entry point.
	/// </summary>
	/// <param name="engine">Link with SLAutomation process.</param>
	public void Run(Engine engine)
	{
		// Validate the script parameter
		ScriptParam spPowerShellScriptFilePath = engine.GetScriptParam("FilePath");

		if (spPowerShellScriptFilePath == null)
		{
			engine.ExitFail("[ERROR]|Run|Script parameter FilePath not available");
		}// End if

		if (string.IsNullOrEmpty(spPowerShellScriptFilePath.Value))
		{
			engine.ExitFail("[ERROR]|Run|Script parameter FilePath is empty");
		}// End if

		// Check if the PowerShell executable exists
		if (File.Exists(POWERSHELL_FILE_PATH))
		{
			if (File.Exists(spPowerShellScriptFilePath.Value))
			{
				// Prepare the process to execute the PowerShell script
				ProcessStartInfo processInfo = new ProcessStartInfo();

				processInfo.FileName = POWERSHELL_FILE_PATH;
				processInfo.Arguments = "\"&'" + spPowerShellScriptFilePath.Value + "'\"";
				processInfo.RedirectStandardError = true;
				processInfo.RedirectStandardOutput = true;
				processInfo.UseShellExecute = false;
				processInfo.CreateNoWindow = true;

				// Start process
				Process process = new Process();
				process.StartInfo = processInfo;
				process.Start();

				engine.GenerateInformation("[INFO]|Run|Output:" + process.StandardOutput.ReadToEnd());

				if (!String.IsNullOrEmpty(process.StandardError.ReadToEnd()))
				{
					engine.ExitFail("[ERROR]|Run|Error:" + process.StandardError.ReadToEnd());
				}// End if
			}// End if
			else
			{
				engine.ExitFail("[ERROR]|Run|PowerShell script (" + spPowerShellScriptFilePath.Value + ") not available");
			}// End else
		}// End if
		else
		{
			engine.ExitFail("[ERROR]|Run|PowerShell executable (" + POWERSHELL_FILE_PATH + ")  not available");
		}// End else
	}// End method Run
}// End class Script