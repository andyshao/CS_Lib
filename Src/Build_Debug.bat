@attrib -R "..\Publish\*.*" /S /D

@cd Library
@call Build_Debug.bat

@echo �������
@pause