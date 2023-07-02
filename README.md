# Execute Powershell Script

The puspose of this automation script is to execute a Powershell script on every DMA that belongs to a cluster

## How to use

Upload the powershell script on each DMA. For example, you could upload the script using the app **Documents** (avaialable in Apps -> Documents)
If the file is uploaded directly to *General documents*, then the full folder path of the script will be: *C:\Skyline DataMiner\Documents\DMA_COMMON_DOCUMENTS\myScript.ps1*
You can create a specific folder for your script (e.g. *Powershell_Scripts* in *General documents*). Then, the full folder path will be  *C:\Skyline DataMiner\Documents\DMA_COMMON_DOCUMENTS\Powershell_Scritps\myScript.ps1*

Once the file is uploaded, execute the automation script. The script parameter required will be the full path defined in the previous step.
