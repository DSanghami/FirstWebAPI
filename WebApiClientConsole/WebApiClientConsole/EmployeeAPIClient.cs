using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace WebApiClientConsole
{
    internal class EmployeeAPIClient
    {
        static Uri uri = new Uri("http://localhost:5238/");
        public static async Task CallGetAllEmployee() //async opertaion means the process is independent
                                                      //if client requesting server through api the client must be set free. the task can be put in queue
                                                      //once it is completed then it can be called back and delivery

        {
            using (var client = new HttpClient()) //httpclient is dotnet library to establish async
            {
                client.BaseAddress = uri;
                //Httpget
                HttpResponseMessage response = await client.GetAsync("GetAllEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String x = await response.Content.ReadAsStringAsync(); //this return only string but i want list so next is about list
                    await Console.Out.WriteLineAsync(x);
                }
            }
        }
        //response type have to be json. then do serialization. fr that we need library install Newtonsoft.Json
        public static async Task CallGetAllEmployeeList()
        {
            using (var client = new HttpClient()) //httpclient is dotnet library to establish async
            {
                client.BaseAddress = uri;
                List<Employee> employees = new List<Employee>();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Httpget:
                HttpResponseMessage response = await client.GetAsync("GetAllEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync(); //this return only string but i want list so next is about list
                                                                              //install Newtonsoft.jsn using packagemanager
                    employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                    foreach (Employee emp in employees)
                    {
                        await Console.Out.WriteLineAsync($"{emp.EmpID},{emp.FirstName},{emp.LastName},{emp.Title},{emp.BirthDate},{emp.HireDate} ");
                    }
                }
            }
        }
        public static async Task AddnewEmployee()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Employee employee = new Employee()
                {
                    FirstName = "William",
                    LastName = "John",
                    City = "Nyc",
                    BirthDate = new DateTime(1980, 01, 01),
                    HireDate = new DateTime(1998, 11, 23),
                    Title = "Manager"
                };
                var myContent = JsonConvert.SerializeObject(employee);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("AddEmployee", byteContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                }

            }
        }
        public static async Task UpdateEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Employee emp = new Employee();
                emp.EmpID = 16;
                emp.FirstName = "Jeon";
                emp.LastName = "Jungkook";
                emp.City = "Uk";
                emp.BirthDate = new DateTime(1992, 01, 01);
                emp.HireDate = new DateTime(2018, 04, 01);
                emp.Title = "Manager";
                var myContent = JsonConvert.SerializeObject(emp);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //httpPUT
                HttpResponseMessage response = await client.PutAsync("EditEmployee", byteContent);
                response.EnsureSuccessStatusCode();





            }
        }
        public static async Task DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //httpdelete
                HttpResponseMessage response = await client.DeleteAsync($"DeleteEmployee?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Error deleting employee. Status code: {response.StatusCode}");
                }
            }






        }
    }
}
