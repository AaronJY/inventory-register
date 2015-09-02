using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Business
{
    /// <summary>
    /// Used to perform device-based logic
    /// </summary>
    public class DeviceBusiness : BusinessBase
    {
        private BusinessManager _businessManager;

        public DeviceBusiness(BusinessManager businessManager)
            : base(businessManager)
        {
            _businessManager = businessManager;
        }

        /// <summary>
        /// Creates a new device in the database
        /// </summary>
        /// <param name="device">Device</param>
        public void AddDevice(Device device)
        {
            UnitOfWork.Devices.AddDevice(device);
        }

        /// <summary>
        /// Gets all devices from the database
        /// </summary>
        /// <returns>List of devices</returns>
        public List<Device> GetDevices()
        {
            return UnitOfWork.Devices.GetDevices();
        }

        /// <summary>
        /// Gets the device from the database
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <returns>Device ID</returns>
        public Device GetDevice(int deviceId)
        {
            return UnitOfWork.Devices.GetDevice(deviceId);
        }

        /// <summary>
        /// Updates a device's values with the values of a passed
        /// in device instance
        /// </summary>
        /// <param name="device">Device</param>
        public void UpdateDevice(Device device)
        {
            UnitOfWork.Devices.UpdateDevice(device);
        }

        /// <summary>
        /// Marks a device as deleted in the database
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        public void DeleteDevice(int deviceId)
        {
            UnitOfWork.Devices.DeleteDevice(deviceId);
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
    }
}
