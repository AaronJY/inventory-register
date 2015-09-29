!define version "1.15"

Name "Inventory Register Installer Version ${version}"
OutFile "IR Installer ${version}.exe"
InstallDir "$PROGRAMFILES\InventoryRegister\"
AutoCloseWindow true

Section "install"
	MessageBox MB_YESNO "You are about to install version ${version}. Would you like to continue?" IDYES yesinstall IDNO noinstall
	yesinstall:
		Call Install
	noinstall:
		Quit

SectionEnd

Function Install
	SetOutPath "$INSTDIR"
	
	File "AutoMapper.dll"
	File "AutoMapper.Net4.dll"
	File "AutoMapper.xml"
	File "EntityFramework.dll"
	File "EntityFramework.SqlServer.dll"
	File "Inventory Register.exe"
	File "Inventory Register.exe.config"

	MessageBox MB_YESNO "Do you want to create a desktop shortcut?" IDYES yes IDNO no
	yes:
		CreateShortcut "$DESKTOP\Inventory Register.lnk" "$INSTDIR\Inventory Register.exe"
		MessageBox MB_OK "Application has been installed successfully!"
	no:
		Quit

	Quit
FunctionEnd