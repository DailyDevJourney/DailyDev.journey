using OnedayOneDev_Shared;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace OneDayOneDev.http
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string BaseUrl)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };

            
        }

        public async Task<PageResult> GetTaskList()
        {
            var response = await _httpClient.GetAsync("api/Tasks/GetAllTask");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PageResult>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new PageResult();


        }
        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
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



    }
}
