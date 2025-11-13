using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class Payment : EntityBase
    {
        public required String Method { get; set; }
        public bool Approved { get; set; }
        public decimal TotalAmount { get; set; }

        public long OrderId { get; set; }
        public required Order Order { get; set; }
    }
}