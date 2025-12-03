namespace GymWpfApp.Models
{
    /// <summary>
    /// Base class for person-related entities (Member, Staff)
    /// Provides common properties for people
    /// </summary>
    public abstract class Person : BaseEntity
    {
        private string _name;
        private string _phone;
        private int _age;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                MarkAsModified();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                MarkAsModified();
            }
        }

        public string Gender { get; set; }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                MarkAsModified();
            }
        }

        /// <summary>
        /// Gets the display name for the person
        /// </summary>
        public virtual string DisplayName => Name;
    }
}
