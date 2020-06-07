using System.Threading.Tasks;

namespace BBDNRESTDemoCSharp
{
	public interface IRestService<TRestModel> 
	{
		Task<TRestModel> CreateObject (TRestModel T);

		Task<TRestModel> ReadObject ();

		Task<TRestModel> UpdateObject (TRestModel T);

		Task<TRestModel> DeleteObject ();
	}
}

