
using WebApiClientConsole;

Console.WriteLine("API CLIENT");
EmployeeAPIClient.DeleteEmployee(16).Wait();
Console.ReadLine();
