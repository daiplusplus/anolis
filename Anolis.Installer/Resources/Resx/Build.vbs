Option Explicit

Dim fso
Set fso = CreateObject("Scripting.FileSystemObject")

' Step 1, convert .resx into .resources using resgen.exe

Dim wsh
Set wsh = CreateObject("WScript.Shell")


Dim resxDir
resxDir = "D:\Users\David\My Documents\Visual Studio Projects\Anolis\Anolis.Installer\Resources\Resx\" 

wsh.CurrentDirectory = resxDir

Compile resxDir

' Step 2, GZip compress

Compress resxDir

Sub Compile(folderPath)
	
	Dim resxFolder
	Set resxFolder = fso.GetFolder(folderPath)

	Dim cmd
	cmd = """C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\resgen.exe"" /compile "
	
	Dim destPath
	destPath = "Resources\"
	
	Dim file
	For Each file in resxFolder.Files
		
		If Right( file.Name, 5 ) = ".resx" Then
			
			Dim resource
			resource = destPath & LeftFR(file.Name, 5) & ".resources"
			
			cmd = cmd & """" & file.Name & """,""" & resource & """ "
			
		End If
		
	Next
	
	wsh.Run cmd, 10, True
	
End Sub

Sub Compress(folderPath)
	
	folderPath = folderPath & "\Resources"
	
	Dim resFolder
	Set resFolder = fso.GetFolder(folderPath)
	
	Dim file
	For Each file in resFolder.Files
		
		If Right( file.Name, Len(".resources") ) = ".resources" Then
			
			Dim gzPath
			gzPath = "GZip\" & file.Name & ".gz"
			
			Dim cmd
			cmd = """C:\Program Files\7-Zip\7z.exe"" a -tgzip -mx=9 """ & gzPath & """ ""Resources\" & file.Name  & """"
			
			wsh.Run cmd
			
		End If
		
	Next
	
	
End Sub

Function LeftFR(name, length)
	
	LeftFR = Left(name, Len(name) - length )
	
End Function