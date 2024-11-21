using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;

namespace AppDev.API.Models.DataTransferObject.Confession
{
    public class ConfessionDTO
    {

        public Guid StudentId { get; set; }
        public ConfessionContextType ContextType { get; set; }
        public StudentDTO Student { get; set; }
        public string ContextValue { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
