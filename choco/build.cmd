dotnet build -c Release ..\Badger.sln
copy ..\Badger\bin\Release\net47\*.dll .\tools
copy ..\Badger\bin\Release\net47\*.exe .\tools
choco pack