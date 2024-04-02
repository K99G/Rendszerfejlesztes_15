using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

public class JsonDataService<T> where T : class // Make sure the class is generic
{
    private readonly IWebHostEnvironment _env;


    private string JsonFileName => Path.Combine(_env.ContentRootPath, "source", $"{typeof(T).Name.ToLower()}.json");

        public JsonDataService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<T> GetAll()
        {
        try
            {
                if (!File.Exists(JsonFileName))
                {
                    return new List<T>();
                }

                using var jsonFileReader = File.OpenText(JsonFileName);
                return JsonSerializer.Deserialize<List<T>>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<T>();
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<T>(); 
            }
    }

        public void Save(List<T> items)
        {
            try
            {
                using var outputStream = File.Create(JsonFileName);
                JsonSerializer.Serialize(new Utf8JsonWriter(outputStream, new JsonWriterOptions { Indented = true }), items);
            }
            catch (Exception ex)
            {
      
                Console.WriteLine($"An error occurred while saving: {ex.Message}");
            }
        
    }
}