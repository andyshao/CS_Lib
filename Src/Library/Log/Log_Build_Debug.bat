@echo ±‡“Îlog4net

@cd log4net\src\log4net
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe log4net.vs2010.sln /t:Rebuild /property:Configuration=Debug /l:FileLogger,Microsoft.Build.Engine;logfile=log4net.vs2010.log
@echo Close notepad to continue...
@if errorlevel 1 @notepad log4net.vs2010.log
@cd ..\..\..\
@xcopy log4net\build\bin\log4net\net\2.0\debug\*.* ..\..\..\Publish\Library\ /s /e /y

@echo ±‡“Îlog4net.Wrap

@cd log4net.Wrap
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Log4netWrap.sln /t:Rebuild /property:Configuration=Debug /l:FileLogger,Microsoft.Build.Engine;logfile=Log4netWrap.log
@echo Close notepad to continue...
@if errorlevel 1 @notepad Log4netWrap.log
@cd ..\