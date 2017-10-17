﻿using CtsContestWeb.Dto;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CtsContestWeb.Db.Repository;

namespace CtsContestWeb.Communication
{
    public class TaskManager : ITaskManager
    {
        private readonly IConfiguration _iconfiguration;
        private readonly ISolutionRepository _solutionRepository;
        private readonly ICompiler _compiler;

        public TaskManager(IConfiguration iconfiguration, ISolutionRepository solutionRepository, ICompiler compiler)
        {
            _iconfiguration = iconfiguration;
            _solutionRepository = solutionRepository;
            _compiler = compiler;
        }

        public async Task<List<TaskDto>> GetAllTasks(string userEmail = null)
        {
            var umbracoApiUrl = _iconfiguration["UmbracoApiUrl"];
            var client = new RestClient(umbracoApiUrl);

            var request = new RestRequest("task/getAll", Method.GET);

            TaskCompletionSource<List<TaskDto>> taskCompletion = new TaskCompletionSource<List<TaskDto>>();
            client.ExecuteAsync<List<TaskDto>>(request, response =>
            {
                taskCompletion.SetResult(response.Data);
            });

            var tasks = await taskCompletion.Task;
            var solvedTasks = _solutionRepository.GetSolvedTasksIdsByUserEmail(userEmail).ToList();

            foreach (var task in tasks)
            {
                if (userEmail != null)
                {
                    if (solvedTasks.Any(t => t == task.Id))
                        task.IsSolved = true;
                }
            }

            return tasks;
        }

        public async Task<TaskDto> GetTaskById(int id, string userEmail = null)
        {
            var umbracoApiUrl = _iconfiguration["UmbracoApiUrl"];
            var pictureUrl = _iconfiguration["UmbracoPictureUrl"];
            var client = new RestClient(umbracoApiUrl);

            var request = new RestRequest("task/get/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());

            TaskCompletionSource<TaskDto> taskCompletion = new TaskCompletionSource<TaskDto>();
            client.ExecuteAsync<TaskDto>(request, response =>
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new ArgumentException("No task with given ID");
                taskCompletion.SetResult(response.Data);
            });

            var task = await taskCompletion.Task;
            if (userEmail != null)
            {
                var solvedTasks = _solutionRepository.GetSolvedTasksIdsByUserEmail(userEmail);
                if (solvedTasks.Any(t => t == task.Id))
                    task.IsSolved = true;
            }
            task.Description = PrependRootUrlToImageLinks(task.Description, pictureUrl);

            return task;
        }

        public async Task<CodeSkeletonDto> GetCodeSkeleton(string userEmail, int taskId, string language)
        {
            var solution = _solutionRepository.GetSolution(userEmail, taskId);
            
            if (solution != null)
            {
                var languages = await _compiler.GetLanguages();
                var languageCode = languages.Codes.FirstOrDefault(c => c.Value == solution.Language).Key;
                return new CodeSkeletonDto
                {
                    Language = languageCode,
                    Skeleton = solution.Source
                };
            }

            var umbracoApiUrl = _iconfiguration["UmbracoApiUrl"];
            var client = new RestClient(umbracoApiUrl);

            var request = new RestRequest("codeskeleton/get/{language}", Method.GET);
            request.AddUrlSegment("language", language);

            TaskCompletionSource<CodeSkeletonDto> taskCompletion = new TaskCompletionSource<CodeSkeletonDto>();
            client.ExecuteAsync<CodeSkeletonDto>(request, response =>
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new ArgumentException("Error getting code skeleton");
                taskCompletion.SetResult(response.Data);
            });

            var skeleton = await taskCompletion.Task;

            return skeleton;
        }

        private string PrependRootUrlToImageLinks(string description, string url)
        {
            var htmlPattern = @"(src="")(/media/(.+?)"")";
            var newDescription = Regex.Replace(description, htmlPattern, "$1" + url + "$2");

            return newDescription;
        }
    }
}
