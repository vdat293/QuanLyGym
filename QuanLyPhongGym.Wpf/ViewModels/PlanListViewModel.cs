using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QuanLyPhongGym.Models;
using QuanLyPhongGym.Services;

namespace QuanLyPhongGym.ViewModels
{
    public class PlanListViewModel : ViewModelBase
    {
        private readonly GymService _svc;
        public ObservableCollection<MembershipPlan> Items { get; } = new();
        private MembershipPlan? _selected;
        public MembershipPlan? Selected { get => _selected; set => Set(ref _selected, value); }

        public RelayCommand LoadCmd { get; }
        public RelayCommand AddCmd { get; }
        public RelayCommand SaveCmd { get; }
        public RelayCommand DeleteCmd { get; }

        public PlanListViewModel(GymService svc)
        {
            _svc = svc;
            LoadCmd = new RelayCommand(async _ => await LoadAsync());
            AddCmd = new RelayCommand(_ => Items.Add(new MembershipPlan { Name = "Gói mới", DurationDays = 30 }));
            SaveCmd = new RelayCommand(async _ => await SaveAsync(), _ => Selected != null);
            DeleteCmd = new RelayCommand(async _ => await DeleteAsync(), _ => Selected != null);
            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            Items.Clear();
            // include Members để DataGrid có thể đếm Members.Count
            foreach (var p in await _svc.Plans.GetAllAsync(null, nameof(QuanLyPhongGym.Models.MembershipPlan.Members)))
                Items.Add(p);
        }
        private async Task SaveAsync()
        {
            if (Selected == null) return;
            if (Selected.Id == 0) await _svc.Plans.AddAsync(Selected);
            else await _svc.Plans.UpdateAsync(Selected);
            await LoadAsync();
        }
        private async Task DeleteAsync()
        {
            if (Selected == null) return;
            await _svc.Plans.DeleteAsync(Selected.Id);
            await LoadAsync();
        }
    }
}