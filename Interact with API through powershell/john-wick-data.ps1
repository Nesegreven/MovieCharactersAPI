# Prevent the window from closing immediately if run directly
if ($Host.Name -eq "ConsoleHost") {
    $wasWindowsTerminal = $env:WT_SESSION
}

# Script to add John Wick franchise data - using the same URL as the working test script
$baseUrl = "http://localhost:5000/api"
$successColor = "Green"
$errorColor = "Red"
$infoColor = "Yellow"
$dataColor = "Cyan"

# Add error handling variables
$ErrorActionPreference = "Stop"
$overallSuccess = $true

function Write-TestHeader {
    param (
        [string]$title
    )
    Write-Host "`n=== $title ===" -ForegroundColor $infoColor
}

function Write-JsonResponse {
    param (
        [object]$response
    )
    if ($response) {
        Write-Host "Response:" -ForegroundColor $dataColor
        Write-Host ($response | ConvertTo-Json -Depth 10) -ForegroundColor $dataColor
    }
}

function Invoke-ApiEndpoint {
    param (
        [string]$name,
        [string]$url,
        [string]$method = "GET",
        [object]$body = $null
    )
    
    try {
        Write-Host "`nExecuting: $name... " -NoNewline

        $params = @{
            Method = $method
            Uri = $url
            ContentType = "application/json"
            UseBasicParsing = $true
        }

        if ($body) {
            $params.Body = ($body | ConvertTo-Json)
            Write-Host "`nRequest Body:" -ForegroundColor $dataColor
            Write-Host ($body | ConvertTo-Json) -ForegroundColor $dataColor
        }

        $response = Invoke-RestMethod @params
        Write-Host "OK" -ForegroundColor $successColor
        Write-JsonResponse $response
        return $response
    }
    catch {
        $script:overallSuccess = $false
        Write-Host "Failed" -ForegroundColor $errorColor
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor $errorColor
        if ($_.ErrorDetails) {
            Write-Host "  Details: $($_.ErrorDetails)" -ForegroundColor $errorColor
        }
        return $null
    }
}

# First verify the API is running by doing a simple GET request
try {
    Write-Host "`nVerifying API is running at $baseUrl..." -NoNewline
    $testResponse = Invoke-WebRequest -Uri "$baseUrl/movies" -Method GET -UseBasicParsing
    Write-Host "OK" -ForegroundColor $successColor
}
catch {
    Write-Host "Failed" -ForegroundColor $errorColor
    Write-Host "`nError: Could not connect to the API. Please ensure:"
    Write-Host "1. The API is running"
    Write-Host "2. It's accessible at $baseUrl"
    Write-Host "3. You have started the API with administrator privileges if required"
    Write-Host "`nSpecific error: $($_.Exception.Message)" -ForegroundColor $errorColor
    exit
}

# Main script
try {
    Write-TestHeader "Adding John Wick Franchise Data"

    # 1. Create the Franchise
    Write-TestHeader "Creating John Wick Franchise"
    $franchise = Invoke-ApiEndpoint -name "Create Franchise" -url "$baseUrl/franchises" -method "POST" -body @{
        name = "John Wick"
        description = "Neo-noir action thriller film series following legendary hitman John Wick's return to the criminal underworld"
    }

    if ($franchise) {
        # 2. Create Characters
        Write-TestHeader "Creating Characters"
        $characters = @(
            @{
                fullName = "John Wick"
                alias = "Baba Yaga"
                gender = "Male"
                pictureUrl = "https://example.com/john-wick.jpg"
            },
            @{
                fullName = "Winston"
                alias = "Manager of the Continental Hotel"
                gender = "Male"
                pictureUrl = "https://example.com/winston.jpg"
            },
            @{
                fullName = "Charon"
                alias = "Continental Concierge"
                gender = "Male"
                pictureUrl = "https://example.com/charon.jpg"
            },
            @{
                fullName = "Viggo Tarasov"
                alias = "Russian Crime Boss"
                gender = "Male"
                pictureUrl = "https://example.com/viggo.jpg"
            },
            @{
                fullName = "Santino D'Antonio"
                alias = "Italian Crime Lord"
                gender = "Male"
                pictureUrl = "https://example.com/santino.jpg"
            }
        )

        $createdCharacters = @()
        foreach ($character in $characters) {
            $result = Invoke-ApiEndpoint -name "Create Character $($character.fullName)" -url "$baseUrl/characters" -method "POST" -body $character
            if ($result) {
                $createdCharacters += $result
            }
        }

        # 3. Create Movies
        Write-TestHeader "Creating Movies"
        $movies = @(
            @{
                movieTitle = "John Wick"
                genre = "Action, Thriller"
                releaseYear = 2014
                director = "Chad Stahelski"
                pictureUrl = "https://example.com/john-wick-1.jpg"
                trailerUrl = "https://example.com/john-wick-1-trailer"
                franchiseId = $franchise.id
            },
            @{
                movieTitle = "John Wick: Chapter 2"
                genre = "Action, Thriller"
                releaseYear = 2017
                director = "Chad Stahelski"
                pictureUrl = "https://example.com/john-wick-2.jpg"
                trailerUrl = "https://example.com/john-wick-2-trailer"
                franchiseId = $franchise.id
            },
            @{
                movieTitle = "John Wick: Chapter 3 – Parabellum"
                genre = "Action, Thriller"
                releaseYear = 2019
                director = "Chad Stahelski"
                pictureUrl = "https://example.com/john-wick-3.jpg"
                trailerUrl = "https://example.com/john-wick-3-trailer"
                franchiseId = $franchise.id
            },
            @{
                movieTitle = "John Wick: Chapter 4"
                genre = "Action, Thriller"
                releaseYear = 2023
                director = "Chad Stahelski"
                pictureUrl = "https://example.com/john-wick-4.jpg"
                trailerUrl = "https://example.com/john-wick-4-trailer"
                franchiseId = $franchise.id
            }
        )

        foreach ($movie in $movies) {
            $createdMovie = Invoke-ApiEndpoint -name "Create Movie $($movie.movieTitle)" -url "$baseUrl/movies" -method "POST" -body $movie
            
            if ($createdMovie) {
                # Add characters to the movie
                # John Wick is in all movies
                $movieCharacters = @($createdCharacters[0].id)  # John Wick

                # Add specific characters based on the movie
                switch ($movie.movieTitle) {
                    "John Wick" {
                        $movieCharacters += @($createdCharacters[1].id, $createdCharacters[2].id, $createdCharacters[3].id)  # Winston, Charon, Viggo
                    }
                    "John Wick: Chapter 2" {
                        $movieCharacters += @($createdCharacters[1].id, $createdCharacters[2].id, $createdCharacters[4].id)  # Winston, Charon, Santino
                    }
                    default {
                        $movieCharacters += @($createdCharacters[1].id, $createdCharacters[2].id)  # Winston, Charon
                    }
                }

                Invoke-ApiEndpoint -name "Add characters to $($movie.movieTitle)" -url "$baseUrl/movies/$($createdMovie.id)/characters" -method "PUT" -body @{
                    characterIds = $movieCharacters
                }
            }
        }
    }
}
catch {
    $overallSuccess = $false
    Write-Host "`nScript encountered an error:" -ForegroundColor $errorColor
    Write-Host $_.Exception.Message -ForegroundColor $errorColor
    Write-Host $_.ScriptStackTrace -ForegroundColor $errorColor
}
finally {
    if ($overallSuccess) {
        Write-Host "`nScript completed successfully!" -ForegroundColor $successColor
    }
    else {
        Write-Host "`nScript completed with errors. Please check the output above." -ForegroundColor $errorColor
    }
    
    # Keep window open
    if ($Host.Name -eq "ConsoleHost" -and -not $wasWindowsTerminal) {
        Write-Host "`nPress any key to exit..." -ForegroundColor $infoColor
        $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
    }
}