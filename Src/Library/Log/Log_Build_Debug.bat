@echo ����log4net

@cd log4net\src\log4net
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe log4net.vs2010.sln /t:Rebuild /property:Configuration=Debug /l:FileLogger,Microsoft.Build.Engine;logfile=log4net.vs2010.log
@echo Close notepad to continue...
@if errorlevel 1 @notepad log4net.vs2010.log
@cd ..\..\..\
@xcopy log4net\build\bin\log4net\net\2.0\debug\*.* ..\..\..\Publish\Library\ /s /e /y