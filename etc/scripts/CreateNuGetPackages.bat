@echo off

set outputDirectory=..\..\build
set nugetExe=..\tools\nuget.exe
set properties=Configuration=Release

if not exist %outputDirectory% (
    mkdir %outputDirectory%
)

%nugetExe% pack ..\..\src\CQ\CQ.csproj -OutputDirectory %outputDirectory% -Properties %properties%

%nugetExe% pack ..\..\src\CQ.HttpApi\CQ.HttpApi.csproj -OutputDirectory %outputDirectory% -Properties %properties%
%nugetExe% pack ..\..\src\CQ.HttpApi.Client\CQ.HttpApi.Client.csproj -OutputDirectory %outputDirectory% -Properties %properties%

%nugetExe% pack ..\..\src\CQ.Integration.Owin\CQ.Integration.Owin.csproj -OutputDirectory %outputDirectory% -Properties %properties%
%nugetExe% pack ..\..\src\CQ.Integration.WebApi\CQ.Integration.WebApi.csproj -OutputDirectory %outputDirectory% -Properties %properties%

%nugetExe% pack ..\..\src\CQ.SimpleInjectorExtensions\CQ.SimpleInjectorExtensions.csproj -OutputDirectory %outputDirectory% -Properties %properties%

