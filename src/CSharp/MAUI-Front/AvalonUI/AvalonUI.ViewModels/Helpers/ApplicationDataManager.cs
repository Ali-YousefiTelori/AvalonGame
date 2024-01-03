using AvalonUI.Models;
using EasyMicroservices.FileManager.Interfaces;
using EasyMicroservices.FileManager.Providers.DirectoryProviders;
using EasyMicroservices.FileManager.Providers.FileProviders;
using EasyMicroservices.FileManager.Providers.PathProviders;

namespace AvalonUI.Helpers;
public class ApplicationDataManager
{
    public static ApplicationDataManager Current { get; set; }
    public IFileManagerProvider FileManager { get; set; }
    public IDirectoryManagerProvider DirectoryManager { get; set; }
    public IPathProvider Path { get; set; }

    public ApplicationData ApplicationData { get; set; }

    public string ApplicationPath { get; set; }
    public string ApplicationDataPath
    {
        get
        {
            return Path.Combine(ApplicationPath, "Data.json");
        }
    }

    public static void Initialize(string appPath)
    {
        var path = new SystemPathProvider();
        string applicationPath = path.Combine(appPath, "Avalon");
        var directory = new DiskDirectoryProvider(applicationPath, path);
        Current = new ApplicationDataManager()
        {
            ApplicationPath = applicationPath,
            DirectoryManager = directory,
            FileManager = new DiskFileProvider(directory),
            Path = path
        };
    }

    public async Task Load(Func<Task> loadComplete)
    {
        if (await FileManager.IsExistFileAsync(ApplicationDataPath))
        {
            try
            {
                var json = await FileManager.ReadAllTextAsync(ApplicationDataPath);
                ApplicationData = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationData>(json);
            }
            catch
            {
                ApplicationData = new ApplicationData();
            }
        }
        else
            ApplicationData = new ApplicationData();
        await loadComplete();
    }

    public async Task Save()
    {
        try
        {
            await FileManager.WriteAllTextAsync(ApplicationDataPath, Newtonsoft.Json.JsonConvert.SerializeObject(ApplicationData));
        }
        catch
        {

        }
    }
}