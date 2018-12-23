using StudentLibrary.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StudentLibrary.DAL
{
    public class GoogleStudentRepository
    {
        public async Task<StudentResult> GetStudentByISBNAsync(string isbn)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.googleapis.com/students/v1/volumes?q={isbn}+isbn&key=AIzaSyD6ZcI8kLpXhpMckhYWZoVsk0qjcM1PXWY";
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<StudentResult>(dataObjects);

                    return result;

                }

                // TODO: Handle error

                return null;
            }
        }
    }
}
