@echo Nuget���Utility

@cd Utility\Utility
nuget pack Utility.csproj -Prop Configuration=Release
@cd ..\..\