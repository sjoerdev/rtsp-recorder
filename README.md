# RTSP Recorder
This is a camera stream recorder that can be used through the command line.

## Requirements:

- **ffmpeg:**

  - install for windows: ``winget install ffmpeg``

  - install for linux: ``sudo apt install ffmpeg``

- **dotnet runtime:**

  - install for windows: ``winget install Microsoft.DotNet.Runtime.9``

## Usage:

> the username and password of your ip camera is usually on the back of the camera

Command Template: ``<Executable> <RTSP_URL> <Minutes_Per_Chunk> <Base_Output_Directory>``

Camera with username and password: ``rtsp-recorder.exe rtsp://username:password@ipadress:port 60 ./``

Camera with username but no password: ``rtsp-recorder.exe rtsp://username@ipadress:port 60 ./``

Camera without username or password: ``rtsp-recorder.exe rtsp://ipadress:port 60 ./``

## Building:

Download .NET 9: https://dotnet.microsoft.com/en-us/download

Windows: ``dotnet publish -o ./build/windows --sc true -r win-x64 -c release``

Linux: ``dotnet publish -o ./build/linux --sc true -r linux-x64 -c release``
