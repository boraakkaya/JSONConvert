using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestJSONConvertWebApp.Model;

namespace TestJSONConvertWebApp.Repositories
{
    public class CustomerRepo
    {
        public async Task<string> addCustomer(Customer customer)
        {
            HttpClient client = new HttpClient();            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string requestUrl = "https://localhost:44332/api/values";
            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("POST"), requestUrl);
            message.Content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(message);
            return response.Content.ReadAsStringAsync().Result;
        }
        public async Task<Customer> getCustomer(int ID)
        {
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  Should not set the header since the response will be text and converted to JObject
            string requestUrl = "https://localhost:44332/api/values/" + ID.ToString();
            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("GET"), requestUrl);            
            HttpResponseMessage response = await client.SendAsync(message);            
            Task<string> responseContent = response.Content.ReadAsStringAsync();
            string responseAsString = responseContent.Result.ToString();
            var jobj = JObject.Parse(responseAsString);
            JToken jsonFields = jobj["fields"];
            Customer customer = new Customer();
            customer.Title = jsonFields["Title"].ToString();
            customer.Lastname = jsonFields["Lastname"].ToString();
            customer.JSONData = JsonConvert.DeserializeObject<JSONData>(jsonFields["JSONData"].ToString());
            return customer;
        }
    }
}
