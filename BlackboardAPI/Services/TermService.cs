using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BlackboardAPI.Services.Interfaces;

namespace BlackboardAPI
{
    public class TermService : IRestTermService<Term>, IDisposable
    {
        HttpClient client;


        public TermService(Token token)
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }


        public async Task<Term> CreateObject(Term newTerm)
        {
            Term termResult = new Term();
            var uri = new Uri(Constants.HOSTNAME + Constants.TERM_PATH);

            try
            {
                var json = JsonConvert.SerializeObject(newTerm);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    termResult = JsonConvert.DeserializeObject<Term>(content);
                    Debug.WriteLine(@" Term successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@" ERROR {0}", ex.Message);
            }

            return termResult;
        }


        public async Task<Term> ReadObject(string term_id)
        {
            Term termResult = new Term();

            var uri = new Uri(Constants.HOSTNAME + Constants.TERM_PATH + "externalId:" + term_id);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    termResult = JsonConvert.DeserializeObject<Term>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return termResult;
        }


        public async Task<Term> UpdateObject(Term updateTerm)
        {
            Term termResult = new Term();

            try
            {
                var json = JsonConvert.SerializeObject(updateTerm);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(Constants.HOSTNAME + Constants.TERM_PATH + "externalId:" + updateTerm.id, body); //updateTerm.id or updateTerm.externalId               
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Term successfully updated.");
                    var content = await response.Content.ReadAsStringAsync();
                    termResult = JsonConvert.DeserializeObject<Term>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return termResult;
        }

        public async Task<Term> DeleteObject(string term_id)
        {
            Term termResult = new Term();
            var uri = new Uri(Constants.HOSTNAME + Constants.TERM_PATH + "externalId:" + term_id);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Term successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    termResult = JsonConvert.DeserializeObject<Term>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return termResult;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                client.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~TermService()
        {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

