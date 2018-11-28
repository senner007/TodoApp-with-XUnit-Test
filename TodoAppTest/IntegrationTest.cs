using Newtonsoft.Json;
using TodoApp;
using TodoAppTest;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace TodoAppTest
{
    public class TestIntegration 
    {

        [Fact]
        public async Task TestClient_GetAll()
        {
           using (var client = new TestClientProvider().Client) 
           {
                  
                var response = await client.GetAsync("api/todo");

                var rc = await response.Content.ReadAsStringAsync(); 

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();

                var jsonTitle = JsonConvert.DeserializeObject<List<Todo>>(jsonResult).First().Name;

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("New Todo", jsonTitle);


            }
             
        }

        [Fact]
        public async Task TestClient_GetId1()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("api/todo/" + 1);

                var rc = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();

                var jsonTitle = JsonConvert.DeserializeObject<Todo>(jsonResult).Name;

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("New Todo", jsonTitle);

            }
        }

    }

}