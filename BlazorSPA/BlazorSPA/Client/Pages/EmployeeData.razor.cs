using BlazorSPA.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorSPA.Client.Pages
{
    public class EmployeeDataModel : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public NavigationManager UrlNavigationManager { get; set; }

        [Parameter]
        public int ParamEmpID { get; set; }
        [Parameter]
        public string Action { get; set; }

        protected List<Employee> empList = new List<Employee>();
        protected Employee emp = new Employee();
        protected string Title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Action == "fetch")
            {
                await FetchEmployee();
                StateHasChanged();
            }
            else if (Action == "create")
            {
                Title = "Add Employee";
                emp = new Employee();
            }
            else if (ParamEmpID != 0)
            {
                if (Action == "edit")
                {
                    Title = "Edit Employee";
                }
                else if (Action == "delete")
                {
                    Title = "Delete Employee";
                }

                emp = await Http.GetJsonAsync<Employee>("/api/Employee/Details/" + ParamEmpID);
            }
        }

        protected async Task FetchEmployee()
        {
            Title = "Employee Data";
            empList = await Http.GetJsonAsync<List<Employee>>("api/Employee/Index");
        }

        protected async Task CreateEmployee()
        {
            if (emp.EmployeeId != 0)
            {
                await Http.SendJsonAsync(HttpMethod.Put, "api/Employee/Edit", emp);
            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/Employee/Create", emp);
            }
            UrlNavigationManager.NavigateTo("/employee/fetch");
        }

        protected async Task DeleteEmployee()
        {
            await Http.DeleteAsync("api/Employee/Delete/" + ParamEmpID);
            UrlNavigationManager.NavigateTo("/employee/fetch");
        }

        protected void Cancel()
        {
            Title = "Employee Data";
            UrlNavigationManager.NavigateTo("/employee/fetch");
        }
    }
}
