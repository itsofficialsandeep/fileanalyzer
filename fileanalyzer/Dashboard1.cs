using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace fileanalyzer
{
    // save the searched files
    public partial class Form3 : Form
    {

        // Function to save search data in the application directory
        public void SaveSearchData(Form3 data)
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(appDirectory, "search_data.xml");

                XmlSerializer serializer = new XmlSerializer(typeof(Form3));
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
                Console.WriteLine("Search data saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving search data: {ex.Message}");
            }
        }

        // Function to load search data from the application directory
        public Form3 LoadSearchData()
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(appDirectory, "search_data.xml");

                if (File.Exists(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Form3));
                    using (TextReader reader = new StreamReader(filePath))
                    {
                        return (Form3)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    Console.WriteLine("No search data found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading search data: {ex.Message}");
            }
            return new Form3(); // Return a new instance if no data is found or an error occurs
        }

        public void save()
        {
            // Saving data
            Form3 searchData = new Form3();
            searchData.SearchedFolders.Add("Folder1");
            searchData.SearchedFiles.Add("File1.txt");
            searchData.TotalItems = 10;

            SaveSearchData(searchData);

            // Loading data
            Form3 loadedData = LoadSearchData();
            Console.WriteLine($"Total items loaded: {loadedData.TotalItems}");
        }

    }
}
