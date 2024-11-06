# Test script for Movie Characters API
$baseUrl = "http://localhost:5000/api"
$successColor = "Green"
$errorColor = "Red"
$infoColor = "Yellow"

# Store IDs of created test entities for cleanup
$testEntities = @{
    Characters = @()
    Movies = @()
    Franchises = @()
}

function Write-TestHeader {
    param (
        [string]$title
    )
    Write-Host "`n=== $title ===" -ForegroundColor $infoColor
}

function Test-Endpoint {
    param (
        [string]$name,
        [string]$url,
        [string]$method = "GET",
        [object]$body = $null,
        [hashtable]$expectedResponse = @{}
    )
    
    try {
        Write-Host "Testing: $name... " -NoNewline

        $params = @{
            Method = $method
            Uri = $url
            ContentType = "application/json"
            UseBasicParsing = $true
        }

        if ($body -and $method -ne "DELETE") {
            $params.Body = ($body | ConvertTo-Json)
        }

        if ($method -eq "DELETE") {
            try {
                Invoke-WebRequest @params | Out-Null
                Write-Host "OK" -ForegroundColor $successColor
                return $true
            }
            catch {
                Write-Host "Failed" -ForegroundColor $errorColor
                Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor $errorColor
                return $false
            }
        }
        else {
            $response = Invoke-RestMethod @params
            Write-Host "OK" -ForegroundColor $successColor

            if ($expectedResponse.Count -gt 0) {
                foreach ($key in $expectedResponse.Keys) {
                    if ($response.$key -ne $expectedResponse.$key) {
                        Write-Host "  Warning: Expected $key to be $($expectedResponse.$key) but got $($response.$key)" -ForegroundColor $infoColor
                    }
                }
            }

            return $response
        }
    }
    catch {
        Write-Host "Failed" -ForegroundColor $errorColor
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor $errorColor
        return $null
    }
}

function Clean-TestData {
    Write-TestHeader "Cleaning up test data"

    # Delete test characters
    foreach ($id in $testEntities.Characters) {
        Test-Endpoint -name "Delete test character $id" -url "$baseUrl/characters/$id" -method "DELETE"
    }

    # Delete test movies
    foreach ($id in $testEntities.Movies) {
        Test-Endpoint -name "Delete test movie $id" -url "$baseUrl/movies/$id" -method "DELETE"
    }

    # Delete test franchises
    foreach ($id in $testEntities.Franchises) {
        Test-Endpoint -name "Delete test franchise $id" -url "$baseUrl/franchises/$id" -method "DELETE"
    }
}

# Main test script
Write-Host "Starting API Test Suite" -ForegroundColor $infoColor
Write-Host "Base URL: $baseUrl" -ForegroundColor $infoColor

# Test Franchises
Write-TestHeader "Testing Franchise Endpoints"

# Get all franchises
$franchises = Test-Endpoint -name "Get all franchises" -url "$baseUrl/franchises"

if ($franchises) {
    $franchiseId = $franchises[0].id
    
    # Get specific franchise
    Test-Endpoint -name "Get franchise by ID" -url "$baseUrl/franchises/$franchiseId"

    # Get movies in franchise
    Test-Endpoint -name "Get movies in franchise" -url "$baseUrl/franchises/$franchiseId/movies"

    # Get characters in franchise
    Test-Endpoint -name "Get characters in franchise" -url "$baseUrl/franchises/$franchiseId/characters"

    # Create new franchise
    $newFranchise = Test-Endpoint -name "Create new franchise" -url "$baseUrl/franchises" -method "POST" -body @{
        name = "Test Franchise"
        description = "Test Description"
    }

    if ($newFranchise) {
        $testEntities.Franchises += $newFranchise.id

        # Update franchise
        Test-Endpoint -name "Update franchise" -url "$baseUrl/franchises/$($newFranchise.id)" -method "PUT" -body @{
            name = "Updated Test Franchise"
            description = "Updated Test Description"
        }
    }
}

# Test Movies
Write-TestHeader "Testing Movie Endpoints"

# Get all movies
$movies = Test-Endpoint -name "Get all movies" -url "$baseUrl/movies"

if ($movies) {
    $movieId = $movies[0].id

    # Get specific movie
    Test-Endpoint -name "Get movie by ID" -url "$baseUrl/movies/$movieId"

    # Get characters in movie
    Test-Endpoint -name "Get characters in movie" -url "$baseUrl/movies/$movieId/characters"

    # Create new movie
    $newMovie = Test-Endpoint -name "Create new movie" -url "$baseUrl/movies" -method "POST" -body @{
        movieTitle = "Test Movie"
        genre = "Test Genre"
        releaseYear = 2024
        director = "Test Director"
        pictureUrl = "https://example.com/test.jpg"
        trailerUrl = "https://example.com/test-trailer"
    }

    if ($newMovie) {
        $testEntities.Movies += $newMovie.id

        # Update movie
        Test-Endpoint -name "Update movie" -url "$baseUrl/movies/$($newMovie.id)" -method "PUT" -body @{
            movieTitle = "Updated Test Movie"
            genre = "Updated Test Genre"
            releaseYear = 2024
            director = "Updated Test Director"
            pictureUrl = "https://example.com/test-updated.jpg"
            trailerUrl = "https://example.com/test-trailer-updated"
        }

        # Update movie characters
        Test-Endpoint -name "Update movie characters" -url "$baseUrl/movies/$($newMovie.id)/characters" -method "PUT" -body @{
            characterIds = @(1, 2)
        }
    }
}

# Test Characters
Write-TestHeader "Testing Character Endpoints"

# Get all characters
$characters = Test-Endpoint -name "Get all characters" -url "$baseUrl/characters"

if ($characters) {
    $characterId = $characters[0].id

    # Get specific character
    Test-Endpoint -name "Get character by ID" -url "$baseUrl/characters/$characterId"

    # Create new character
    $newCharacter = Test-Endpoint -name "Create new character" -url "$baseUrl/characters" -method "POST" -body @{
        fullName = "Test Character"
        alias = "Test Alias"
        gender = "Other"
        pictureUrl = "https://example.com/test-char.jpg"
    }

    if ($newCharacter) {
        $testEntities.Characters += $newCharacter.id

        # Update character
        Test-Endpoint -name "Update character" -url "$baseUrl/characters/$($newCharacter.id)" -method "PUT" -body @{
            fullName = "Updated Test Character"
            alias = "Updated Test Alias"
            gender = "Other"
            pictureUrl = "https://example.com/test-char-updated.jpg"
        }
    }
}

# Clean up all test data
Clean-TestData

Write-Host "`nTest Suite Completed!" -ForegroundColor $successColor