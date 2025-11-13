namespace GamerSpace.Application.DTOs
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
    }
}