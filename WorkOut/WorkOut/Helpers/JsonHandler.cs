using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WorkOut.Models;

namespace WorkOut.Helpers
{
    public static class JsonHandler
    {
        // Ścieżka pliku JSON w katalogu bin/Debug/Data
        private static string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");

        public static List<User> LoadUsers()
        {
            // Upewnij się, że katalog i plik istnieją
            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory); // Tworzy katalog, jeśli nie istnieje
            }

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]"); // Tworzy pusty plik JSON
            }

            // Odczytaj dane z pliku JSON
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                // Obsłuż potencjalne błędy odczytu
                return new List<User>();
            }
        }

        public static void SaveUsers(List<User> users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch
            {
                // Obsłuż potencjalne błędy zapisu
            }
        }
    }
}