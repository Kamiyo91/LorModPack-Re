using System.Collections.Generic;

namespace BLL_Re21341.Models
{
    public class ExtraRewards
    {
        public string MessageId { get; set; }
        public List<DropIdQuantity> DroppedBooks { get; set; }
        public List<int> DroppedKeypages { get; set; }
    }

    public class DropIdQuantity
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}