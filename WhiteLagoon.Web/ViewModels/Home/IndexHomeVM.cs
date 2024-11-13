using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Web.ViewModels.Home
{
    public class IndexHomeVM
    {
        public IEnumerable<Villa>? VillaList { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public int Nights { get; set; } //Number Of Night
    }
}
