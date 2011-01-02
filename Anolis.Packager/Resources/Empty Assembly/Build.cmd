set installerPath="D:\Users\David\My Documents\Visual Studio Projects\Anolis\Anolis.Installer\bin\Release\Anolis.Installer.exe"

set cscPath=%windir%\Microsoft.NET\Framework64\v2.0.50727\Csc.exe
set alPath="C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin\al.exe"
set ilmergePath="C:\Program Files (x86)\Microsoft .NET Tools\ILMerge\ilmerge.exe"



mkdir out

%cscPath% /noconfig /nowarn:1701,1702 /errorreport:prompt /warn:4 /define:TRACE /debug:pdbonly /filealign:512 /optimize+ /out:"out\EmptyAssembly.csmodule" /target:module "AssemblyInfo.cs" "Anolis.EmptyAssembly.cs"

%alPath% /out:out\Packages.dll /target:lib /embedresource:PACKAGE.anop out\EmptyAssembly.csmodule

%ilmergePath% /log:out\ilmerge.log /target:winexe /out:out\EmbeddedInstaller.exe %installerPath% out\Packages.dll