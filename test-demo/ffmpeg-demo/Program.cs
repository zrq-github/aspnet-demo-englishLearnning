using System.Diagnostics;
using System.Reflection;
using FFmpeg.NET;

namespace ffmpeg_demo;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // 获取exe的路径
        var exePath = Assembly.GetExecutingAssembly().Location;
        var parentDir = Directory.GetParent(exePath).Parent.Parent.Parent.FullName;

        var ffmpegPath = Path.Combine(parentDir, "ffmpeg.exe");

        var sourceFile = Path.Combine(parentDir, "sourceFile", "123.mp3");
        var destFile = Path.Combine(parentDir, "destFile", "123.m4a");

        var inputFile = new InputFile(sourceFile);
        var outputFile = new OutputFile(destFile);

        CancellationToken ct = default;

        var ffmpeg = new Engine(ffmpegPath);
        string? errorMsg = null;
        ffmpeg.Data += (s, e) =>
        {
            var input = e.Input;
            var output = e.Output;
            var data = e.Data;
        };
        ffmpeg.Error += (s, e) =>
        {
            errorMsg = e.Exception.Message;
        };

        ffmpeg.ConvertAsync(inputFile, outputFile, ct).Wait(ct);
    }

    private static void RunFFmpeg(string ffmpegPath, string arguments)
    {
        try
        {
            // 创建 Process 实例
            using (var process = new Process())
            {
                // 设置进程启动信息
                process.StartInfo.FileName = ffmpegPath;         // FFmpeg 可执行文件路径
                process.StartInfo.Arguments = arguments;         // FFmpeg 命令行参数
                process.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
                process.StartInfo.RedirectStandardError = true;  // 重定向错误输出
                process.StartInfo.UseShellExecute = false;       // 不使用操作系统外壳程序启动
                process.StartInfo.CreateNoWindow = true;         // 不创建窗口

                // 订阅错误输出事件
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data)) Console.WriteLine($"Error: {e.Data}"); // 输出错误信息
                };

                // 启动进程
                process.Start();
                process.BeginErrorReadLine(); // 开始异步读取错误输出

                // 等待进程退出
                process.WaitForExit();

                // 检查退出代码
                if (process.ExitCode == 0)
                    Console.WriteLine("转换成功！");
                else
                    Console.WriteLine($"FFmpeg 退出时出现错误，退出代码: {process.ExitCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"调用 FFmpeg 时发生错误: {ex.Message}");
        }
    }
}
