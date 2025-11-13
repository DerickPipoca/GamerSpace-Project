namespace GamerSpace.Application.DTOs
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public long TypeId { get; set; }
    }
}