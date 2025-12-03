using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using System.Collections.ObjectModel;

namespace GymWpfApp.Services
{
    /// <summary>
    /// Service for managing gym members
    /// Implements IDataService interface and inherits from BaseDataService
    /// </summary>
    public class MemberService : BaseDataService<Member>, IDataService<Member>
    {
        private const string DATA_FILE = "members.json";

        public MemberService() : base(DATA_FILE)
        {
        }

        /// <summary>
        /// Gets all members (for backward compatibility)
        /// </summary>
        public ObservableCollection<Member> Members => GetAll();
    }
}
