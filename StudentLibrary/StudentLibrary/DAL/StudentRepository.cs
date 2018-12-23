using StudentLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentLibrary.DAL
{
    public class StudentRepository
    {
        readonly SQLiteAsyncConnection database;

        public StudentRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Student>().Wait();
        }

        public Task<List<Student>> GetStudentsAsync()
        {
            return database.Table<Student>().ToListAsync();
        }

        public Task<List<Student>> GetStudentsById(int id)
        {
            return database.QueryAsync<Student>($"SELECT * FROM [Students] WHERE [Id] = {id}");
        }

        public Task<Student> GetStudentAsync(int id)
        {
            return database.Table<Student>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveStudentAsync(Student student)
        {
            if (student.Id != 0)
            {
                return database.UpdateAsync(student);
            }
            else
            {
                return database.InsertAsync(student);
            }
        }

        public async Task<List<Student>> SearchStudentsAsync(string action, string searchText)
        {
            switch (action)
            {
                case "First Name":
                case "Имя":
                    return await database.Table<Student>().Where(x => x.FirstName.ToLower().Contains(searchText.ToLower())
                    || x.FirstName.ToLower().Contains(searchText.ToLower())).ToListAsync();
                case "Group":
                case "Группа":
                    return await database.Table<Student>().Where(x => x.Group.ToLower().Contains(searchText.ToLower())
                    || x.Group.ToLower().Contains(searchText.ToLower())).ToListAsync();
                default:
                    return await database.Table<Student>().Where(x => x.LastName.ToLower().Contains(searchText.ToLower())
                    || x.LastName.ToLower().Contains(searchText.ToLower())).ToListAsync();
            }
            
        }

        public Task<int> DeleteStudentAsync(Student student)
        {
            return database.DeleteAsync(student);
        }

        internal Task SaveStudentBatchAsync(List<Student> students)
        {
            return database.InsertAllAsync(students);
        }
    }
}
