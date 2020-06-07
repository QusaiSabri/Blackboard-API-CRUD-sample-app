using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BlackboardAPI.Services;

namespace BlackboardAPI
{
    public class MembershipService : IRestMembershipService<Membership>, IDisposable
    {
        HttpClient client;


        public MembershipService(Token token)
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }


        public async Task<Membership> CreateObject(Membership newMembership)
        {
            Membership membershipResult = new Membership();
            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + newMembership.courseId + "users/externalId:" + newMembership.userId);

            try
            {
                var json = JsonConvert.SerializeObject(membershipResult);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    membershipResult = JsonConvert.DeserializeObject<Membership>(content);
                    Debug.WriteLine(@"				Membership successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return membershipResult;
        }


        public async Task<Membership> ReadObject(string course_id, string user_id)
        {
            Membership membershipResult = new Membership();

            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + course_id + "users/externalId:" + user_id);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    membershipResult = JsonConvert.DeserializeObject<Membership>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return membershipResult;
        }


        public async Task<Membership> UpdateObject(Membership updateMembership)
        {
            Membership membershipResult = new Membership();

            try
            {
                var json = JsonConvert.SerializeObject(updateMembership);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + updateMembership.courseId + "users/externalId:" + updateMembership.userId, body);


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Membership successfully updated.");
                    var content = await response.Content.ReadAsStringAsync();
                    membershipResult = JsonConvert.DeserializeObject<Membership>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return membershipResult;
        }

        public async Task<Membership> DeleteObject(string course_id, string user_id)
        {
            Membership membershipResult = new Membership();
            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + course_id + "users/externalId:" + user_id);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Membership successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    membershipResult = JsonConvert.DeserializeObject<Membership>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return membershipResult;
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
        ~MembershipService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
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

