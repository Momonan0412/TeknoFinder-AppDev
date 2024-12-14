using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppDev.API.Models.EnumValidation;

namespace AppDev.API.Models.Entities
{

    public class Confession
    {
        public Guid ConfessionId { get; set; }

        [Required]
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        [ValidEnum(typeof(ConfessionContextType))]
        public ConfessionContextType ContextType { get; set; }
        [Required]
        public string ContextValue { get; set; }

        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(1500)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }  = DateTime.UtcNow;
    }
}
