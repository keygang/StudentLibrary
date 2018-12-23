using StudentLibrary.Models;
using StudentLibrary.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace StudentLibrary.ViewModels
{
    public class StudentDetailViewModel : BaseViewModel
    {
        Student student;
        public Student Student
        {
            get { return student; }
            set {
                student = value;
                RaisePropertyChanged("Student");
            }
        }

        public StudentDetailViewModel(Student student = null)
        {
            Student = student;

            MessagingCenter.Subscribe<AddStudent, Student>(this, "AddOrUpdateStudent", (obj, studentUpdate) =>
            {
                if (studentUpdate != null)
                {
                    Student = studentUpdate as Student;
                }
            });
        }
    }
}
