using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;

namespace HashHunters.Autotrader
{
    public interface IREST
    {
        void Init(string baseAddress);
        T Get<T, TParam>(string url, TParam parameters = default(TParam)) 
            where T : class 
            where TParam : class;
    }

    public class REST : IREST
    {
        public HttpClient Client { get; private set; }

        public void Init(string baseAddress)
        {
            Client = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public T Get<T, TParam>(string url, TParam parameters)
            where T : class
            where TParam : class
        {
            if (Client == null)
                throw new Exception("Rest is not initialized!");

            var paramsStr = "";
            if (parameters != null)
            {
                var parametersArray = parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Select(p => $"{p.Name}={p.GetValue(parameters, null)}").ToArray();

                if (parametersArray.Any())
                    paramsStr = "?" + string.Join("&", parametersArray);
            }

            var respTask = Client.GetAsync($"{url}{paramsStr}");
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
