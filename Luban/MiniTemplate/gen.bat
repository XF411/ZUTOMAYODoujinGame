set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-simple-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=..\..\Assets\GameScripts\HotFix\GameProto\GameConfig ^
    -x outputDataDir=..\..\Assets\AssetRaw\Configs
pause