@echo off

mongo script_01_security.js

(echo security:
 echo   authorization: "enabled"
)>>C:\"Program Files"\MongoDB\Server\3.6\mongod.cfg

net stop MongoDB
net start MongoDB

echo Security has been installed successfully!