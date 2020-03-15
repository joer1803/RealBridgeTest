using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinLearningAPI.Models;
using XamarinLearningAPI.ViewModels;
using XamarinLearningAPI.Views;

namespace XamarinLearningAPI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //string name = string.Empty;
        ApiService apiService;
        List<RadioChannel> radioChannels;
        bool _IsBusy = false;
        public MainViewModel()
        {
            apiService = new ApiService();
            LoadChannels();
            SelectChannel = new Command<string>(async (id) => 
            {
                await NavigateToSelectedChannel(id);
            });
        }
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        public void LoadChannels()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                RadioChannels = await apiService.GetRadioChannels();
            });
            IsBusy = false;

        }
        
        public List<RadioChannel> RadioChannels
        {
            get { return radioChannels; }
            set
            {
                radioChannels = value;
                OnPropertyChanged(nameof(RadioChannels));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand SelectChannel { get; private set; }

        async Task NavigateToSelectedChannel(string chId)
        {
            var schedVM = new SheduleViewModel(chId);
            var schedPage = new TodaySchedule();
            schedPage.BindingContext = schedVM;
            await Application.Current.MainPage.Navigation.PushAsync(schedPage);
        }

    }
}
