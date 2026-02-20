$files = Get-ChildItem -Path . -Filter *.cs -Recurse | Where-Object { $_.FullName -notmatch '\\obj\\' -and $_.FullName -notmatch '\\bin\\' }
$modifiedCount = 0

foreach ($file in $files) {
    $lines = [System.IO.File]::ReadAllLines($file.FullName)
    
    $usings = [System.Collections.Generic.List[string]]::new()
    $nonUsings = [System.Collections.Generic.List[string]]::new()
    
    $namespaceFound = $false
    
    foreach ($line in $lines) {
        $trim = $line.Trim()
        
        # Identify using directive (but ignore using statements inside methods like 'using (var x = ...)')
        if (-not $namespaceFound -and $trim.StartsWith("using ") -and $trim.EndsWith(";")) {
            $usings.Add($trim)
        } elseif (-not $namespaceFound -and [string]::IsNullOrWhiteSpace($trim)) {
            # Skip empty lines in the header
        } else {
            # We reached code or comments
            if ($trim.StartsWith("namespace ") -or $trim.StartsWith("public ") -or $trim.StartsWith("internal ") -or $trim.StartsWith("class ") -or $trim.StartsWith("struct ") -or $trim.StartsWith("enum ")) {
                $namespaceFound = $true
            }
            $nonUsings.Add($line)
        }
    }
    
    if ($usings.Count -gt 0) {
        $sortedUsings = $usings | Sort-Object {
            $u = $_
            # Extract the actual namespace being imported, ignoring alias "using X = Y;"
            $target = $u
            if ($u -match "=\s*([^;]+);") {
                $target = $matches[1].Trim()
            } else {
                $target = $u -replace '^using\s+', '' -replace ';$', ''
            }
            
            if ($target.StartsWith("System")) { return "1_$target" }
            if ($target.StartsWith("Microsoft")) { return "2_$target" }
            if ($target.StartsWith("Terraria")) { return "3_$target" }
            if ($target.StartsWith("OmoriMod")) { return "4_$target" }
            return "5_$target"
        }
        
        $newContent = [System.Collections.Generic.List[string]]::new()
        foreach ($u in $sortedUsings) {
            $newContent.Add($u)
        }
        $newContent.Add("")
        
        # Trim leading blank lines from nonUsings
        while ($nonUsings.Count -gt 0 -and [string]::IsNullOrWhiteSpace($nonUsings[0])) {
            $nonUsings.RemoveAt(0)
        }
        
        $newContent.AddRange($nonUsings)
        
        # Compare to see if we need to write
        # Join with CRLF for comparison since file might have CRLF or LF. We'll check content length and characters.
        $originalText = $lines -join "`n"
        $newText = $newContent -join "`n"
        
        if ($originalText -ne $newText) {
            $utf8NoBom = New-Object System.Text.UTF8Encoding $false
            [System.IO.File]::WriteAllLines($file.FullName, $newContent, $utf8NoBom)
            $modifiedCount++
        }
    }
}
Write-Output "Modified $modifiedCount files."
