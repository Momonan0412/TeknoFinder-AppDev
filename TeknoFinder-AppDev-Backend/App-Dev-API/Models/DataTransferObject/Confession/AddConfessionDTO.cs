using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;
using AppDev.API.Models.EnumValidation;

namespace AppDev.API.Models.DataTransferObject.Confession
{
    public class AddConfessionDTO 
    {
        public new Guid StudentId { get; set; }
        public new ConfessionContextType ContextType { get; set; }
        public new string ContextValue { get; set; }
        public new string Title { get; set; }
        public new string Content { get; set; }
    }
}
