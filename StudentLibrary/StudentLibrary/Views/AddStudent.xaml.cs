using StudentLibrary.Models;
using StudentLibrary.Services;
using StudentLibrary.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace StudentLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStudent : ContentPage
    {
        public StudentDetailViewModel _student;

        public AddStudent(Student student = null)
        {
            InitializeComponent();
            Student editingStudent = student == null ? new Student() { Photo = "icon.png" } : student;
            group.Keyboard = Keyboard.Numeric;
            dateOfEntring.Keyboard = Keyboard.Numeric;
            this._student = new StudentDetailViewModel(editingStudent);

            BindingContext = this._student;
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                this._student.Student.Photo = photo.Path;
                this._student.RaisePropertyChanged("Student");
            }
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddOrUpdateStudent", this._student.Student);
            await Navigation.PopAsync();
        }

        async void OnScan_Clicked(object sender, EventArgs eventArgs)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    Console.WriteLine("Scanned Barcode: " + result.Text);

                    var service = new GoogleStudentsService();
                    this._student.Student = await service.GetStudentAsync(result.Text);
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Occured", "An Error occured while scanning. Try again later.", "Ok");
                Log.Warning("Scanner", ex.Message);
            }

        }

        protected override void OnAppearing()
        {
            image.Opacity = 0;
            image.FadeTo(1, 2000, Easing.Linear);
            base.OnAppearing();
        }

    }
}