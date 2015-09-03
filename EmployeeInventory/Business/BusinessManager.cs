using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ES.InventoryRegister.Business
{
    public class BusinessManager : IDisposable
    {
        private EmployeeBusiness _employeeBusiness { get; set; }
        private DepartmentBusiness _departmentBusiness { get; set; }
        private DeviceBusiness _deviceBusiness { get; set; }
        
        internal IUnitOfWork UnitOfWork { get; private set; }

        public BusinessManager()
        {
            // Create a new instance of the UnitOfWork
            UnitOfWork = new UnitOfWork();
        }

        public EmployeeBusiness EmployeeBusiness
        {
            get
            {
                // Create a new instance if it hasn't been accessed yet
                if (_employeeBusiness == null)
                    _employeeBusiness = new EmployeeBusiness(this);

                return _employeeBusiness;
            }
        }

        public DepartmentBusiness DepartmentBusiness
        {
            get
            {
                if (_departmentBusiness == null)
                    _departmentBusiness = new DepartmentBusiness(this);

                return _departmentBusiness;
            }
        }

        public DeviceBusiness DeviceBusiness
        {
            get
            {
                if (_deviceBusiness == null)
                    _deviceBusiness = new DeviceBusiness(this);

                return _deviceBusiness;
            }
        }

        /// <summary>
        /// Gets an entity type from a passed in string
        /// </summary>
        /// <param name="str">Type string</param>
        /// <returns>Entity type</returns>
        public Type GetEntityTypeFromStr(string str)
        {
            switch (str)
            {
                case "Computer": return typeof(Computer);
                case "Tablet": return typeof(Entities.Tablet);
                case "Laptop": return typeof(Laptop);
                case "Monitor": return typeof(Monitor);
                case "Phone": return typeof(Phone);
                case "Desktop": return typeof(Desktop);
                default: return null;
            }
        }

        /// <summary>
        /// Exports devices into a CSV format
        /// </summary>
        public void ExportAsXml(string savePath)
        {
            // Cretae the XML document
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement elmtRoot = xmlDoc.DocumentElement;

            // Insert the declaration at the top of the document
            xmlDoc.InsertBefore(xmlDeclaration, elmtRoot);

            // Create tag to hold devices under
            XmlElement elmtDevices = xmlDoc.CreateElement(string.Empty, "Devices", string.Empty);
            xmlDoc.AppendChild(elmtDevices);

            List<Device> devices = UnitOfWork.Devices.GetDevices();
            foreach (IEntity device in devices)
            {
                // Get the type of the device
                Type deviceType = ObjectContext.GetObjectType(device.GetType());
                // Check whether or not the type inherits from 'Computer'
                bool isComputer = deviceType.IsSubclassOf(typeof(Computer));

                Console.WriteLine(deviceType.Name);

                XmlElement elmtDevice = xmlDoc.CreateElement(string.Empty, "Device", string.Empty);
                elmtDevices.AppendChild(elmtDevice);

                foreach (var prop in device.GetType().GetProperties())
                {
                    if (prop.GetValue(device) != null)
                    {
                        var propVal = prop.GetValue(device);

                        if (prop.Name == "ProductKeys")
                        {
                            XmlElement elmtDeviceProductKeys = xmlDoc.CreateElement("ProductKeys");
                            elmtDevice.AppendChild(elmtDeviceProductKeys);

                            foreach (ProductKey key in (propVal as List<ProductKey>))
                            {
                                XmlElement elmtDeviceProductKey = xmlDoc.CreateElement("ProductKey");
                                elmtDeviceProductKeys.AppendChild(elmtDeviceProductKey);

                                // Get key name
                                XmlElement elmtDeviceProductKeyName = xmlDoc.CreateElement("Name");
                                elmtDeviceProductKey.AppendChild(elmtDeviceProductKeyName);

                                XmlText elmtDeviceProductKeyNameVal = xmlDoc.CreateTextNode(key.Name);
                                elmtDeviceProductKeyName.AppendChild(elmtDeviceProductKeyNameVal);

                                // Get key value
                                XmlElement elmtDeviceProductKeyValue = xmlDoc.CreateElement("Value");
                                elmtDeviceProductKey.AppendChild(elmtDeviceProductKeyValue);

                                XmlText elmtDeviceProductKeyValueVal = xmlDoc.CreateTextNode(key.Key);
                                elmtDeviceProductKeyValue.AppendChild(elmtDeviceProductKeyValueVal);
                            }
                        }
                        else if (prop.Name == "Owner")
                        {
                            // Treat the properly value as an employee
                            Employee owner = propVal as Employee;

                            XmlElement elmtDeviceOwner = xmlDoc.CreateElement("Owner");
                            elmtDevice.AppendChild(elmtDeviceOwner);

                            // Get owner name
                            XmlElement elmtDeviceOwnerName = xmlDoc.CreateElement("Name");
                            elmtDeviceOwner.AppendChild(elmtDeviceOwnerName);

                            XmlText elmtDeviceOwnerNameVal = xmlDoc.CreateTextNode(owner.Name);
                            elmtDeviceOwnerName.AppendChild(elmtDeviceOwnerNameVal);

                            // Get owner ID
                            XmlElement elmtDeviceOwnerId = xmlDoc.CreateElement("ID");
                            elmtDeviceOwner.AppendChild(elmtDeviceOwnerId);

                            XmlText elmtDeviceOwnerIdVal = xmlDoc.CreateTextNode(owner.Id.ToString());
                            elmtDeviceOwnerId.AppendChild(elmtDeviceOwnerIdVal);

                            // Get owner department
                            XmlElement elmtDeviceOwnerDepartment = xmlDoc.CreateElement("Department");
                            elmtDeviceOwner.AppendChild(elmtDeviceOwnerDepartment);

                            XmlText elmtDeviceOwnerDepartmentVal = xmlDoc.CreateTextNode(owner.Department.Name);
                            elmtDeviceOwnerDepartment.AppendChild(elmtDeviceOwnerDepartmentVal);
                        }
                        else
                        {
                            XmlElement elmtDeviceProp = xmlDoc.CreateElement(string.Empty, prop.Name, string.Empty);
                            elmtDevice.AppendChild(elmtDeviceProp);

                            XmlText elmtDevicePropVal = xmlDoc.CreateTextNode(propVal.ToString());
                            elmtDeviceProp.AppendChild(elmtDevicePropVal);
                        }
                    }

                    
                }
            }

            xmlDoc.Save(savePath);
        }

        #region Handle disposing
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UnitOfWork != null)
                {
                    // Save changes made to UnitOfWork
                    UnitOfWork.SaveChanges();

                    // Dispose
                    UnitOfWork.Dispose();
                }
            }
        }
        #endregion
        
    }
}
