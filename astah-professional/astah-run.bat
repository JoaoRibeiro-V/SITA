@echo off
REM call astah-run.bat "astah_directory_path" update-all
REM call astah-run.bat "astah_directory_path" update-all use-builtin-timestamp
REM call astah-run.bat "astah_directory_path" update-all ubt
REM call astah-run.bat "astah_directory_path" update-all-force
REM call astah-run.bat "astah_directory_path" xml sample.asta sample.xml
REM call astah-run.bat "astah_directory_path" obfuscated-xml sample.asta sample.xml

set ASTAH_HOME=%~dp0

set INITIAL_HEAP_SIZE=64m
set MAXIMUM_HEAP_SIZE=1024m

set JAVA_OPTS=-Xms%INITIAL_HEAP_SIZE% -Xmx%MAXIMUM_HEAP_SIZE%
rem set JAVA_OPTS=%JAVA_OPTS% -DrootLevel=DEBUG

java %JAVA_OPTS% -cp "%ASTAH_HOME%\astah-pro.jar" JP.co.esm.caddies.jomt.JudeProRunner %*
