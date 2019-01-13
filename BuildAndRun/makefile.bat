.\csc /t:library /out:Business.dll /r:System.Core.dll,Microsoft.CodeAnalysis.dll,Microsoft.CodeAnalysis.CSharp.dll,System.Collections.Immutable.dll,System.Text.Encoding.CodePages.dll,System.Reflection.Metadata.dll,System.Runtime.dll /recurse:..\PillarKata\Business\*.cs
.\csc /out:PillarKata.exe /r:Business.dll,Microsoft.CodeAnalysis.dll,Microsoft.CodeAnalysis.CSharp.dll,System.Collections.Immutable.dll,System.Text.Encoding.CodePages.dll,System.Reflection.Metadata.dll ..\PillarKata\PillarKata\Program.cs
cd ..\PillarKata\UnitTests\
..\..\BuildAndRun\dotnet.exe test
cd ..\..\BuildAndRun\