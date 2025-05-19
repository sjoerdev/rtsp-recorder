using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // validate argumenta
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <Executable> <RTSP_URL> <Minutes_Per_Chunk> <Base_Output_Directory>");
            return;
        }

        // gather arguments
        string rtsp = args[0];
        float minutesPerChunk = float.Parse(args[1]);
        string baseOutputDirectory = args[2];

        // convert minutes to seconds
        int secondsPerChunk = (int)(minutesPerChunk * 60);

        // create timestamped output directory
        string folderName = DateTime.Now.ToString("dd_MM_yyyy-HH_mm_ss");
        string outputDir = Path.Combine(baseOutputDirectory, folderName);
        Directory.CreateDirectory(outputDir);

        // give feedback in the terminal
        Console.WriteLine("Recording started. (Press Ctrl+C to stop)");

        // create label for filename
        string label = minutesPerChunk % 1 == 0 ? $"{(int)minutesPerChunk}min" : $"{secondsPerChunk}sec";
        string filenamePattern = $"chunk_%03d_{label}.mp4";

        // determine ffmpeg arguments
        var ffmpeg_args = $"-hide_banner -loglevel quiet -rtsp_transport tcp -i \"{rtsp}\" -c copy -f segment -segment_time {secondsPerChunk} -reset_timestamps 1 \"{Path.Combine(outputDir, filenamePattern)}\"";

        // find and use ffmpeg
        var ffmpeg_process = new Process();
        ffmpeg_process.StartInfo.FileName = "ffmpeg";
        ffmpeg_process.StartInfo.Arguments = ffmpeg_args;
        ffmpeg_process.StartInfo.UseShellExecute = false;
        ffmpeg_process.StartInfo.RedirectStandardInput = true;
        ffmpeg_process.StartInfo.CreateNoWindow = true;
        ffmpeg_process.Start();

        // gracefully end recording when ctrl+c is pressed
        Console.CancelKeyPress += (sender, args) =>
        {
            //Console.WriteLine("Stopping recording...");
            ffmpeg_process.StandardInput.WriteLine("q");
            args.Cancel = true;
        };

        var startTime = DateTime.Now;
        int currentChunk = 0;

        // take away the console cursor
        Console.CursorVisible = false;

        // while ffmpeg is recording
        while (!ffmpeg_process.HasExited)
        {
            var elapsed = (DateTime.Now - startTime).TotalSeconds;
            var percent = elapsed / secondsPerChunk * 100;

            if (percent > 100)
            {
                currentChunk++;
                startTime = DateTime.Now;
                percent = 0;
            }

            OverWriteLineAndReturn("Recording chunk " + currentChunk.ToString() + " - " + percent.ToString("0.00") + "%");
            Thread.Sleep(100);
        }

        OverWriteLine("Recording saved to: " + outputDir);
        Console.CursorVisible = true;
    }

    static void OverWriteLine(string message)
    {
        Console.Write("\r" + message.PadRight(Console.WindowWidth - 1));
    }
    
    static void OverWriteLineAndReturn(string message)
    {
        Console.Write("\r" + message.PadRight(Console.WindowWidth - 1) + "\r");
    }
}