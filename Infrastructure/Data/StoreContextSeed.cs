using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using System.Linq;

namespace Infrastructure.Data
{
	public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.Authors.Any())
                {
                    var authorData =
                        File.ReadAllText(path + @"/Data/SeedData/author.json");
                    var list = JsonSerializer.Deserialize<List<Author>>(authorData);
                    foreach (var item in list)
                    {
                        context.Authors.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}


