using System;

namespace GymWpfApp.Models
{
    /// <summary>
    /// Base class for all entities in the system
    /// Provides common properties like Id, CreatedDate, ModifiedDate
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        protected BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Updates the ModifiedDate to current time
        /// </summary>
        public virtual void MarkAsModified()
        {
            ModifiedDate = DateTime.Now;
        }
    }
}
