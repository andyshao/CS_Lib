@echo Nuget���log4net.Wrap

@cd log4net.Wrap\log4net.Wrap
nuget pack log4net.Wrap.csproj
@cd ..\..\

:@cd log4net.Wrap\log4net.Wrap(NET4)
:nuget pack log4net.Wrap(NET4).csproj
:@cd ..\..\