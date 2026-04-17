@echo off
rem Batch file to compile a sample of Astah API on Windows

setlocal

set ASTAH_HOME=..\..\..\
if exist "%ASTAH_HOME%\jre\bin\javaw.exe" set PATH=%ASTAH_HOME%\jre\bin;%PATH%

rem Remove "rem" from following two lines, if you'd like to use j2sdk.
rem set JAVA_HOME=C:\jdk1.7.0_45
rem set PATH=%JAVA_HOME%\bin

rem set ASTAH_COM_JAR=%ASTAH_HOME%\astah-community.jar
rem set ASTAH_UML_JAR=%ASTAH_HOME%\astah-uml.jar
rem set ASTAH_PRO_JAR=%ASTAH_HOME%\astah-pro.jar
set ASTAH_JAR=%ASTAH_COM_JAR%;%ASTAH_UML_JAR%;%ASTAH_PRO_JAR%
set API_JAR=%ASTAH_HOME%\astah-api.jar
set CLASSPATH=%ASTAH_JAR%;%API_JAR%

rem compile
javac -classpath %CLASSPATH% *.java
IF ERRORLEVEL 2 goto noJavac
goto end

:noJavac
echo.
echo Failed to compile.
echo Java SDK is required to compile.
echo.
pause
goto end

:end

endlocal
