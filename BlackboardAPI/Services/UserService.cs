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
    public class UserService : IRestUserService<User>, IDisposable
    {
        HttpClient client;


        public UserService(Token token)
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }

        public async Task<User> CreateObject(User newUser)
        {
            User user = new User();
            var uri = new Uri(Constants.HOSTNAME + Constants.USER_PATH);

            try
            {
                var json = JsonConvert.SerializeObject(newUser);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                    Debug.WriteLine(@"				User successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return user;
        }

        public async Task<User> ReadObject(string user_id)
        {
            User userResult = new User();

            var uri = new Uri(Constants.HOSTNAME + Constants.USER_PATH + "externalId:" + user_id);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    userResult = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return userResult;
        }


        public async Task<User> UpdateObject(User updateUser)
        {
            User userResult = new User();

            try
            {
                var json = JsonConvert.SerializeObject(updateUser);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(Constants.HOSTNAME + Constants.USER_PATH + "externalId:" + updateUser.id, body); //updateUser.id ? updateUser.externalId?


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				User successfully updated.");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        userResult = JsonConvert.DeserializeObject<User>(content);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return userResult;
        }

        public async Task<User> DeleteObject(string user_id)
        {
            User userResult = new User();
            var uri = new Uri(Constants.HOSTNAME + Constants.USER_PATH + "externalId:" + user_id);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				User successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    userResult = JsonConvert.DeserializeObject<User>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return userResult;
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
        ~UserService()
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

