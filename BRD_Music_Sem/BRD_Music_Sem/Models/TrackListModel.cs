using DataGate.Core.Attributes;

namespace BRD_Music_Sem.Models
{
    public class TrackListModel
    {
        [Id]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}