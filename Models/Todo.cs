using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_2.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public string Description { get; set; } = null!;
        
        [Required]
        public bool IsComplete { get; set; }


        public ToDo(int priority, string description, bool isComplete) 
        {
            this.Priority = priority;
            this.Description= description;
            this.IsComplete = isComplete;
        }

    }
}
