@attrib -R "..\Publish\*.*" /S /D

@cd Library
@call Build_Release.bat

@echo �������
@pause