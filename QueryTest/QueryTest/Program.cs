using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

public sealed class CustomSection : ConfigurationSection
{
    public CustomSection()
    {
        
    }
    
    [ConfigurationProperty("name",)]
}

class UsingConfigurationClass
{

    public class ApplicationMain
    {
        static void CreateConfigurationFile()
        {
            try
            {
                CustomSection customSection = new CustomSection();
            }
        }

        public static void UserMenu()
        {
            string applicationName = Environment.GetCommandLineArgs()[0] + ".exe";

            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine("Application: " + applicationName);
            buffer.AppendLine("Make your Selection");
            buffer.AppendLine("?    -- Display help.");
            buffer.AppendLine("Q,q  -- Exit the application.");

            buffer.Append("1    -- Instantiate the");
            buffer.AppendLine(" Configuration class.");

            buffer.Append("2    -- Use GetSection(string) to read ");
            buffer.AppendLine(" a custom section.");

            buffer.Append("3    -- Use SaveAs methods");
            buffer.AppendLine(" to save the configuration file.");

            buffer.Append("4    -- Use AppSettings property to read");
            buffer.AppendLine(" the appSettings section.");
            buffer.Append("5    -- Use ConnectionStrings property to read");
            buffer.AppendLine(" the connectionStrings section.");

            buffer.Append("6    -- Use Configuration class properties");
            buffer.AppendLine(" to obtain configuration information.");

            Console.Write(buffer.ToString());

        }

        static void Main(string[] args)
        {
            string selection;

            string appName = Environment.GetCommandLineArgs()[0];

            while (true)
            {
                UserMenu();
                Console.Write("> ");
                selection = Console.ReadLine();
                if (!string.IsNullOrEmpty(selection))
                    break;
            }

            while (selection.ToLower() != "q")
            {
                switch (selection)
                {
                    case "1":
                        CreateConfigurationFile();
                        break;
                }
            }
        }

    }
}


/*
            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Index> familyQuery = client.CreateDocumentQuery<Index>(
                UriFactory.CreateDocumentCollectionUri("index_tag_admin_db", "indexTagAdminItems"));
            foreach (var index in familyQuery)
            {
                Console.WriteLine("\tRead {0}", index.Id + " " + index.Name);
            }

            Console.Read();
                [CosmosCollection("indexTagAdminItems")]
    public class Index : ISharedCosmosEntity {
        public string CosmosEntityName { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Location { get; set; }

        public bool Enabled { get; set; }
    }
*/