using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using GymWpfApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GymWpfApp.Services
{
    /// <summary>
    /// Base implementation for data services
    /// Provides common CRUD operations and JSON persistence
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class BaseDataService<T> : IDataService<T> where T : BaseEntity
    {
        protected readonly string _dataFile;
        protected ObservableCollection<T> _items;

        protected BaseDataService(string dataFile)
        {
            _dataFile = dataFile;
            _items = new ObservableCollection<T>();
        }

        public virtual ObservableCollection<T> GetAll()
        {
            return _items;
        }

        public virtual void Load()
        {
            if (File.Exists(_dataFile))
            {
                try
                {
                    string json = File.ReadAllText(_dataFile);
                    var list = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
                    if (list != null)
                    {
                        _items.Clear();
                        foreach (var item in list)
                        {
                            _items.Add(item);
                        }
                        Logger.Write($"Loaded {_items.Count} items from {_dataFile}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write($"Lỗi load {typeof(T).Name}: {ex.Message}");
                }
            }
            else
            {
                Logger.Write($"File {_dataFile} chưa tồn tại, khởi tạo collection rỗng");
            }
        }

        public virtual void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_items, Formatting.Indented);
                File.WriteAllText(_dataFile, json);
                Logger.Write($"Saved {_items.Count} items to {_dataFile}");
            }
            catch (Exception ex)
            {
                Logger.Write($"Lỗi save {typeof(T).Name}: {ex.Message}");
            }
        }

        public virtual int NextId()
        {
            return _items.Count == 0 ? 1 : _items.Max(m => m.Id) + 1;
        }

        public virtual void Add(T entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = NextId();
            }
            _items.Add(entity);
            Save();
            Logger.Write($"Added {typeof(T).Name} with ID: {entity.Id}");
        }

        public virtual void Update(T entity)
        {
            entity.MarkAsModified();
            Save();
            Logger.Write($"Updated {typeof(T).Name} with ID: {entity.Id}");
        }

        public virtual void Remove(T entity)
        {
            _items.Remove(entity);
            Save();
            Logger.Write($"Removed {typeof(T).Name} with ID: {entity.Id}");
        }
    }
}
