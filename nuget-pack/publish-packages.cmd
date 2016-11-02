for %i in (packages\*.nupkg) do nuget push "%i" -Source https://www.nuget.org

pause