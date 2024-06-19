

using Epicor_Wpf_Analizer.Interfaces;
using Epicor_Wpf_Analizer.Models;
using Epicor_Wpf_Analizer.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

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



        public StartPageViewModel()
        {
            TotalRecords = "Total Records : 0";
           // QueueList = new ObservableCollection<SupportCallOpen>();
            service = new QueueServices();

            Task.Run(async () =>
            {
              await OnStartLoadDataAsync();
            });

        }

        private async Task OnStartLoadDataAsync()
        {
            try
            {
              
                var supportList = await service.ListOpenQueuesAsync();
                await Task.Delay(5000);
                if (supportList != null) 
                { 
                    TotalRecords=$"Total Records : {supportList.Count}" ;
                    QueueList = new ObservableCollection<SupportCallOpen>(supportList);
                    //await Application.Current.Dispatcher.InvokeAsync(() =>
                    //{


                    //    foreach (var item in supportList)
                    //    {
                    //        QueueList.Add(item);
                    //    }
                    //});
                }


            }catch(Exception ex)
            {
                Debug.WriteLine($"Execption {ex.Message}");
            }
        }
    }
}
