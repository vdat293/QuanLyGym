using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using System.Collections.ObjectModel;

namespace GymWpfApp.Services
{
    /// <summary>
    /// Service for managing gym staff
    /// Implements IDataService interface and inherits from BaseDataService
    /// </summary>
    public class StaffService : BaseDataService<Staff>, IDataService<Staff>
    {
        private const string DATA_FILE = "staff.json";

        public StaffService() : base(DATA_FILE)
        {
        }

        /// <summary>
        /// Gets all staff (for backward compatibility)
        /// </summary>
        public ObservableCollection<Staff> StaffList => GetAll();
    }
}
