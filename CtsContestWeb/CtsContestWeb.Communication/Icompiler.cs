﻿using System.Threading.Tasks;
using CtsContestWeb.Dto;

namespace CtsContestWeb.Communication
{
    public interface ICompiler
    {
        Task<LanguageDto> GetLanguages();
        Task<CompileDto> Compile(TaskDto task, string source, int language);
    }
}