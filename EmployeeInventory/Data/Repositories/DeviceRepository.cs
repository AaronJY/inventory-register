using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ES.InventoryRegister.Data.Repositories
{
    public class DeviceRepository : RepositoryBase<Device>
    {
        private InventoryDbContext _context;

        public DeviceRepository(InventoryDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new device
        /// </summary>
        /// <param name="device">Device entity</param>
        public void AddDevice(Device device)
        {
            // Update the CreationDate
            device.CreationDate = DateTime.Now;

            // Attach the owner to the context
            _context.Set<Employee>().Attach(device.Owner);

            // Add the device to the database
            _context.Set<Device>().Add(device);
        }

        /// <summary>
        /// Returns a list of all devices
        /// </summary>
        /// <returns>List of devices</returns>
        public List<Device> GetDevices()
        {
            List<Device> devices = _context.Set<Device>()
                .Where(x => x.Deleted == false)
                .ToList();

            return devices;
        }

        /// <summary>
        /// Gets the device entity from the database
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <returns>Device</returns>
        public Device GetDevice(int deviceId)
        {
            Device device = _context.Set<Device>()
                .Include(x => x.Owner)
                .FirstOrDefault(x => x.Id == deviceId);

            Type deviceType = ObjectContext.GetObjectType(device.GetType());

            if (deviceType.IsSubclassOf(typeof(Computer)))
            {
                device = _context.Set<Computer>()
                    .Include(x => x.ProductKeys)
                    .FirstOrDefault(x => x.Id == deviceId);
            }
            
            return device;
        }

        /// <summary>
        /// Updates a device with a given Device's values
        /// </summary>
        /// <param name="device">Device</param>
        public void UpdateDevice(Device device)
        {
            Device currentDevice = GetDevice(device.Id);

            _context.Entry(currentDevice).CurrentValues.SetValues(device);
            _context.SaveChanges();
        }

        /// <summary>
        /// Marks a device as deleted
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public void DeleteDevice(int deviceId)
        {
            _context.Entry(GetDevice(deviceId)).Entity.Deleted = true;
            _context.SaveChanges();
        }
    }
}
