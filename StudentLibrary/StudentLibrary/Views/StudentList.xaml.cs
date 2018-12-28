using StudentLibrary.Models;
using StudentLibrary.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentList : ContentPage
    {
        StudentsViewModel studentsViewModel;
        public bool showFilter = true;
        public string action;

        public StudentList()
        {
            InitializeComponent();
            BindingContext = studentsViewModel = new StudentsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Student;
            if (item == null)
                return;

            await Navigation.PushAsync(new StudentDetailPage(item));

            // Manually deselect item.
            StudentsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new AddStudent());
        }

        async public void Groups_Clicked(object sender, EventArgs e)
        {
            SortLabel.Text = $"{Localization.Language.SortedBy}: {action}";
            this.action = "Group";
            studentsViewModel.LoadStudentsCommand.Execute(null);
            var buttons = (from student in studentsViewModel.Students
                               group student by student.Group into g
                               select g.Key).ToArray();
            var choice = await DisplayActionSheet(Localization.Language.SelectGroup, Localization.Language.AllStudents, "", buttons);
            if (choice == Localization.Language.AllStudents)
                choice = "";
            SearchBar_TextChanged(this, new TextChangedEventArgs("", choice));
        }

        public async void Sort_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet($"{Localization.Language.SortedBy}: ", Localization.Language.Cancel, null, Localization.Language.LastName, Localization.Language.FirstName, Localization.Language.Group);

            if (action != Localization.Language.Cancel)
            {
                studentsViewModel.ExecuteSortStudentsCommand(action);
            }

            SortLabel.Text = $"{Localization.Language.SortedBy}: {action}";
            this.action = action;
        }

        public void Search_Clicked(object sender, EventArgs e)
        {
            StudentSearch.IsVisible = !StudentSearch.IsVisible;
        }

        async public void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSettingsPage(studentsViewModel.Students.ToArray()));
            UserSettings.Current.Save();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (studentsViewModel.SearchText == null)
                studentsViewModel.SearchText = "";
            if (this.action == null)
                this.action = "Last Name";
            studentsViewModel.SearchStudentsCommand.Execute(action);
        }

        async private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  
            string searchText = e.NewTextValue;

            if (string.IsNullOrEmpty(searchText))
            {
                studentsViewModel.LoadStudentsCommand.Execute(action);
                
                //list.ItemsSource = tempdata;
            }

            else
            {
                if (action == null)
                {
                    action = "Last Name";
                }
                studentsViewModel.SearchText = searchText;
                studentsViewModel.SearchStudentsCommand.Execute(action);
                 //studentsViewModel.Students.Where(x => x.Title.Contains(searchText) || x.Author.Contains(searchText));

                //list.ItemsSource = tempdata.Where(x => x.Name.StartsWith(e.NewTextValue));
            }
        }

        private void picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}
