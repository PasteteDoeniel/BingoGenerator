using System.IO;
using System.IO.Compression;
using System.Xml;

namespace BingoGenerator
{
    public sealed class SaveDocument : XmlDocument
    {
        public bool SaveFileContaintEntry(string entryName)
        {
            foreach (XmlNode item in ChildNodes)
            {
                if (item.Name == entryName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetEntry(string entryName, ref XmlNode entry) 
        {
            if (SaveFileContaintEntry(entryName))
            {
                foreach (XmlNode item in ChildNodes)
                {
                    if (item.Name == entryName)
                    {
                        entry = item;
                        return true;
                    }
                }
            }
            return false;
        }
    }

    class SaveManager
    {
        private static List<ISaveInterface> SaveableInstances = new List<ISaveInterface>();

        private static SaveManager? SaveManagerInstance;
        public static SaveManager Get()
        {
            if (SaveManagerInstance == null)
            {
                SaveManagerInstance = new SaveManager();
            }
            return SaveManagerInstance;
        }

        public void Register(ISaveInterface saveable)
        {
            SaveableInstances.Add(saveable);
        }

        public void Save()
        {
            SaveDocument SaveDoc = new SaveDocument();

            foreach (var Instance in SaveableInstances)
            {
                if (Instance is ISaveInterface SaveInterface)
                {
                    SaveInterface.Save(ref SaveDoc);
                }
            }
            MemoryStream MStream = new MemoryStream();
            SaveDoc.Save(MStream);
            byte[] Bytes = MStream.ToArray();

            using (var CompressedStream = new MemoryStream())
            {
                using (var CompressionStream = new DeflateStream(CompressedStream, CompressionMode.Compress))
                {
                    CompressionStream.Write(Bytes, 0, Bytes.Length);
                }

                File.WriteAllBytes(GetSaveFilePath(), CompressedStream.ToArray());
            }
        }

        public void Load() 
        {
            SaveDocument LoadedDoc = new SaveDocument();

            if (File.Exists(GetSaveFilePath()))
            {

                byte[] CompressedData = File.ReadAllBytes(GetSaveFilePath());

                using (var CompressedStream = new MemoryStream(CompressedData))
                using (var DecompressionStream = new DeflateStream(CompressedStream, CompressionMode.Decompress))
                using (var MemoryStream = new MemoryStream())
                {
                    DecompressionStream.CopyTo(MemoryStream);
                    byte[] Bytes = MemoryStream.ToArray();
                    using (MemoryStream ms = new MemoryStream(Bytes))
                    {
                        LoadedDoc.Load(ms);
                    }
                }
            }

            foreach (var Instance in SaveableInstances)
            {
                if (Instance is ISaveInterface SaveInterface)
                {
                    SaveInterface.Load(LoadedDoc);
                }
            }
        }

        private string GetSaveFilePath()
        {
            return Path.Combine(Constants.GetApplicationPath(), Constants.SaveFileName);
        }
    }
}
