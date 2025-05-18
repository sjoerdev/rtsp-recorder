using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static float minutesPerChunk = 0.1f;
    static int secondsPerChunk = (int)(minutesPerChunk * 60);
    static string rtsp = "rtsp://admin:DoomSlayer04@192.168.2.21:554";

    static void Main(string[] args)
    {
        // get base directory (where the .exe is located)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // create timestamped output directory
        string folderName = DateTime.Now.ToString("dd_MM_yyyy-HH_mm_ss");
        string outputDir = Path.Combine(baseDir, folderName);
        Directory.CreateDirectory(outputDir);

        // give feedback in the terminal
        Console.WriteLine($"Recording to {outputDir}");
        Console.WriteLine($"Each chunk length: {minutesPerChunk} minute(s) ({secondsPerChunk} seconds)");
        Console.WriteLine("Press Ctrl+C to stop.");

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
            Console.WriteLine("\nStopping recording...");
            ffmpeg_process.StandardInput.WriteLine("q");
            args.Cancel = true;
        };

        var startTime = DateTime.Now;
        int currentChunk = 0;

        // while ffmpeg is still recording
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

            Console.Write("\rRecording chunk " + currentChunk.ToString() + " - " + percent.ToString("0.00") + "%");
            Thread.Sleep(100);
        }

        Console.WriteLine("Recording stopped.");
    }
}