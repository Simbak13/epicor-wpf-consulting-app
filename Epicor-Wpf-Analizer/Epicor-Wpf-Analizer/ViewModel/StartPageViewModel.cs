

using Epicor_Wpf_Analizer.Interfaces;
using Epicor_Wpf_Analizer.Models;
using Epicor_Wpf_Analizer.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace Epicor_Wpf_Analizer.ViewModel
{
    public class StartPageViewModel : ViewModelBase
    {
        private IQueueServices service;

        private ObservableCollection<SupportCallOpen> queueList;
        public ObservableCollection<SupportCallOpen> QueueList
        {
            get { return queueList;  }
            set
            {
                queueList = value;
                RaisePropertyChanged(nameof(QueueList));
            }
        }

        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; RaisePropertyChanged(nameof(IsLoading)); }
        }

        private string totalRecords;
        public string TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; RaisePropertyChanged(nameof(TotalRecords)); }
        }

        private SupportCallOpen selectedCall;
        public SupportCallOpen SelectedCall
        {
            get { return selectedCall; }
            set
            {
                selectedCall = value;
                RaisePropertyChanged(nameof(SelectedCall));
            }
        }

        public ICommand SelectCommand { get; private set; }

        public StartPageViewModel()
        {
            TotalRecords = "Total Records : 0";
            service = new QueueServices();
            SelectCommand = new RelayCommand<SupportCallOpen>(SelectCallRow);
            Task.Run(async () =>
            {
              await OnStartLoadDataAsync();
            });

        }

         private async Task OnStartLoadDataAsync()
        {
            try
            {
              
                var supportList = await service.ListOpenQueuesAsync(null,100);
                if (supportList != null) 
                {

                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        TotalRecords = $"Total Records : {supportList.Count}";
                        QueueList = new ObservableCollection<SupportCallOpen>(supportList);
                    });
                }


            }catch(Exception ex)
            {
                Debug.WriteLine($"Execption {ex.Message}");
            }
        }

        private void SelectCallRow(SupportCallOpen call)
        {
             SelectedCall = call;

            MessageBox.Show(SelectedCall.SupportCallID);
        }
    }
}
