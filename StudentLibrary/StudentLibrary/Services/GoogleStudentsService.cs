using StudentLibrary.DAL;
using StudentLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentLibrary.Services
{
    public class GoogleStudentsService
    {
        public async Task<Student> GetStudentAsync(string isbn)
        {

            GoogleStudentRepository googleStudentRepository = new GoogleStudentRepository();

            StudentResult studentResult = await googleStudentRepository.GetStudentByISBNAsync(isbn);
            if (studentResult == null)
            {
                return null;
            }

            Item student = studentResult.Items.First();

            return new Student
            {
                FirstName = SetFirstName(student),

                LastName = SetLastName(student),

                Group = SetGroup(student),

                DateOfEntering = SetDateOfEntering(student)
            };
        }
        
        private string SetFirstName(Item student)
        {
            if (student.VolumeInfo == null && student.VolumeInfo.FirstName == null)
            {
                return null;
            }
            else
            {
                return student.VolumeInfo.FirstName;
            }
        }

        private string SetLastName(Item student)
        {
            if (student.VolumeInfo == null && student.VolumeInfo.LastName == null)
            {
                return null;
            }
            else
            {
                return student.VolumeInfo.LastName;
            }
        }

        private string SetGroup(Item student)
        {
            if (student.VolumeInfo == null && student.VolumeInfo.Group == null)
            {
                return null;
            }
            else
            {
                return string.Join(",", student.VolumeInfo.Group);
            }
        }

        private string SetDateOfEntering(Item student)
        {
            if (student.VolumeInfo == null && student.VolumeInfo.DateOfEntering == null)
            {
                return null;
            }
            else
            {
                return string.Join(",", student.VolumeInfo.DateOfEntering);
            }
        }
    }
}
