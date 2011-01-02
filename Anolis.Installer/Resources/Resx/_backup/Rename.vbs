Option Explicit

Main

Sub Main
	
	Dim findThis, replaceWithThis
	findThis = "Anolis.Installer.Resources.Resx."
	
	If Len( findThis ) = 0 Then Exit Sub
	
	replaceWithThis = ""
	
	'''''''''''''''''''''''''''''''''''''''
	
	Dim fso
	Set fso = CreateObject("Scripting.FileSystemObject")
	
	Dim dir
	Set dir = fso.GetFolder(".")
	
	Dim file
	For Each file in dir.Files
		
		Dim name
		name = Replace(file.Name, findThis, replaceWithThis)
		
		If file.Name <> name Then
			file.Name = name
		End If
		
	Next
	
End Sub