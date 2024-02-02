# MongoDB-Verbindungsdaten
$MongoDBConnectionString = "mongodb://localhost:27017"
$DatabaseName = "Modul165"
$Collections = @("registrations", "Users")

# MongoDB-Client erstellen
$MongoClient = New-Object MongoDB.Driver.MongoClient($MongoDBConnectionString)

# MongoDB-Datenbank abrufen oder erstellen
$Database = $MongoClient.GetDatabase($DatabaseName)

# Überprüfen, ob die Sammlungen vorhanden sind, und sie ggf. erstellen
foreach ($CollectionName in $Collections) {
    $CollectionExists = $Database.ListCollectionNames().ToList() -contains $CollectionName
    if (-not $CollectionExists) {
        $Database.CreateCollection($CollectionName)
        Write-Host "Sammlung '$CollectionName' wurde erstellt."
    } else {
        Write-Host "Sammlung '$CollectionName' existiert bereits."
    }
}

Write-Host "Datenbank '$DatabaseName' mit den Sammlungen $($Collections -join ', ') wurde erstellt oder ist bereits vorhanden."
