# CatsLife

CatsLife is a .NET console application that retrieves random cat facts from an external API and saves them to a text file.

The application uses dependency injection, external configuration, logging, error handling and command-line configuration overrides.

## Features

- Fetches random cat facts from an external API
- Saves retrieved facts to a text file
- Dependency Injection
- Asynchronous API and file operations
- JSON deserialization with `System.Text.Json`
- Configuration using `appsettings.json`
- Command-line configuration overrides
- Configurable number of API requests
- Configurable interval between requests
- Logging with Serilog
- Error handling for HTTP, JSON and file system errors
- Automatic creation of output directories
- Displays the active configuration when the application starts

## Requirements

- .NET 10 SDK

## Build

Clone the repository and navigate to the solution directory:

```bash
git clone <repository-url>
cd CatsLife
```

Restore dependencies and build the solution:

```bash
dotnet restore
dotnet build
```

## Run

Run the application using:

```bash
dotnet run --project CatsLife
```

The default configuration is stored in `appsettings.json`.

By default, the application stores output files in:

```text
C:\CatsLife\CatFacts.txt
C:\CatsLife\CatFactsLog.txt
```

## Configuration

Example `appsettings.json`:

```json
{
  "CatFactApi": {
    "Url": "https://catfact.ninja/fact"
  },
  "Files": {
    "FactFilePath": "C:\\CatsLife\\CatFacts.txt",
    "LogFilePath": "C:\\CatsLife\\CatFactsLog.txt"
  },
  "TimeSettings": {
    "IntervalMilliSeconds": 1500,
    "MaxFacts": 5
  }
}
```

## Command-line options

Configuration values can be overridden using command-line arguments.

| Option | Description |
| --- | --- |
| `-count` | Number of cat facts to retrieve |
| `-interval` | Interval between requests in milliseconds |
| `-fact` | Path to the cat facts output file |
| `-log` | Path to the log file |
| `-url` | Cat Facts API URL |

When using `dotnet run`, application arguments should be placed after `--`.

### Examples

Retrieve 3 facts:

```bash
dotnet run --project CatsLife -- -count 3
```

Retrieve 5 facts with a 2-second interval:

```bash
dotnet run --project CatsLife -- -count 5 -interval 2000
```

Use custom output paths:

```bash
dotnet run --project CatsLife -- -fact "C:\Facts\facts.txt" -log "C:\Logs\catslife.log"
```

Multiple options can be combined:

```bash
dotnet run --project CatsLife -- -count 3 -interval 1000 -fact "C:\Facts\facts.txt"
```

Command-line values override the corresponding values from `appsettings.json`.

## Project structure

```text
CatsLife
├── CatsLife
│   ├── Program.cs
│   ├── CatsLifeAppFlow.cs
│   └── appsettings.json
│
└── CatsLifeServices
    ├── Interfaces
    │   ├── IGetFact.cs
    │   └── IFileWriter.cs
    ├── Models
    │   └── CatFact.cs
    ├── Providers
    │   └── ProviderAPI.cs
    └── Writers
        └── FileWriterTXT.cs
```

`CatsLife` is responsible for application startup, configuration and application flow.

`CatsLifeServices` contains the API provider, file writer, interfaces and data model.

## Error handling

The application handles common runtime errors including:

- HTTP request failures
- Invalid JSON responses
- File I/O errors
- Insufficient file system permissions
- Invalid `-count` and `-interval` values

Technical error details are written to the configured log file, while user-friendly messages are displayed in the console.
