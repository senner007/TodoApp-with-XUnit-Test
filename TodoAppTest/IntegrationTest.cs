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
        public async Task TestClient_InMemoryDb_GetAll()
        {
           using (var client = new TestClientProvider_TestDb().Client) 
           {
                  
                var response = await client.GetAsync("api/todo");

                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();

                var jsonTitle = JsonConvert.DeserializeObject<List<Todo>>(jsonResult).First().Name;

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("New Todo", jsonTitle);


            }
             
        }

        [Fact]
        public async Task TestClient_InMemoryDb_GetId1()
        {
            using (var client = new TestClientProvider_TestDb().Client)
            {

                var response = await client.GetAsync("api/todo/" + 1);


                response.EnsureSuccessStatusCode();

                string jsonResult = await response.Content.ReadAsStringAsync();

                var jsonTitle = JsonConvert.DeserializeObject<Todo>(jsonResult).Name;

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("New Todo", jsonTitle);

            }
        }

        [Fact]
        public async Task TestClient_ProductionDb_GetAll()
        {
           using (var client = new TestClientProvider_Production().Client) 
           {
                  
                var response = await client.GetAsync("api/todo");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
             
        }

    }

}