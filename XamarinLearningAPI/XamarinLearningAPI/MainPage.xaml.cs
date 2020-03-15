using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinLearningAPI.Views;

namespace XamarinLearningAPI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainViewModel VM => ((MainViewModel)BindingContext);
        public MainPage()
        {
            InitializeComponent();
        }
        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    var button = (Button)sender;
        //    await Navigation.PushAsync(new TodaySchedule());
        //}   
    }
}
