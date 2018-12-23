using StudentLibrary.Models;
using StudentLibrary.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentLibrary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentDetailPage : ContentPage
	{
        StudentDetailViewModel _student;
        //StudentDetailViewModel viewModel;

        public StudentDetailPage (Student student)
		{
			InitializeComponent ();
            this._student = new StudentDetailViewModel(student);

            BindingContext = this._student;
        }

        async void EditItem_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddStudent(this._student.Student));
        }

        async Task DeleteItem_Clicked(object sender, EventArgs args)
        {
            await App.Database.DeleteStudentAsync(this._student.Student);
            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            image.RotationY = 180;
            image.RotateYTo(0, 1500, Easing.BounceOut);
            base.OnAppearing();
        }

    }
}