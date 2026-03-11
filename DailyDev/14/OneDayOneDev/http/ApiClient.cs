using Microsoft.VisualBasic;
using OnedayOneDev_Shared;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Request;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace OneDayOneDev.http
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string BaseUrl)
        {
            var authHandler = new AuthHandler
            {
                InnerHandler = new HttpClientHandler()
            };

            _httpClient = new HttpClient(authHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            
        }

        private void EnsureAuthentification()
        {
            if (!AuthSession.IsAuthenticated) 
            {
                throw new UnauthorizedAccessException("Session expirée");
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("Username", username),
                new KeyValuePair<string,string>("Password", password),
            });

            var response = await _httpClient.PostAsync("api/auth/login", form);

            if (!response.IsSuccessStatusCode)
                return false;

            var login = await response.Content.ReadFromJsonAsync<AuthLoginResponse>();

            if (login is null || string.IsNullOrWhiteSpace(login.access_token))
                return false;

            AuthSession.SetToken(login.access_token, login.expires_in);



            return true;
        }

        public async Task<PageResult<TaskItem>> GetTaskList()
        {
            EnsureAuthentification();
            var response = await _httpClient.GetAsync("api/Tasks/GetAllTask");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PageResult<TaskItem>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new PageResult<TaskItem>() ;


        }

        
        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            EnsureAuthentification();
            var response = await _httpClient.GetAsync($"api/Tasks/GetTaskById?identifiant={id}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TaskItem>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new TaskItem();


        }
        public async Task<TaskItem?> CreateATaskAsync(string Title,string DueDate,TaskPriority priority)
        {
            EnsureAuthentification();
            var task = new TaskItem();

            task.Title = Title;
            task.DueDate = IDateTimeProvider.ParseDate(DueDate);
            task.Priority = priority;

            var json = JsonSerializer.Serialize(task);

            var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PostAsync("api/Tasks/CreateATask", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erreur création tâche");

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TaskItem>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


        }
        public async Task<Result<TaskItem?>?> CreateATaskAsync(TaskItem task)
        {
            EnsureAuthentification();
            var json = JsonSerializer.Serialize(task);

            var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PostAsync("api/Tasks/CreateATask", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erreur création tâche");

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result<TaskItem?>?>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            EnsureAuthentification();
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Tasks/DeleteATask?identifiant={id}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException("Tâche introuvable");
                }
                else if(!response.IsSuccessStatusCode)
                {
                    return false;
                }

                    return true;
            }
            catch (Exception ex) 
            {
                throw;
            }

        }

        public async Task<Result<TaskItem>> SetTaskDoneAsync(int id)
        {
            EnsureAuthentification();
            var json = JsonSerializer.Serialize(id);

            var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PutAsync("api/Tasks/SetTaskCompleted", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erreur ");

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result<TaskItem>>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


        }
        public async Task<Result<TaskItem>> SetTaskUndoneAsync(int id)
        {
            EnsureAuthentification();
            var json = JsonSerializer.Serialize(id);

            var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PutAsync("api/Tasks/SetTaskIncompleted", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erreur ");

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result<TaskItem>>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


        }
        public async Task<Result<TaskItem>> UpdateTaskAsync(int id, string title, string duedate, bool iscompleted, TaskPriority taskpriority)
        {
            EnsureAuthentification();
            var payload = new
            {
                identifiant = id,
                NewTitle = title,
                NewDueDate = duedate,
                NewIscompleted = iscompleted,
                Priority = taskpriority
            };

            var json = JsonSerializer.Serialize(payload);

            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PutAsync("api/Tasks/UpdateTask", content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"HTTP {(int)response.StatusCode} {response.StatusCode} - {responseJson}");

                var result = JsonSerializer.Deserialize<Result<TaskItem>>(
                    responseJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? throw new Exception("Réponse invalide (deserialize null).");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<User?> GetCurrentUser()
        {
            EnsureAuthentification();

            var response = await _httpClient.GetAsync("/api/auth/me");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<User>();
        }



    }
}
