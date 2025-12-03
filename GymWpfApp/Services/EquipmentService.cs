using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using System.Collections.ObjectModel;

namespace GymWpfApp.Services
{
    /// <summary>
    /// Service for managing gym equipment
    /// Implements IDataService interface and inherits from BaseDataService
    /// </summary>
    public class EquipmentService : BaseDataService<Equipment>, IDataService<Equipment>
    {
        private const string DATA_FILE = "equipment.json";

        public EquipmentService() : base(DATA_FILE)
        {
        }

        /// <summary>
        /// Gets all equipment (for backward compatibility)
        /// </summary>
        public ObservableCollection<Equipment> EquipmentList => GetAll();
    }
}
