set packcmd=dotnet pack
set clearpackages=del bin\debug\*.nupkg
:: -c Release

cd ../

cd src/src/Aolyn.Utility
%clearpackages%
%packcmd%

cd ../../../
cd src/src/Aolyn.Data.Npgsql
%clearpackages%
%packcmd%

cd ../../../
cd src/src/Aolyn.Config
%clearpackages%
%packcmd%

pause