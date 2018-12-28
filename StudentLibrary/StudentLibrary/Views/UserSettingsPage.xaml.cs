using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentLibrary.Models;
using StudentLibrary.Services;
using StudentLibrary.ViewModels;

namespace StudentLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSettingsPage : ContentPage
    {
        Student[] students;

        public UserSettingsPage(Student[] students)
        {
            InitializeComponent();
            this.students = students;
            var viewModel = new UserSettingsViewModel();
            BindingContext = viewModel;
            LangPicker.ItemsSource = viewModel.Languages;
            LangPicker.SelectedIndex = viewModel.Languages.IndexOf(viewModel.Language);
            ThemePicker.ItemsSource = viewModel.Colors;
            ThemePicker.SelectedIndex = viewModel.Colors.IndexOf(viewModel.Theme);
        }

        async public void Statistics_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Statistics(students));
        }
    }
}