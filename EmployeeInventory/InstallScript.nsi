OutFile "Installer.exe"
InstallDir "$PROGRAMFILES\InventoryRegister\"

Section "install"
	SetOutPath "$INSTDIR"
	
	File "AutoMapper.dll"
	File "AutoMapper.Net4.dll"
	File "AutoMapper.xml"
	File "ConnectionString.txt"
	File "EntityFramework.dll"
	File "EntityFramework.SqlServer.dll"
	File "Inventory Register.exe"
	File "MySql.Data.Entity.EF6.dll"

	MessageBox MB_YESNO "Do you want to create a desktop shortcut?" IDYES yes IDNO no
	yes:
		CreateShortcut "$DESKTOP\Inventory Register.lnk" "$INSTDIR\Inventory Register.exe"
	no:
SectionEnd