@echo off
rem ---------------------------------------------------------------------------
rem Batch file to run command line tool for ASTAH
rem ---------------------------------------------------------------------------

rem Command Examples
rem ---------------------------------------------------------------------------
rem call astah-command.bat -image er -f C:\input\hoge.asta -t png -o C:\output
rem call astah-command.bat -image all -f C:\input\hoge.asta -t png -o C:\output
rem ---------------------------------------------------------------------------

rem Option Examples
rem ---------------------------------------------------------------------------
rem usage: Export Image Options
rem  -f,--file <target file>    target file
rem  -image                     export documents to image
rem  -o,--output <output dir>   output dir
rem  -t,--type <image type>     png/jpg/emf
rem ---------------------------------------------------------------------------

set ASTAH_HOME=%~dp0

set INITIAL_HEAP_SIZE=16m
set MAXIMUM_HEAP_SIZE=384m

set JAVA_OPTS=-Xms%INITIAL_HEAP_SIZE% -Xmx%MAXIMUM_HEAP_SIZE%
rem set JAVA_OPTS=%JAVA_OPTS% -DrootLevel=DEBUG

java %JAVA_OPTS% -cp "%ASTAH_HOME%\astah-pro.jar" com.change_vision.jude.cmdline.JudeCommandRunner %*
IF NOT ERRORLEVEL 0 goto canNotRun
goto end

:canNotRun
echo.
echo JudeCommandRunner can't run truly.
echo.
pause
goto end

:end
echo.
echo Finished.
echo.
