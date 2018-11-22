@echo off
title Adding context menu item
prompt $g
@echo on
reg add HKEY_CURRENT_USER\Software\Classes\*\shell\OpenWithAnipad /f
reg add HKEY_CURRENT_USER\Software\Classes\*\shell\OpenWithAnipad /f /ve /d "Open with Anipad"
reg add HKEY_CURRENT_USER\Software\Classes\*\shell\OpenWithAnipad /f /v Icon /d "%~dp0Resources\MainIcon.ico"
reg add HKEY_CURRENT_USER\Software\Classes\*\shell\OpenWithAnipad\command /f
reg add HKEY_CURRENT_USER\Software\Classes\*\shell\OpenWithAnipad\command /f /ve /d "%~dp0Anipad.exe %%1"
@echo off
echo.
echo Success
echo Now you can rightclick any file and choose "Open with Anipad"
echo.
pause