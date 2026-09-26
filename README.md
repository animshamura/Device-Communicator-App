# DeviceCommunicatorApp

A Windows desktop tool for communicating with network devices over TCP. It supports connection setup, text and hex payload sending, receive logging, and protocol-style terminal inspection.

## Overview

This project is a lightweight device communication client built with .NET WinForms. It is useful for testing simple serial/TCP-style device interfaces, sending commands, and logging byte traffic to inspect how a device responds.

## Project Structure

```text
DeviceCommunicatorApp/
├── src/
│   └── DeviceCommunicatorApp/
│       ├── App/
│       │   └── Program.cs
│       ├── Forms/
│       │   ├── Form1.cs
│       │   └── Form1.Designer.cs
│       ├── Models/
│       │   └── PacketModel.cs
│       └── DeviceCommunicatorApp.csproj
├── tests/
│   └── DeviceCommunicatorApp.Tests/
│       ├── PacketModelTests.cs
│       └── DeviceCommunicatorApp.Tests.csproj
├── docs/
│   └── screenshots/
├── .gitignore
├── README.md
└── DeviceCommunicatorApp.slnx
```

## Features

- Connect to a target IP and port
- Send ASCII text payloads
- Send raw hex payloads
- Append CRLF automatically when needed
- Receive and log device responses in terminal view
- Toggle between ASCII and hex logging
- Keep recent command history with keyboard navigation
- Built-in clear log action and connection controls

## Prerequisites

- Windows 10/11
- .NET 10 SDK
- Visual Studio 2022 or later with Windows desktop support

## Run locally

```bash
dotnet restore
Dotnet build DeviceCommunicatorApp.slnx
```

Then open the solution in Visual Studio and run the app.

## Run tests

```bash
dotnet test DeviceCommunicatorApp.slnx
```

## Screenshot

Place app screenshots in the folder below:

- docs/screenshots/

Example:

![Main UI](docs/screenshots/device-communicator-main.png)

## Notes

This project is intentionally simple and focused on testability and clarity. It is a good base for adding protocol parsing, packet filters, replay tools, and richer device automation features.
