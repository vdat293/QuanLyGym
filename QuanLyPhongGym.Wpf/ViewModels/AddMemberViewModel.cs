using QuanLyPhongGym.Models;
using QuanLyPhongGym.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyPhongGym.ViewModels
{
    public class AddMemberViewModel : ViewModelBase
    {
        private readonly GymService _svc;

        // Form fields
        private string _familyName = string.Empty;
        public string FamilyName { get => _familyName; set => Set(ref _familyName, value); }

        private string? _middleName;
        public string? MiddleName { get => _middleName; set => Set(ref _middleName, value); }

        private string _givenName = string.Empty;
        public string GivenName { get => _givenName; set => Set(ref _givenName, value); }

        private string? _cccd;
        public string? CCCD { get => _cccd; set => Set(ref _cccd, value); }

        private string? _phone;
        public string? Phone { get => _phone; set => Set(ref _phone, value); }

        private DateTime? _birthDate;
        public DateTime? BirthDate { get => _birthDate; set => Set(ref _birthDate, value); }

        private MembershipPlan? _selectedPlan;
        public MembershipPlan? SelectedPlan
        {
            get => _selectedPlan;
            set
            {
                Set(ref _selectedPlan, value);
                OnPropertyChanged(nameof(DurationMonths));
            }
        }

        private int _ptStatusIndex = 0;
        public int PtStatusIndex { get => _ptStatusIndex; set => Set(ref _ptStatusIndex, value); }

        public ObservableCollection<MembershipPlan> AvailablePlans { get; } = new();

        // Display duration in months from selected plan
        public string DurationMonths => SelectedPlan != null
            ? $"{SelectedPlan.Months} tháng"
            : "Chọn gói tập";

        public RelayCommand SaveCmd { get; }

        public bool DialogResult { get; private set; }

        public AddMemberViewModel(GymService svc)
        {
            _svc = svc;
            SaveCmd = new RelayCommand(async _ => await SaveAsync(), _ => CanSave());
            _ = LoadPlansAsync();
        }

        private async Task LoadPlansAsync()
        {
            var plans = await _svc.Plans.GetAllAsync();
            foreach (var plan in plans)
            {
                AvailablePlans.Add(plan);
            }

            if (AvailablePlans.Any())
            {
                SelectedPlan = AvailablePlans.First();
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(FamilyName)
                && !string.IsNullOrWhiteSpace(GivenName)
                && SelectedPlan != null;
        }

        private async Task SaveAsync()
        {
            if (!CanSave()) return;

            try
            {
                var ptStatus = PtStatusIndex switch
                {
                    0 => PtStatus.KhongCoPT,
                    1 => PtStatus.CoPT,
                    2 => PtStatus.PTNgoai,
                    _ => PtStatus.KhongCoPT
                };

                var member = new Member
                {
                    FamilyName = FamilyName.Trim(),
                    MiddleName = string.IsNullOrWhiteSpace(MiddleName) ? null : MiddleName.Trim(),
                    GivenName = GivenName.Trim(),
                    CCCD = string.IsNullOrWhiteSpace(CCCD) ? null : CCCD.Trim(),
                    Phone = string.IsNullOrWhiteSpace(Phone) ? null : Phone.Trim(),
                    BirthDate = BirthDate,
                    MembershipPlanId = SelectedPlan!.Id,
                    JoinDate = DateTime.Today,
                    PtStatus = ptStatus,
                    IsActive = true,
                    TotalTrainingDays = 0
                };

                await _svc.Members.AddAsync(member);
                DialogResult = true;

                MessageBox.Show(
                    $"Đã thêm hội viên: {member.FullNameDisplay}",
                    "Thành công",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi thêm hội viên:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                DialogResult = false;
            }
        }
    }
}
