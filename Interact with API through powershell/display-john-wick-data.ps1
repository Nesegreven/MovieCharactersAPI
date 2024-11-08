# Prevent the window from closing immediately if run directly
if ($Host.Name -eq "ConsoleHost") {
    $wasWindowsTerminal = $env:WT_SESSION
}

# Script to display John Wick franchise data
$baseUrl = "http://localhost:5000/api"
$successColor = "Green"
$errorColor = "Red"
$infoColor = "Yellow"
$dataColor = "Cyan"
$headerColor = "Magenta"
$detailColor = "White"

function Write-Header {
    param (
        [string]$title
    )
    Write-Host "`n=== $title ===" -ForegroundColor $headerColor
}

function Write-SubHeader {
    param (
        [string]$title
    )
    Write-Host "`n--- $title ---" -ForegroundColor $infoColor
}

function Write-PropertyValue {
    param (
        [string]$property,
        $value
    )
    Write-Host "$property`: " -NoNewline -ForegroundColor $dataColor
    Write-Host "$value" -ForegroundColor $detailColor
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

function Display-MovieCharacters {
    param (
        [int]$movieId,
        [array]$allCharacters
    )
    
    $movieCharacters = Get-ApiData "movies/$movieId/characters"
    if ($movieCharacters) {
        Write-SubHeader "Characters in this movie"
        foreach ($charId in $movieCharacters.characterIds) {
            $character = $allCharacters | Where-Object { $_.id -eq $charId }
            if ($character) {
                Write-Host "• $($character.fullName)" -NoNewline -ForegroundColor $detailColor
                if ($character.alias) {
                    Write-Host " ($($character.alias))" -ForegroundColor $infoColor
                }
                else {
                    Write-Host ""
                }
            }
        }
    }
}

try {
    # First verify the API is running
    Write-Host "Connecting to API..." -NoNewline
    try {
        $null = Invoke-WebRequest -Uri "$baseUrl/movies" -Method GET -UseBasicParsing
        Write-Host "OK" -ForegroundColor $successColor
    }
    catch {
        throw "Could not connect to API at $baseUrl. Please ensure the API is running."
    }

    # Get all franchises and find John Wick franchise
    Write-Header "Fetching John Wick Franchise Data"
    $franchises = Get-ApiData "franchises"
    $johnWickFranchise = $franchises | Where-Object { $_.name -like "*John Wick*" }

    if (-not $johnWickFranchise) {
        throw "John Wick franchise not found in the database!"
    }

    # Display Franchise Information
    Write-Header "Franchise Information"
    Write-PropertyValue "Name" $johnWickFranchise.name
    Write-PropertyValue "Description" $johnWickFranchise.description
    Write-PropertyValue "ID" $johnWickFranchise.id

    # Get all characters for reference
    $allCharacters = Get-ApiData "characters"
    
    # Get and display all characters in the franchise
    Write-Header "All Characters"
    $franchiseCharacters = Get-ApiData "franchises/$($johnWickFranchise.id)/characters"
    
    if ($franchiseCharacters) {
        foreach ($character in $franchiseCharacters) {
            Write-SubHeader $character.fullName
            Write-PropertyValue "ID" $character.id
            if ($character.alias) { Write-PropertyValue "Alias" $character.alias }
            Write-PropertyValue "Gender" $character.gender
            Write-PropertyValue "Picture URL" $character.pictureUrl
        }
    }

    # Get and display all movies in the franchise
    Write-Header "Movies"
    $franchiseMovies = Get-ApiData "franchises/$($johnWickFranchise.id)/movies"
    
    if ($franchiseMovies) {
        foreach ($movie in $franchiseMovies | Sort-Object releaseYear) {
            Write-SubHeader $movie.movieTitle
            Write-PropertyValue "ID" $movie.id
            Write-PropertyValue "Release Year" $movie.releaseYear
            Write-PropertyValue "Director" $movie.director
            Write-PropertyValue "Genre" $movie.genre
            Write-PropertyValue "Picture URL" $movie.pictureUrl
            Write-PropertyValue "Trailer URL" $movie.trailerUrl
            
            # Display characters in this movie
            Display-MovieCharacters -movieId $movie.id -allCharacters $allCharacters
        }
    }

    Write-Host "`nData retrieval completed successfully!" -ForegroundColor $successColor
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
