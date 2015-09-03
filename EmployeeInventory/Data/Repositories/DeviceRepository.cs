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
        public List<Device> GetDevices(bool includeDeleted = false)
        {
            List<Device> devices = _context.Set<Device>()
                .Where(x => (x.Deleted == false && includeDeleted == false))
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
            //!!! Really should use reflection to auto populate
            //!!! properties for the below!

            // Get the device currently existing in the database
            Device existingDevice = GetDevice(newDevice.Id);
            // Get the type of the device
            Type deviceType = ObjectContext.GetObjectType(existingDevice.GetType());

            existingDevice.Owner = newDevice.Owner;
            existingDevice.Make = newDevice.Make;
            existingDevice.Model = newDevice.Model;
            existingDevice.PurchaseDate = newDevice.PurchaseDate;
            existingDevice.ExpiryDate = newDevice.ExpiryDate;
            existingDevice.SerialNumber = newDevice.SerialNumber;
            existingDevice.Name = newDevice.Name;
            existingDevice.Notes = newDevice.Notes;

            // Update UpdateDate
            existingDevice.UpdateDate = DateTime.Now;

            // Update values based on the type of device
            if (deviceType.IsSubclassOf(typeof(Computer)))
            {
                Computer existingComputer = existingDevice as Computer;
                Computer newComputer = newDevice as Computer;

                existingComputer.DiskSpace = newComputer.DiskSpace;
                existingComputer.Processor = newComputer.Processor;
                existingComputer.Memory = newComputer.Memory;
                existingComputer.DiskType = newComputer.DiskType;
                existingComputer.OperatingSystem = newComputer.OperatingSystem;
                existingComputer.ProductKeys = newComputer.ProductKeys;

                if (deviceType == typeof(Phone))
                {
                    Phone existingPhone = existingComputer as Phone;
                    Phone newPhone = newComputer as Phone;

                    existingPhone.HasCamera = newPhone.HasCamera;
                }
            } 
            else if (deviceType == typeof(Monitor))
            {
                Monitor existingMonitor = existingDevice as Monitor;
                Monitor newMonitor = newDevice as Monitor;

                existingMonitor.DisplayInterfaces = newMonitor.DisplayInterfaces;
                existingMonitor.ScreenSize = newMonitor.ScreenSize;
            }
            
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
