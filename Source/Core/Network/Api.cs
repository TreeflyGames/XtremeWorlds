using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Core.Network
{
    public class Api
    {
        private const string apiKey = "-5xPMTEPy3151vdLJKzaE04dGTnOu7vu";
        private const string apiUrl = "https://giamon.com/forums/index.php/api/auth";

        public static object Auth(string username, string password)
        {
            using (var client = new HttpClient())
            {

                var @params = new Dictionary<string, string>() { { "login", username }, { "password", password } };

                client.DefaultRequestHeaders.Add("XF-Api-Key", apiKey);

                var result = client.PostAsync(apiUrl, new FormUrlEncodedContent(@params)).Result;
                if (result.StatusCode == HttpStatusCode.OK) // HttpStatusCode.OK corresponds to HTTP status 200
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

    }
}