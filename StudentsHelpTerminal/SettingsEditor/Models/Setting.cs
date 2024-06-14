using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace SettingsEditor.Models
{
    public class Setting
    {
        #region Name

        private string _name;
        public string Name 
        { 
            get => _name; 
            set => _name = value.Trim(); 
        }

        #endregion
        #region Value

        private object _value;
        public object Value 
        {
            get => _value;
            set 
            {
                try
                {
                    if (Type == typeof(DirectoryInfo))
                    {
                        _value = new DirectoryInfo(value.ToString());
                        IsValid = (_value as DirectoryInfo).Exists;
                        InvalidMessage = IsValid ? "" : "Error! Directory is not exist";
                        return;
                    }

                    if (Type == typeof(FileInfo))
                    {
                        _value = new FileInfo(value.ToString());
                        IsValid = (_value as FileInfo).Exists;
                        InvalidMessage = IsValid ? "" : "Error! File is not exist";
                        return;
                    }
                
                    _value = Convert.ChangeType(value, Type);
                    IsValid = true;
                }
                catch (Exception ex)
                { 
                    IsValid = false;   
                    InvalidMessage = ex.Message;
                }
            } 
        }

        #endregion
        #region Type

        public Type Type { get; set; }

        #endregion
        #region Description

        private string _description;

        public string Description 
        {
            get => _description; 
            set => _description = value.Trim();
        }

        #endregion
        #region Section

        private string _section;

        public string Section
        {
            get => _section;
            set => _section = value.Trim();
        }

        #endregion
        #region Validation parameters

        public bool IsValid { get; private set; }
        public string InvalidMessage { get; private set; }

        #endregion
    }
}
