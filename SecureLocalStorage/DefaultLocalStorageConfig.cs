﻿using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using DeviceId;

namespace SecureLocalStorage
{
    public class DefaultLocalStorageConfig : ISecureLocalStorageConfig
    {
        public string DefaultPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public string ApplicationName
        {
            get
            {
                var myProduct =
                    (AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
                        typeof(AssemblyProductAttribute));
                return myProduct.Product;
            }

        }

        public string StoragePath => Path.Combine(DefaultPath, ApplicationName);

        public Func<string> BuildLocalSecureKey { get; set; }  = () => new DeviceIdBuilder()
            .AddMachineName()
            .AddProcessorId()
            .AddMotherboardSerialNumber()
            .AddSystemDriveSerialNumber()
            .ToString();
    }
}
