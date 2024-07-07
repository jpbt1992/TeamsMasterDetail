using TeamsMasterDetail.Domain.Entities;

namespace TeamsMasterDetail.Application.Models
{
    public class MasterDetailViewModel
    {
        public List<Team>? Teams { get; set; }

        public Team? SelectedTeam { get; set; }

        public Member? SelectedMember { get; set; }

        public InputData InputData { get; set; }

        public DisplayMode DisplayMode { get; set; }
    }
}
