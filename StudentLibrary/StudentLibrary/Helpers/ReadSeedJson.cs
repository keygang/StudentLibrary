
using StudentLibrary.Models;
using StudentLibrary.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace StudentLibrary.Helpers
{
    public class ReadSeedJson
    {
        #region How to load an Json file embedded resource
        public static List<Student> GetSeedData()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(StudentsViewModel)).Assembly;

            Stream stream = assembly.GetManifestResourceStream("StudentLibrary.DAL.SeedData.json");

            List<Student> students;

            string json;
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(json);
            }

            return students;
        }
        #endregion

    }
}
