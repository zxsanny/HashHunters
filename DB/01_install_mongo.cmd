@echo off

echo Powershell execution policy set unrestricted...
powershell -command "& {Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force}"

echo downloading mongo...
powershell -command "$client = new-object System.Net.WebClient; $client.DownloadFile(\"https://fastdl.mongodb.org/win32/mongodb-win32-x86_64-2008plus-ssl-3.6.1-signed.msi\", \"mongo3.6.msi\")"

echo installing mongo...
msiexec.exe /q /i mongo3.6.msi ^
            INSTALLLOCATION="C:\Program Files\MongoDB\Server\3.6\" ^
            ADDLOCAL="all"
            
echo Powershell execution policy return restricted...
powershell -command "& {Set-ExecutionPolicy -ExecutionPolicy Restricted -Force}"

echo removing installer...
del mongo3.6.msi

echo PATH setup
setx PATH "%PATH%;C:\Program Files\MongoDB\Server\3.6\bin\"

echo Mongo has been installed successfully!