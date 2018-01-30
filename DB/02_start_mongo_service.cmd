@echo off

mkdir d:\dev\db
mkdir d:\dev\db\log

(
 echo net:
 echo   bindIp: 127.0.0.1
 echo systemLog:
 echo   destination: file
 echo   path: d:\dev\db\log\mongo.log
 echo storage:
 echo   dbPath: d:\dev\db
) >C:\"Program Files"\MongoDB\Server\3.6\mongod.cfg

"C:\Program Files\MongoDB\Server\3.6\bin\mongod.exe" --config "C:\Program Files\MongoDB\Server\3.6\mongod.cfg" --install

net start MongoDB

echo Mongo service has been installed successfully!