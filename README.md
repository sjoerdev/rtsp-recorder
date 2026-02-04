# RTSP Recorder
This is a camera stream recorder that can be used through the command line.

## Requirements:

install ffmpeg:

windows: ``winget install ffmpeg``

linux: ``sudo apt install ffmpeg``

## Usage:

Command Template: ``<Executable> <RTSP_URL> <Minutes_Per_Chunk> <Base_Output_Directory>``

Command Example: ``rtsp-recorder.exe rtsp://username:password@ipadress:port 60 ./``

## Building:

Download .NET 9: https://dotnet.microsoft.com/en-us/download

Windows: ``dotnet publish -o ./build/windows --sc true -r win-x64 -c release``

Linux: ``dotnet publish -o ./build/linux --sc true -r linux-x64 -c release``
