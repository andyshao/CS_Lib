@attrib -R "..\Publish\*.*" /S /D
@echo ɾ���ɳ���
@rmdir "..\Publish\Library\" /s /q

@cd Library
@call Library_Build_Release.bat

@echo �������
@pause