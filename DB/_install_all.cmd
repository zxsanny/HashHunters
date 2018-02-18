@setlocal enableextensions
@cd /d "%~dp0"

call 01_install_mongo.cmd
call 02_start_mongo_service.cmd
call 03_security.cmd
call 04_initial_db_structure.cmd

pause