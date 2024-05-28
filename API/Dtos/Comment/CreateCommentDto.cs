using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at leas 5 characters.")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at leas 5 characters.")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
