﻿using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
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

        /// <summary>
        /// Asks the device repository for all the devices that
        /// are currently tied to a given department
        /// </summary>
        /// <param name="departmentName">Department name</param>
        /// <returns>Devices</returns>
        public List<Device> GetDevicesInUseByDepartment(string departmentName)
        {
            return UnitOfWork.Devices.GetDevicesInUseByDepartment(departmentName);
        }

        /// <summary>
        /// Asks the device repository whether or not the given asset
        /// number is already in use
        /// </summary>
        /// <param name="assetNumber">Asset number</param>
        /// <returns>Result</returns>
        public bool IsAssetNumberInUse(int assetNumber)
        {
            return UnitOfWork.Devices.IsAssetNumberInUse(assetNumber);
        }

        public void GetComputerAsViewModel(int deviceId)
        {
            Computer entity = (Computer)UnitOfWork.Devices.GetDevice(deviceId);

            Console.WriteLine(entity.Processor);
        }
    }
}
