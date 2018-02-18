using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;

namespace HashHunters.Autotrader.Services
{
    public static class RESTHelper
    {
        public static T Get<T, TParam>(this HttpClient client, string url, TParam parameters)
            where T : class
            where TParam : class
        {
            var paramsStr = "";
            if (parameters != null)
            {
                var parametersArray = parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Select(p => $"{p.Name}={p.GetValue(parameters, null)}").ToArray();

                if (parametersArray.Any())
                    paramsStr = "?" + string.Join("&", parametersArray);
            }

            var respTask = client.GetAsync($"{url}{paramsStr}");
            respTask.Wait();

            var h = respTask.Result.Headers;
            var task = respTask.Result.Content.ReadAsStringAsync();
            task.Wait();
            try
            {
                var res = JsonConvert.DeserializeObject<T>(task.Result);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(task.Result, e);
            }
        }
    }
}
