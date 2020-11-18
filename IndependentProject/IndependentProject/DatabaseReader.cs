using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using static IndependentProject.Models.Manifest;
using static IndependentProject.Models.DestinyInventoryItem;
using Windows.Storage;
using System.IO;
using Windows.Storage.Streams;
using System.IO.Compression;

namespace IndependentProject
{
    //This class was roughly built based off of the example listed at destinydevs.github.io/BungieNetPlatform/docs/Manifest
    class DatabaseReader
    {

        public static async Task<ManifestRootObject> GetManifest()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-Key", "b9b4222d5e7b4bdc8d36b6a48c350a43");

            string responseString = await httpClient.GetStringAsync("https://www.bungie.net/platform/Destiny2/Manifest/");

            ManifestRootObject manifest = JsonConvert.DeserializeObject<ManifestRootObject>(responseString);
            HttpResponseMessage dataFile = await httpClient.GetAsync("https://www.bungie.net/" + manifest.Response.mobileWorldContentPaths["en"]);
            using (Stream dataStream = await dataFile.Content.ReadAsStreamAsync())
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

                ZipArchive zipArchive = new ZipArchive(dataStream);

                if(zipArchive.Entries.Count != 1)
                {
                    throw new FormatException();
                }

                ZipArchiveEntry zipEntry = zipArchive.Entries[0];
                using (Stream streamEntry = zipEntry.Open())
                {
                    StorageFile unzippedFile = await storageFolder.CreateFileAsync("Manifest.content", CreationCollisionOption.ReplaceExisting);
                    using (Stream streamUnzippedFile = await unzippedFile.OpenStreamForWriteAsync())
                    {
                        await streamEntry.CopyToAsync(streamUnzippedFile);
                    }
                }
            }
            return manifest;
        }

        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=Manifest.content"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static Dictionary<string, Dictionary<string, string>> GetData()
        {
            Dictionary<string, Dictionary<string, string>> allData;
            Dictionary<string, string> hashDict = new Dictionary<string, string>()
                {
                    {"DestinyInventoryItemDefinition", "itemHash" }
                };

            using (SqliteConnection db =
                new SqliteConnection("Filename=Manifest.content"))
            {
                db.Open();

                allData = new Dictionary<string, Dictionary<string, string>>();
                foreach(string tableName in hashDict.Keys)
                {
                    SqliteCommand selectCommand = new SqliteCommand("SELECT json from " + tableName, db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        if (tableName.Equals("DestinyInventoryItemDefinition"))
                        {
                            if (!allData.Keys.Contains("DestinyInventoryItemDefinition"))
                            {
                                allData.Add("DestinyInventoryItemDefinition", new Dictionary<string, string>());
                            }
                            string json = query.GetString(0);
                            allData["DestinyInventoryItemDefinition"].Add(JsonConvert.DeserializeObject<DestinyInventoryItemRootObject>(json).hash+"", json); 
                        }
                    }
                }

                db.Close();
            }
            return allData;
        }
    }
}
