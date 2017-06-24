@echo off

if not exist *.nuspec e:\nuget\nuget.exe spec

@echo begin nupkg create
@E:\nuget\nuget.exe pack Rc.PetaPoco.csproj

@echo move nupkg to specified path
@copy /y *.nupkg D:\Simon\MyNuGet

@del \\192.168.16.102\个人文件\Simon\MyNuGet\Rc.PetaPoco.*.nupkg
@move /y *.nupkg \\192.168.16.102\个人文件\Simon\MyNuGet\

@pause