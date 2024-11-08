# Prevent the window from closing immediately if run directly
if ($Host.Name -eq "ConsoleHost") {
    $wasWindowsTerminal = $env:WT_SESSION
}

# Script to update John Wick movie characters
$baseUrl = "http://localhost:5000/api"
$successColor = "Green"
$errorColor = "Red"
$infoColor = "Yellow"
$dataColor = "Cyan"

function Write-Header {
    param (
        [string]$title
    )
    Write-Host "`n=== $title ===" -ForegroundColor $infoColor
}

function Get-ApiData {
    param (
        [string]$endpoint
    )
    
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl/$endpoint" -Method GET -UseBasicParsing
        return $response
    }
    catch {
        Write-Host "Error fetching data from $endpoint`: $($_.Exception.Message)" -ForegroundColor $errorColor
        return $null
    }
}

function Update-MovieCharacters {
    param (
        [int]$movieId,
        [int[]]$characterIds
    )
    
    try {
        Write-Host "Updating characters for movie ID $movieId... " -NoNewline
        
        $body = @{
            characterIds = $characterIds
        }
        
        $json = $body | ConvertTo-Json
        Write-Host "`nRequest Body:" -ForegroundColor $dataColor
        Write-Host $json -ForegroundColor $dataColor
        
        $response = Invoke-RestMethod -Uri "$baseUrl/movies/$movieId/characters" -Method PUT -Body $json -ContentType "application/json" -UseBasicParsing
        Write-Host "OK" -ForegroundColor $successColor
        return $true
    }
    catch {
        Write-Host "Failed" -ForegroundColor $errorColor
        Write-Host "Error: $($_.Exception.Message)" -ForegroundColor $errorColor
        return $false
    }
}

try {
    Write-Header "Updating John Wick Movie Characters"
    
    # Get all movies
    Write-Host "`nFetching movies..." -NoNewline
    $movies = Get-ApiData "movies"
    if (-not $movies) {
        throw "No movies found in the database!"
    }
    Write-Host "OK" -ForegroundColor $successColor
    
    # Get all characters
    Write-Host "Fetching characters..." -NoNewline
    $characters = Get-ApiData "characters"
    if (-not $characters) {
        throw "No characters found in the database!"
    }
    Write-Host "OK" -ForegroundColor $successColor
    
    # Find John Wick character (main character)
    $johnWick = $characters | Where-Object { $_.fullName -eq "John Wick" }
    $winston = $characters | Where-Object { $_.fullName -eq "Winston" }
    $charon = $characters | Where-Object { $_.fullName -eq "Charon" }
    $viggo = $characters | Where-Object { $_.fullName -eq "Viggo Tarasov" }
    $santino = $characters | Where-Object { $_.fullName -eq "Santino D'Antonio" }
    
    if (-not $johnWick) {
        throw "John Wick character not found!"
    }
    
    Write-Header "Found Characters"
    Write-Host "John Wick ID: $($johnWick.id)"
    Write-Host "Winston ID: $($winston.id)"
    Write-Host "Charon ID: $($charon.id)"
    Write-Host "Viggo ID: $($viggo.id)"
    Write-Host "Santino ID: $($santino.id)"
    
    # Update each movie's characters
    Write-Header "Updating Movies"
    
    # John Wick (2014)
    $movie1 = $movies | Where-Object { $_.movieTitle -eq "John Wick" }
    if ($movie1) {
        Write-Host "`nUpdating John Wick (2014)..."
        $movie1Characters = @($johnWick.id, $winston.id, $charon.id, $viggo.id)
        Update-MovieCharacters -movieId $movie1.id -characterIds $movie1Characters
    }
    
    # John Wick: Chapter 2
    $movie2 = $movies | Where-Object { $_.movieTitle -eq "John Wick: Chapter 2" }
    if ($movie2) {
        Write-Host "`nUpdating John Wick: Chapter 2..."
        $movie2Characters = @($johnWick.id, $winston.id, $charon.id, $santino.id)
        Update-MovieCharacters -movieId $movie2.id -characterIds $movie2Characters
    }
    
    # John Wick: Chapter 3
    $movie3 = $movies | Where-Object { $_.movieTitle -eq "John Wick: Chapter 3 - Parabellum" }
    if ($movie3) {
        Write-Host "`nUpdating John Wick: Chapter 3..."
        $movie3Characters = @($johnWick.id, $winston.id, $charon.id)
        Update-MovieCharacters -movieId $movie3.id -characterIds $movie3Characters
    }
    
    # John Wick: Chapter 4
    $movie4 = $movies | Where-Object { $_.movieTitle -eq "John Wick: Chapter 4" }
    if ($movie4) {
        Write-Host "`nUpdating John Wick: Chapter 4..."
        $movie4Characters = @($johnWick.id, $winston.id, $charon.id)
        Update-MovieCharacters -movieId $movie4.id -characterIds $movie4Characters
    }
    
    Write-Host "`nAll movie characters updated successfully!" -ForegroundColor $successColor
}
catch {
    Write-Host "`nScript encountered an error:" -ForegroundColor $errorColor
    Write-Host $_.Exception.Message -ForegroundColor $errorColor
    Write-Host $_.ScriptStackTrace -ForegroundColor $errorColor
}
finally {
    # Keep window open
    if ($Host.Name -eq "ConsoleHost" -and -not $wasWindowsTerminal) {
        Write-Host "`nPress any key to exit..." -ForegroundColor $infoColor
        $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
    }
}