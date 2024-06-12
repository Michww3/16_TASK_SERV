using System;
using System.IO;
using System.ServiceProcess;

public class Program
{
    public static void Main()
    {
        ServiceBase[] ServicesToRun;
        ServicesToRun = new ServiceBase[]
        {
            new MonitorService()
        };
        ServiceBase.Run(ServicesToRun);
    }
}

public class MonitorService : ServiceBase
{
    private FileSystemWatcher watcher;

    public MonitorService()
    {
        ServiceName = "MonitorService";
    }

    protected override void OnStart(string[] args)
    {
        watcher = new FileSystemWatcher(@"C:\Users\User\Desktop");
        watcher.Deleted += OnDeleted;

        watcher.EnableRaisingEvents = true;
    }

    protected override void OnStop()
    {
        watcher.EnableRaisingEvents = false;
        watcher.Dispose();
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        File.AppendAllText(@"C:\Users\User\Desktop\Service.txt", $"File deleted: {e.FullPath}\n");
    }
}
