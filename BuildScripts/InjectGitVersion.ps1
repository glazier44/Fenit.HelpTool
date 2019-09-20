$gitVersion = git rev-list --all --count;
$name = git rev-parse --abbrev-ref HEAD;
$gitText= "{0:0000}" -f [convert]::ToInt32($gitVersion, 10);

$assemblyFile = $args[0] + "\Properties\AssemblyInfo.cs";
$templateFile =  $args[0] + "\Properties\AssemblyInfo_template.cs";




    $newAssemblyContent = Get-Content $templateFile |
    %{$_ -replace '\$FILEVERSION\$', ('1.02.' + $gitText) } |
    %{$_ -replace '\$INFOVERSION\$', ('1.02.' + $gitText + "-" + $name) };



If (-not (Test-Path $assemblyFile) -or ((Compare-Object (Get-Content $assemblyFile) $newAssemblyContent))) {
    echo "Injecting Git Version Info to AssemblyInfo.cs"
    $newAssemblyContent > $assemblyFile;       
}