using QuanLyPhongGym.Services;

namespace QuanLyPhongGym.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MemberListViewModel Members { get; }
        public PlanListViewModel Plans { get; }
        public TrainerListViewModel Trainers { get; }
        public ScheduleListViewModel Schedules { get; }

        public MainViewModel()
        {
            var svc = new GymService();
            Members = new MemberListViewModel(svc);
            Plans = new PlanListViewModel(svc);
            Trainers = new TrainerListViewModel(svc);
            Schedules = new ScheduleListViewModel(svc);
        }
    }
}