# RTSP Recorder
This is a camera stream recorder that can be used through the command line.

## Requirements:

- install ffmpeg: ``winget install ffmpeg``
- install dotnet: ``winget install Microsoft.DotNet.Runtime.9``

## Usage:

**Run this command to start recording:**

- Template: ``<Executable> <RTSP_URL> <Minutes_Per_Chunk> <Base_Output_Directory>``
- Example: ``./rtsp-recorder.exe rtsp://username:password@ipadress:port 60 ./``

**How do i formulate the rtsp link?:**

> [!TIP]
> the username and password of your ip camera is usually on the back of the camera
>
> the ip adress of the camera can be seen in the settings of your router
>
> ip cameras usually use one of these ports: ``554`` or ``8554``
>
> example of an rtsp link: ``rtsp://admin:admin@192.168.2.21:554``

- with username and password: ``rtsp://username:password@ipadress:port``
- with username but no password: ``rtsp://username@ipadress:port``
- with no usernamne and no password: ``rtsp://ipadress:port``

**How do i find the ip adress of my ip camera if its connected to my router?:**

1. run the ``ipconfig`` command and look for the "Default Gateway" adress
2. fill in this adress in your browser like its a normal link to go to the router dashboard
3. log in to the dashboard of your router (usually the password and username is "admin")
4. look at the network topology or map and find your ip camera
5. select the ip camera and see what ip adress the router has given it
6. (optional) assign a static ip adress to the ip camera so the ip wont change randomly

**How do i find the ip adress of my ip camera if its connected to my computer?:**

1. make sure you have ``nmap`` installed by running ``winget install nmap``
2. scan for rtsp devices using nmap by running ``nmap -p 554 --open 192.168.1.0/24``
3. your camera ip adress should now be shown and should work like normal

**How do i connect my rtsp ip camera directly to my computer?:**

> [!tip]
> We can connect the camera directly to our computer without a router in between.
>
> We can do this by making our computer work like a tiny router.

1. Connect camera to your computer via ethernet
2. Manually set a static ip on your PC

> Example:
> 
> Computer IP: ``192.168.1.10``
> 
> Subnet: ``255.255.255.0``
> 
> Gateway: (leave empty)

3. Set camera static ip in the same subnet
> Example:
> 
> Camera IP: ``192.168.1.20``
> 
> Subnet: ``255.255.255.0``

## Building:

Download .NET 9: https://dotnet.microsoft.com/en-us/download

Windows: ``dotnet publish -o ./build/windows --sc true -r win-x64 -c release``

Linux: ``dotnet publish -o ./build/linux --sc true -r linux-x64 -c release``
