dotnet build
dotnet nuget push -s https://nuget.dtvl.com.tw -k %NUGET_API_KEY% bin/Debug/Rugal.JsStore.1.0.0.2.nupkg

pause