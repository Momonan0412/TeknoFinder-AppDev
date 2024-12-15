using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;

namespace AppDev.API.Models.DataTransferObject.Confession
{
    public class GetAllConfessionDTO : AddConfessionDTO
    {

        public Guid StudentId { get; set; }
        //public ConfessionContextType ContextType { get; set; }
        public new StudentDTO Student { get; set; }

        public DateTime CreatedOn { get; set; }
        //public string ContextValue { get; set; }
        //public string Title { get; set; }
        //public string Content { get; set; }

    }
}
