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
        IFileService fileService = new JsonFileService();
        IDialogService dialogService = new DefaultDialogService();
        #endregion

        #region Commands    
        public RelayCommand AddAccountCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    AddAccount(obj);
                });
            }
        }
        #endregion

        #region Private Methods
        private void AddAccount(object obj)
        {
            Account account = new Account(login: Login, password: Password);
            if (CheckAccount(Login))
            {
                dialogService.ShowMessage("Аккаунт уже добавлен!");
                return;
            }
            else
            {
                App.MainViewModel.Accounts.Insert(0, account);
                SaveData();
                dialogService.ShowMessage("Аккаунт успешно добавлен!");
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
            File.WriteAllText("accounts.json", JsonConvert.SerializeObject(App.MainViewModel.Accounts.ToList(), Formatting.Indented));
        }
        #endregion
    }
}
