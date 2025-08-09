for /d /r . %%d in (bin,obj,.vs) do @if exist "%%d" rd /s/q "%%d"
del /S *.csproj.user
del /S *.DotSettings.user