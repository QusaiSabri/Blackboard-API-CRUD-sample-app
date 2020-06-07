﻿using System;
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
    public class CourseService : IRestCourseService<Course>, IDisposable
    {
        HttpClient client;


        public CourseService(Token token)
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }


        public async Task<Course> CreateObject(Course newCourse)
        {
            Course course = new Course();
            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH);

            try
            {
                var json = JsonConvert.SerializeObject(newCourse);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    course = JsonConvert.DeserializeObject<Course>(content);
                    Debug.WriteLine(@"				Course successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return course;
        }


        public async Task<Course> ReadObject(string course_id)
        {
            Course courseResult = new Course();

            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + course_id);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    courseResult = JsonConvert.DeserializeObject<Course>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return courseResult;
        }


        public async Task<Course> UpdateObject(Course updateCourse)
        {
            Course courseResult = new Course();

            try
            {
                var json = JsonConvert.SerializeObject(updateCourse);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + updateCourse.courseId, body);


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Course successfully updated.");
                    var content = await response.Content.ReadAsStringAsync();
                    courseResult = JsonConvert.DeserializeObject<Course>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return courseResult;
        }

        public async Task<Course> DeleteObject(string course_id)
        {
            Course courseResult = new Course();
            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + course_id);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Course successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    courseResult = JsonConvert.DeserializeObject<Course>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return courseResult;
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

                client.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~CourseService()
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

