using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace MVVM
{
    public class AddAccountViewModel : BaseViewModel
    {
        #region Public Properties  
        
        public string? Login { get; set; }
        public string? Password { get; set; }
        IFileService FileService = new JsonFileService();
        IDialogService DialogService = new DefaultDialogService();
        #endregion

        #region Commands    
        public RelayCommand AddAccountCommand
        {
            get
            {
                return new RelayCommand(AddAccount);
            }
        }
        #endregion

        #region Private Methods
        private void AddAccount(object obj)
        {
            Account account = new Account(login: Login, password: Password);
            if (CheckAccount(Login))
            {
                DialogService.ShowMessage("Аккаунт уже добавлен!");
                return;
            }
            else
            {
                App.MainViewModel.Accounts.Insert(0, account);
                SaveData();
                DialogService.ShowMessage("Аккаунт успешно добавлен!");
            }
        }
        private bool CheckAccount(string Login)
        {
            bool re = true;
            var acc = App.MainViewModel.Accounts.Where(acc => acc.Login == Login).FirstOrDefault();
            if (acc != null) 
            {
                re = true;
            }
            else
            {
                re = false;
            }  
            return re;
        }

        private void SaveData()
        {
            FileService.Save("accounts.json", App.MainViewModel.Accounts.ToList());
            //File.WriteAllText("accounts.json", JsonConvert.SerializeObject(, Formatting.Indented));
        }
        #endregion
    }
}
