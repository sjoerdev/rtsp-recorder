# RTSP Recorder
This is a camera stream recorder that can be used through the command line.

## Requirements:

- install ffmpeg: ``winget install ffmpeg``
- install dotnet: ``winget install Microsoft.DotNet.Runtime.9``

## Usage:

> [!TIP]
> the username and password of your ip camera is usually on the back of the camera
>
> the ip adress of the camera can be seen in the setting of your router
>
> ip cameras usually use one of these ports: ``554`` or ``8554``
>
> example of an rtsp link: ``rtsp://admin:admin@192.168.2.21:554``

**how do i formulate the rtsp link?:**

- with username and password: ``rtsp://username:password@ipadress:port``
- with username but no password: ``rtsp://username@ipadress:port``
- with no usernamne and no password: ``rtsp://ipadress:port``

**how do i formulate the full command?:**

- Template: ``<Executable> <RTSP_URL> <Minutes_Per_Chunk> <Base_Output_Directory>``
- Example: ``./rtsp-recorder.exe rtsp://username:password@ipadress:port 60 ./``

## Building:

Download .NET 9: https://dotnet.microsoft.com/en-us/download

Windows: ``dotnet publish -o ./build/windows --sc true -r win-x64 -c release``

Linux: ``dotnet publish -o ./build/linux --sc true -r linux-x64 -c release``
