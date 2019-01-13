.\csc /t:library /out:Business.dll /r:System.Core.dll /recurse:..\PillarKata\Business\*.cs
.\csc /out:PillarKata.exe /r:Business.dll ..\PillarKata\PillarKata\Program.cs