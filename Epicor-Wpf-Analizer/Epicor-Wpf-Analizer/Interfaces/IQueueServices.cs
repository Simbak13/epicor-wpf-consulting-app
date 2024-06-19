

using Epicor_Wpf_Analizer.Helpers;
using Epicor_Wpf_Analizer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epicor_Wpf_Analizer.Interfaces
{
    public interface IQueueServices
    {
        Task<List<SupportCallOpen>>
            ListOpenQueuesAsync(FiltersParams filters = null, int rowsNumber = 50);
    }
}
