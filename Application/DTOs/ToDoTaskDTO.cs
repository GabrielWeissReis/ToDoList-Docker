using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ToDoTaskDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters long")]
    public string Title { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 500 characters long")]
    public string Description { get; set; }

    [Required]
    public bool IsCompleted { get; set; }
}
