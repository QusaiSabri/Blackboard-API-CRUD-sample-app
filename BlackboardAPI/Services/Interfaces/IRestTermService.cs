using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardAPI.Services.Interfaces
{
    interface IRestTermService<TRestModel>
    {
        Task<TRestModel> CreateObject(TRestModel T);

        Task<TRestModel> ReadObject(string TermID);

        Task<TRestModel> UpdateObject(TRestModel T);

        Task<TRestModel> DeleteObject(string TermID);
    }
}
