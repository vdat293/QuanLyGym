using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QuanLyPhongGym.Models;
using QuanLyPhongGym.Services;


namespace QuanLyPhongGym.ViewModels
{
    public class ScheduleListViewModel : ViewModelBase
    {
        private readonly GymService _svc;
        public ObservableCollection<WorkoutSchedule> Items { get; } = new();
        public ObservableCollection<Member> Members { get; } = new();
        public ObservableCollection<Trainer> Trainers { get; } = new();

        private WorkoutSchedule? _selected;
        public WorkoutSchedule? Selected { get => _selected; set => Set(ref _selected, value); }

        public RelayCommand LoadCmd { get; }
        public RelayCommand AddCmd { get; }
        public RelayCommand SaveCmd { get; }
        public RelayCommand DeleteCmd { get; }

        public ScheduleListViewModel(GymService svc)
        {
            _svc = svc;
            LoadCmd = new RelayCommand(async _ => await LoadAsync());
            AddCmd = new RelayCommand(_ => Items.Add(new WorkoutSchedule { Title = "Buổi tập mới", StartTime = System.DateTime.Now, EndTime = System.DateTime.Now.AddHours(1) }));
            SaveCmd = new RelayCommand(async _ => await SaveAsync(), _ => Selected != null);
            DeleteCmd = new RelayCommand(async _ => await DeleteAsync(), _ => Selected != null);
            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            Items.Clear(); Members.Clear(); Trainers.Clear();
            foreach (var m in await _svc.Members.GetAllAsync()) Members.Add(m);
            foreach (var t in await _svc.Trainers.GetAllAsync()) Trainers.Add(t);
            foreach (var s in await _svc.Schedules.GetAllAsync(null, nameof(WorkoutSchedule.Member), nameof(WorkoutSchedule.Trainer))) Items.Add(s);
        }
        private async Task SaveAsync()
        {
            if (Selected == null) return;
            if (Selected.Id == 0) await _svc.Schedules.AddAsync(Selected);
            else await _svc.Schedules.UpdateAsync(Selected);
            await LoadAsync();
        }
        private async Task DeleteAsync()
        {
            if (Selected == null) return;
            await _svc.Schedules.DeleteAsync(Selected.Id);
            await LoadAsync();
        }
    }
}