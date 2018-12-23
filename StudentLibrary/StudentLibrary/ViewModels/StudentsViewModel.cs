using StudentLibrary.Helpers;
using StudentLibrary.Models;
using StudentLibrary.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;

namespace StudentLibrary.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {

        private ObservableCollection<Student> _students;

        public string SearchText { get; set; }

        public ObservableCollection<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }

        public Command LoadStudentsCommand { get; set; }

        public Command SearchStudentsCommand { get; set; }

        public StudentsViewModel()
        {
            Students = new ObservableCollection<Student>();

            LoadStudentsCommand = new Command(async () => await ExecuteLoadStudentsCommand());
            SearchStudentsCommand = new Command(async (action) => await ExecuteSearchStudentsCommand(action.ToString()));

            MessagingCenter.Subscribe<AddStudent, Student>(this, "AddOrUpdateStudent", async (obj, student) =>
            {
                if (student != null)
                {

                    Student _student = student as Student;

                    await App.Database.SaveStudentAsync(_student);

                    Students.Add(_student);
                }
            });

        }

        private async Task ExecuteLoadStudentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Students.Clear();

                List<Student> students = App.Database.GetStudentsAsync().Result;

                //if (!students.Any())
                //{
                //    students = ReadSeedJson.GetSeedData();
                //    if (students.Any())
                //    {
                //        await App.Database.SaveStudentBatchAsync(students);
                //    }
                //}

                foreach (Student student in students)
                {
                    if (student != null)
                    {
                        Students.Add(student);
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
        }

        private async Task ExecuteSearchStudentsCommand(string action)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Students.Clear();


                List<Student> students = await App.Database.SearchStudentsAsync(action, SearchText);

                foreach (Student student in students)
                {
                    if (student != null)
                    {
                        Students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ExecuteSortStudentsCommand(string sortType)
        {
            List<Student> tempStudents;

            switch (sortType)
            {
                case "Group":
                case "Группа":
                    tempStudents = Students.OrderBy(x => x.Group).ToList();
                    break;
                case "First Name":
                case "Имя":
                    tempStudents = Students.OrderBy(x => x.FirstName).ToList();
                    break;
                default:
                    tempStudents = Students.OrderBy(x => x.LastName).ToList();
                    break;
            }

            Students.Clear();
            Students = new ObservableCollection<Student>(tempStudents);
        }
    }
}
