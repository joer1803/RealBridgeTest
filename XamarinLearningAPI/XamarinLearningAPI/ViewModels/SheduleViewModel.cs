using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace XamarinLearningAPI.ViewModels
{
    public class SheduleViewModel : INotifyPropertyChanged
    {
        ApiService apiService;
        List<RadioEpisode> radioEpisodes;
        bool _IsBusy = false;
        public SheduleViewModel(string channel)
        {
            apiService = new ApiService();
            LoadSchedule(channel);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public List<RadioEpisode> RadioEpisodes
        {
            get { return radioEpisodes; }
            set
            {
                radioEpisodes = value;
                OnPropertyChanged(nameof(RadioEpisodes));
            }
        }

        public void LoadSchedule(string channel = "")
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                RadioEpisodes = await apiService.GetRadioSchedule(channel, "");
            });
            IsBusy = false;

        }

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
    }
}
