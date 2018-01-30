@echo off

echo Powershell execution policy set unrestricted...
powershell -command "& {Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force}"

net stop MongoDb

"C:\Program Files\MongoDB\Server\3.6\bin\mongod.exe" --remove

echo downloading mongo...
powershell -command "$client = new-object System.Net.WebClient; $client.DownloadFile(\"https://fastdl.mongodb.org/win32/mongodb-win32-x86_64-2008plus-ssl-3.6.1-signed.msi\", \"mongo3.6.msi\")"

echo uninstalling mongo compass...
%localappdata%\MongoDBCompassCommunity\Update.exe --uninstall

echo uninstalling mongo...
msiexec /q /x mongo3.6.msi

echo cleanup folders..
rd /s /q %programfiles%\MongoDB
rd /s /q %localappdata%\MongoDBCompassCommunity

echo Powershell execution policy return restricted...
powershell -command "& {Set-ExecutionPolicy -ExecutionPolicy Restricted -Force}"

echo removing installer...
del mongo3.6.msi

rmdir /s /q "d:\dev\db\"

echo DONE!
pause