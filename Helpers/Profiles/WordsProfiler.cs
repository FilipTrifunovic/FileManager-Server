using AutoMapper;
using server.Dtos;
using server.Models;

namespace Helpers.Profiles
{
    public class WordsProfiler :Profile
    {
        public WordsProfiler()
        {
            CreateMap<Words,WordsDto>();
        }
    }
}