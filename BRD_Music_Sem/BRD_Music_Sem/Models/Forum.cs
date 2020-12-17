using System.Collections.Generic;
using DataGate.Core.Attributes;

namespace BRD_Music_Sem.Models
{
    public class Forum
    {
        [Id]
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string,string> Content { get; set; }
    }
}