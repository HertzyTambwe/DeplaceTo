using System.IO;

class ReadingCSV
{
    static void Main()
    {
        Console.WriteLine(@"Entrer le chemin de la source des donnees. Qui dois terminer par un Slash (\).");
        string? source = Console.ReadLine();
        Console.WriteLine(@"Entrer le chemin la destination des donnees. Qui dois terminer par un Slash (\).");
        string? destination = Console.ReadLine();
        Console.WriteLine("Entrer le chemin du fichier csv");
        string? csv = Console.ReadLine();
        var data = GetFilesList(csv);
        int cunt = 0;
        foreach (var itemData in data)
        {
            foreach (var itemInDirectory in Directory.GetFiles(source, "*.pdf", SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new(itemInDirectory);
                DirectoryInfo directoryInfo = new(fileInfo.Directory.Name);
                if (fileInfo.Name.Contains(itemData.ToString()))
                {
                    cunt++;
                    if (Directory.Exists(directoryInfo.Name))
                    {
                        File.Copy(fileInfo.FullName, destination + directoryInfo.Name + @"\" + fileInfo.Name, true);
                    }
                    else if(!Directory.Exists(directoryInfo.Name))
                    {
                        Directory.CreateDirectory(destination + directoryInfo.Name);
                        File.Copy(fileInfo.FullName, destination + directoryInfo.Name + @"\" + fileInfo.Name, true);
                    }
                }
            }
            if (cunt == 0)
            {
                string fileName = destination + "debug.txt";
                using FileStream target = File.Open(fileName, FileMode.Append, FileAccess.Write);
                using StreamWriter writer = new(target);
                writer.WriteLine(itemData);
            }
            cunt = 0;
        }
    }
    static List<String> GetFilesList(string csvFile)
    {
        var reader = new StreamReader(File.OpenRead(csvFile));
        List<string> listItem = new();
        while (!reader.EndOfStream)
        {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            listItem.Add(reader.ReadLine());
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
        }
        return listItem;
    }
}