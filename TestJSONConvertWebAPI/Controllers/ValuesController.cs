using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestJSONConvertWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static string graphToken = "";
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", graphToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string requestUrl = "https://graph.microsoft.com/beta/sites/root/lists/9c942ba0-52d0-4df0-9e06-e02f7579961a/items/" + id.ToString();
            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("GET"), requestUrl);            
            HttpResponseMessage response = await client.SendAsync(message);
            return response.Content.ReadAsStringAsync().Result;
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Post([FromBody] Customer customer)
        {
            //return customer.JSONData.Comments;
            fieldProp _fieldProp = new fieldProp();
            

            dataFields flds = new dataFields();
            flds.Title = customer.Title;
            flds.Lastname = customer.Lastname;
            flds.JSONData = JsonConvert.SerializeObject(customer.JSONData);
            _fieldProp.fields = flds;
                        
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", graphToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string requestUrl = "https://graph.microsoft.com/beta/sites/root/lists/9c942ba0-52d0-4df0-9e06-e02f7579961a/items";
            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("POST"), requestUrl);
            message.Content = new StringContent(JsonConvert.SerializeObject(_fieldProp), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(message);
            return response.Content.ReadAsStringAsync().Result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    public class fieldProp
    {
        public dataFields fields { get; set; }
    }

    public class dataFields
    {
        public string Title { get; set; }
        public string Lastname { get; set; }
        public string JSONData { get; set; }
    }


        public class Customer
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Lastname { get; set; }
            public JSONData JSONData { get; set; }
        }
        public class JSONData
        {
            public int Age { get; set; }
            public string JobTitle { get; set; }
            public string Comments { get; set; }
            public SubJSONData SubJSONData { get; set; }
        }
        public class SubJSONData
        {
            public string FavoriteColor { get; set; }
            public string FavoriteNUmber { get; set; }
        }
}
