@attrib -R "..\Publish\*.*" /S /D
@echo É¾³ı¾É³ÌĞò
@rmdir "..\Publish\Library\" /s /q

@cd Library
@call Library_Build_Release.bat

@echo ±àÒëÍê±Ï
@pause