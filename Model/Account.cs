﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVVM
{
    public class Account : BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Account(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
        //#region Events
        //public event PropertyChangedEventHandler? PropertyChanged;
        //#endregion
        //#region Protected Methods
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }

    public class AccountMain
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class RootAccs
    {
        public List<AccountMain> MyArray { get; set; }
    }
}