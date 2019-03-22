using System.Collections.Generic;
using HorseRacing.Models;

namespace HorseRacing.Services
{
    public interface ISelection
    {
        List<Horse> Get(int total = 6);
    }
}