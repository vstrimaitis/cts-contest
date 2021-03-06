﻿using CtsContestWeb.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtsContestWeb.Communication
{
    public interface ITaskManager
    {
        Task<List<TaskDto>> GetAllTasks(string userEmail = null);
        Task<TaskDto> DownloadTaskByIdAsync(int id);
        Task<TaskDto> GetCachedTaskByIdAsync(int id, string userEmail = null);
        Task<int?> GetTaskIdForDuelAsync(IEnumerable<string> usersEmail);
        Task<bool> HasPlayerAnyDuelTasksLeft(string userEmail);
        void RemoveTasksCache();
    }
}