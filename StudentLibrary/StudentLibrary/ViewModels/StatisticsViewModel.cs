using StudentLibrary.Helpers;
using StudentLibrary.Models;
using StudentLibrary.Views;
using StudentLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace StudentLibrary.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        private PlotModel _plotModel;
        Student[] students;

        public PlotModel plotModel
        {
            get { return _plotModel; }
            set
            {
                _plotModel = value;
                RaisePropertyChanged("_plotModel");
            }
        }
       
        public StatisticsViewModel()
        {
            CreatePlot();
        }

        public StatisticsViewModel(Student[] students)
        {
            this.students = students;
            CreatePlot();
        }

        private void CreatePlot()
        {
            CategoryAxis xaxis = new CategoryAxis();
            xaxis.Position = AxisPosition.Left;
            xaxis.MajorGridlineStyle = LineStyle.Solid;
            xaxis.MinorGridlineStyle = LineStyle.Dot;
            LinearAxis yaxis = new LinearAxis();
            yaxis.Position = AxisPosition.Left;
            yaxis.MajorGridlineStyle = LineStyle.Dot;
            xaxis.MinorGridlineStyle = LineStyle.Dot;
            var groups = (from student in students group student by student.Group).ToArray();
            BarSeries barSeries = new BarSeries();
            foreach (var g in groups)
            {
                xaxis.Labels.Add(g.Key);
                barSeries.Items.Add(new BarItem(g.Count()));
            }
            plotModel = new PlotModel();
            plotModel.Title = Localization.Language.Group;
            plotModel.Background = OxyColors.Gray;

            plotModel.Axes.Add(xaxis);
            plotModel.Axes.Add(yaxis);
            plotModel.Series.Add(barSeries);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}