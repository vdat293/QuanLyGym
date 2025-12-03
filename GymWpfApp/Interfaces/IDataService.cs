using System.Collections.ObjectModel;
using GymWpfApp.Models;

namespace GymWpfApp.Interfaces
{
    /// <summary>
    /// Generic interface for data services
    /// Provides CRUD operations for entities
    /// </summary>
    /// <typeparam name="T">Entity type (must inherit from BaseEntity)</typeparam>
    public interface IDataService<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets the collection of all entities
        /// </summary>
        ObservableCollection<T> GetAll();

        /// <summary>
        /// Loads data from persistent storage
        /// </summary>
        void Load();

        /// <summary>
        /// Saves data to persistent storage
        /// </summary>
        void Save();

        /// <summary>
        /// Gets the next available ID
        /// </summary>
        /// <returns>Next ID value</returns>
        int NextId();

        /// <summary>
        /// Adds a new entity to the collection
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(T entity);

        /// <summary>
        /// Removes an entity from the collection
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        void Remove(T entity);
    }
}
