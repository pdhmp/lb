using System;
using System.IO;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;

public class FileSync
{
    public string sourcePath = "";
    public string destinationPath = "";
    public string StatusMsg = "";

    public void Sync()
    {
        try
        {

            StatusMsg = StatusMsg + "=================================================\r\n\r\n" + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "\tANALYZING FOLDER: " + sourcePath + "\r\n";

            FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                                        FileSyncOptions.RecycleDeletedFiles | 
                                        FileSyncOptions.RecyclePreviousFileOnUpdates | 
                                        FileSyncOptions.RecycleConflictLoserFiles;

            FileSyncScopeFilter filter = new FileSyncScopeFilter();
            filter.FileNameExcludes.Add("*.lnk"); // Exclude all *.lnk files
            filter.FileNameExcludes.Add("File.ID");

            Guid sourceId = GetSyncID(sourcePath + "\\File.ID");
            Guid destId = GetSyncID(destinationPath + "\\File.ID");

            FileSyncProvider sourceProvider = new FileSyncProvider(sourceId, sourcePath, filter, options);
            FileSyncProvider destProvider = new FileSyncProvider(destId, destinationPath, filter, options);

            destProvider.ApplyingChange += new EventHandler<ApplyingChangeEventArgs>(OnApplyingChange);
            destProvider.AppliedChange += new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
            destProvider.SkippedChange += new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);
            destProvider.DetectedChanges += new EventHandler<DetectedChangesEventArgs>(OnDetectedChanges);

            destProvider.DetectChanges();
            sourceProvider.DetectChanges();

            StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "\tSTARTING SYNC\r\n";

            SyncOrchestrator agent = new SyncOrchestrator();
            agent.LocalProvider = sourceProvider;
            agent.RemoteProvider = destProvider;
            agent.Direction = SyncDirectionOrder.Upload;

            agent.Synchronize();
            StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "SYNC COMPLETE!\r\n";

        }
        catch (Exception e)
        {
            StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "Exception from File Sync Provider:\r\n" + e.ToString() + "\r\n";
        }
    }

    public void OnApplyingChange(object sender, ApplyingChangeEventArgs args)
    {
        switch (args.ChangeType)
        {
            case ChangeType.Create:
                StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- CREATE \t" + args.NewFileData.Name + "\r\n";
                break;
            case ChangeType.Delete:
                StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- DELETE \t" + args.CurrentFileData.Name + "\r\n";
                break;
            case ChangeType.Update:
                StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- OVERWRITE \t" + args.CurrentFileData.Name + "\r\n";
                break;
            case ChangeType.Rename:
                StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- RENAME \t" + args.CurrentFileData.Name + " as " + args.NewFileData.Name + "\r\n";
                break;
        }
    }

    public void OnDetectedChanges(object sender, DetectedChangesEventArgs args)
    {
        StatusMsg = StatusMsg + "-- TotalDirectoriesFound " + args.TotalDirectoriesFound + "\r\n";
        StatusMsg = StatusMsg + "-- TotalFilesFound " + args.TotalFilesFound + "\r\n";
        StatusMsg = StatusMsg + "-- TotalFileSize " + args.TotalFileSize + "\r\n";
    }

    public void OnAppliedChange(object sender, AppliedChangeEventArgs args)
    {
        switch(args.ChangeType)
        {
            case ChangeType.Create:
                StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- Applied CREATE for file " + args.NewFilePath + "\r\n"; 
               break;
            case ChangeType.Delete:
               StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- Applied DELETE for file " + args.OldFilePath + "\r\n"; 
               break;
            case ChangeType.Update:
               StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- Applied OVERWRITE for file " + args.OldFilePath + "\r\n"; 
               break;
            case ChangeType.Rename:
               StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- Applied RENAME for file " + args.OldFilePath + " as " + args.NewFilePath + "\r\n"; 
               break;
        }
    }

    public void OnSkippedChange(object sender, SkippedChangeEventArgs args)
    {
        StatusMsg = StatusMsg + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "-- Skipped applying " + args.ChangeType.ToString().ToUpper() + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ? args.CurrentFilePath : args.NewFilePath) + " due to error";
              
        if (args.Exception != null)
            StatusMsg = StatusMsg + "   [" + args.Exception.Message + "]\r\n\r\n";
    }

    private static Guid GetSyncID(string syncFilePath)
    {
        Guid guid;
        SyncId replicaID = null;

        if (!File.Exists(syncFilePath))
        {
            guid = Guid.NewGuid();
            replicaID = new SyncId(guid);
            FileStream fs = File.Open(syncFilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(guid.ToString());
            sw.Close();
            fs.Close();
        }

        else
        {

            FileStream fs = File.Open(syncFilePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string guidString = sr.ReadLine();
            guid = new Guid(guidString);
            replicaID = new SyncId(guid);
            sr.Close();
            fs.Close();
        }

        return (guid);
    }


}
