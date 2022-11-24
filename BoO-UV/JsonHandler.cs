using System.Text.Json;

namespace BoO_UV;

public class JsonHandler
{
    public string configfilename { get; set; } = "config.json";
    public string directoryname { get; set; } = "BoO-UV";
    //public string directorypath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public string directorypath { get; set; } = FileSystem.Current.AppDataDirectory;
    public string objectdirectoryname { get; set; } = "objects";
    public string libdirectoryname { get; set; } = "lib";
    public string characterdirectoryname { get; set; } = "characters";

    #region Object
    public void WriteObject(Object currobject)
    {
        string path = Path.Combine(new string[] { directorypath, directoryname, libdirectoryname, objectdirectoryname, currobject.name });
        CreateJsonFile(currobject, path + ".json");
    }
    public void DeleteObject(in Object currobject)
    {
        string path = Path.Combine(new string[] { directorypath, directoryname, libdirectoryname, objectdirectoryname, currobject.name });
        DeleteFile(path + ".json");
    }
    public Object GetObject(in Object currobject)
    {
        string path = Path.Combine(new string[] { directorypath, directoryname, libdirectoryname, objectdirectoryname, currobject.name });
        return ReadObject(path + ".json");
    }
    #endregion
    #region Character
    public void WriteCharacter(Character currchar)
    {
        string path = Path.Combine(new string[] { directorypath, directoryname, libdirectoryname, characterdirectoryname, currchar.name });
        CreateJsonFile(currchar, path + ".json");
    }
    public void DeleteCharacter(in Character currchar)
    {
        string path = Path.Combine(new string[] { directorypath, directoryname, libdirectoryname, characterdirectoryname, currchar.name });
        DeleteFile(path + ".json");
    }
    public Character GetCharacter(in Character currchar)
    {
        string path = Path.Combine(new string[] { directorypath, directoryname, libdirectoryname, characterdirectoryname, currchar.name });
        return ReadCharacter(path + ".json");
    }
    #endregion

    private async Task DeleteFile(string path)
    {
        File.Delete(path);
    }

    private async Task DeleteDirectory(string path)
    {
        Directory.Delete(path, true);
    }
    private async Task CreateJsonFile(object jsonObject, string path)
    {
        string directorypath = path.Substring(0, path.LastIndexOf('\\'));
        Directory.CreateDirectory(directorypath);
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault };
        using FileStream createStream = File.Create(path);
        await JsonSerializer.SerializeAsync(createStream, jsonObject, jsonOptions);
        await createStream.DisposeAsync();
    }

    #region Reading
    public Object ReadObject(string path)
    {
        if (File.Exists(path))
        {
            using FileStream openStream = File.OpenRead(path);
            Object currobject = JsonSerializer.Deserialize<Object>(openStream);
            openStream.Dispose();
            return currobject;
        }
        return null;
    }
    public Character ReadCharacter(string path)
    {
        if (File.Exists(path))
        {
            using FileStream openStream = File.OpenRead(path);
            Character currchar = JsonSerializer.Deserialize<Character>(openStream);
            openStream.Dispose();
            return currchar;
        }
        return null;
    }
    /*
    private Setting ReadSettings(string path)
    {
        if (File.Exists(path))
        {
            using FileStream openStream = File.OpenRead(path);
            Setting setting = JsonSerializer.Deserialize<Setting>(openStream);
            openStream.Dispose();
            return setting;
        }
        return null;
    }*/
    #endregion
}
