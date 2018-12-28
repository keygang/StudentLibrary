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
using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Series;

namespace StudentLibrary.Views
{
    
    public partial class Statistics : ContentPage
    {
        public Statistics(Student[] students)
        {
            InitializeComponent();

            BindingContext = new StatisticsViewModel(students);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var barViewModel = BindingContext as StatisticsViewModel;
            if (barViewModel != null) barViewModel.OnAppearing();
        }
    }
}