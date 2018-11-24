using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApp
{
 
    public class Todo
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        public bool Checkmark { get; set; }
    }
  
}