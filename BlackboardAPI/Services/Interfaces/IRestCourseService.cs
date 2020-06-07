using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardAPI.Services
{
    interface IRestCourseService<TRestModel>
    {
        Task<TRestModel> CreateObject(TRestModel T);

        Task<TRestModel> ReadObject(string CourseID);

        Task<TRestModel> UpdateObject(TRestModel T);

        Task<TRestModel> DeleteObject(string CourseID);
    }
}
