del /f /s /ah "*.suo"
del /f /s /ah "StyleCop.Cache"

rmdir /s /q "NetworkInterface\bin"
rmdir /s /q "NetworkInterface\obj"

rmdir /s /q "CommonCode\bin"
rmdir /s /q "CommonCode\obj"

del /f "Client\Client.csproj.Debug.cachefile"
del /f "Client\Client.csproj.Release.cachefile"
rmdir /s /q "Client\bin"
rmdir /s /q "Client\obj"
rmdir /s /q "Client\Content\bin"
rmdir /s /q "Client\Content\obj"

del /f "Engine\Engine.csproj.Debug.cachefile"
del /f "Engine\Engine.csproj.Release.cachefile"
rmdir /s /q "Engine\bin"
rmdir /s /q "Engine\obj"
rmdir /s /q "Engine\Content\bin"
rmdir /s /q "Engine\Content\obj"

del /f "Server\Server.csproj.Debug.cachefile"
del /f "Server\Server.csproj.Release.cachefile"
rmdir /s /q "Server\bin"
rmdir /s /q "Server\obj"
rmdir /s /q "Server\Content\bin"
rmdir /s /q "Server\Content\obj"

del /f "WorldEditor\WorldEditor.csproj.Debug.cachefile"
del /f "WorldEditor\WorldEditor.csproj.Release.cachefile"
rmdir /s /q "WorldEditor\bin"
rmdir /s /q "WorldEditor\obj"
rmdir /s /q "WorldEditor\Content\bin"
rmdir /s /q "WorldEditor\Content\obj"

pause