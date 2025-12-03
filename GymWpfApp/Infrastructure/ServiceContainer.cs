using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using GymWpfApp.Services;
using System;
using System.Collections.Generic;

namespace GymWpfApp.Infrastructure
{
    /// <summary>
    /// Simple Dependency Injection Container
    /// Manages service instances and their lifecycle
    /// Implements Singleton pattern for services
    /// </summary>
    public class ServiceContainer
    {
        private static ServiceContainer _instance;
        private static readonly object _lock = new object();
        private readonly Dictionary<Type, object> _services;

        private ServiceContainer()
        {
            _services = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the singleton instance of ServiceContainer
        /// </summary>
        public static ServiceContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Registers a service instance
        /// </summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <typeparam name="TImplementation">Implementation type</typeparam>
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
        {
            _services[typeof(TInterface)] = new TImplementation();
        }

        /// <summary>
        /// Registers a service instance with a factory function
        /// </summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <param name="factory">Factory function to create the instance</param>
        public void Register<TInterface>(Func<TInterface> factory)
        {
            _services[typeof(TInterface)] = factory();
        }

        /// <summary>
        /// Registers a service instance directly
        /// </summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <param name="instance">Service instance</param>
        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _services[typeof(TInterface)] = instance;
        }

        /// <summary>
        /// Resolves a service instance
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service instance</returns>
        public T Resolve<T>()
        {
            Type type = typeof(T);
            if (_services.ContainsKey(type))
            {
                return (T)_services[type];
            }
            throw new InvalidOperationException($"Service of type {type.Name} is not registered");
        }

        /// <summary>
        /// Checks if a service is registered
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>True if registered, false otherwise</returns>
        public bool IsRegistered<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Initializes all default services for the application
        /// </summary>
        public void InitializeServices()
        {
            // Register Member Service
            var memberService = new MemberService();
            memberService.Load();
            RegisterInstance<IDataService<Member>>(memberService);

            // Register Staff Service
            var staffService = new StaffService();
            staffService.Load();
            RegisterInstance<IDataService<Staff>>(staffService);

            // Register Equipment Service
            var equipmentService = new EquipmentService();
            equipmentService.Load();
            RegisterInstance<IDataService<Equipment>>(equipmentService);
        }

        /// <summary>
        /// Clears all registered services (useful for testing)
        /// </summary>
        public void Clear()
        {
            _services.Clear();
        }
    }
}
