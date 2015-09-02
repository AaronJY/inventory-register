using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ES.InventoryRegister.XAML;

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
        /// Creates a new device entry in the database
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
        /// Returns a list of all devices currently in the database
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
        /// Gets a device entity from the database with a
        /// given ID
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <returns>Device</returns>
        public Device GetDevice(int deviceId)
        {
            // Select the first device in the database
            // that has the passed in ID
            Device device = _context.Set<Device>()
                .Include(x => x.Owner)
                .FirstOrDefault(x => x.Id == deviceId);

            Type deviceType = ObjectContext.GetObjectType(device.GetType());

            if (deviceType.IsSubclassOf(typeof(Computer)))
            {
                device = _context.Set<Computer>()
                    .Include(x => x.ProductKeys)
                    .Include(x => x.Owner)
                    .FirstOrDefault(x => x.Id == deviceId);
            }
            
            return device;
        }

        /// <summary>
        /// Updates a device entity in the database with
        /// the property values of a passed in entity
        /// </summary>
        /// <param name="newDevice">Device</param>
        public void UpdateDevice(Device newDevice)
        {
            // Get the device currently existing in the database
            Device existingDevice = GetDevice(newDevice.Id);

            // Update the existing device's base properties with the
            // passed in device's base properties
            _context.Entry(existingDevice).CurrentValues.SetValues(newDevice);

            // Update the existing device's owner
            _context.Entry(existingDevice.Owner).CurrentValues.SetValues(newDevice.Owner);

            // Save the changes to the database
            _context.SaveChanges();
        }

        /// <summary>
        /// Marks a device as deleted in the database
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public void DeleteDevice(int deviceId)
        {
            _context.Entry(GetDevice(deviceId)).Entity.Deleted = true;

            _context.SaveChanges();
        }
    }
}
