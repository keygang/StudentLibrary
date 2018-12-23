using StudentLibrary.DAL;
using StudentLibrary.Interfaces;

using Xamarin.Forms;

namespace StudentLibrary
{
	public partial class App : Application
	{
        static StudentRepository studentRepository;

		public App ()
		{
            InitializeComponent();
            UserSettings.Load();
            UserSettings.Current.Language = UserSettings.Current.Language;
            MainPage = new NavigationPage(new StudentLibrary.Views.StudentList());//.MainTabPage();
            UserSettings.Current.Theme = UserSettings.Current.Theme;
        }

        public static StudentRepository Database
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new StudentRepository(DependencyService.Get<IFileHelper>().GetLocalFilePath("StudentSQLite.db3"));
                }
                return studentRepository;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
