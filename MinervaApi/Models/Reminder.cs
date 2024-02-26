using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Models
{
    public class Reminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int remindersAutoId { get; set; }

        public int RemindersId { get; set; }

        public int TenantId { get; set; }

        [StringLength(45)]
        public string Details { get; set; }
    }
}
