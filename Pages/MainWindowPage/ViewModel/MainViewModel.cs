using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Windows;

namespace MVVM
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Public Properties
        public string Title { get; set; } = "Hello AgNet";

        IFileService fileService = new JsonFileService();
        IDialogService dialogService = new DefaultDialogService();

        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>
        {
            new Account("antonsukharev2016", "samsung9300duos")
        };

        public Account SelectedAccount { get; set; }

        #endregion

        #region NewAccount Props
        public string NewAccount_Login { get; set; }
        public string NewAccount_Password { get; set; }
        #endregion


        // Переписать команды
        #region Commands     

        // команда открытия нового окна
        public RelayCommand AddAccountOpenWindow
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    dialogService.NewWindow();
                });
            }
        }

        // Добавить новый аккаунт
        public RelayCommand AddNewAccountCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Account account = new Account(NewAccount_Login, NewAccount_Password);
                    Accounts.Insert(0, account);
                });
            }
        }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, Accounts.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              List<Account> phones = fileService.Open(dialogService.FilePath);
                              Accounts.Clear();
                              foreach (var p in phones)
                              {
                                  Accounts.Add(p);
                              }
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Account account = new Account("", "");
                      Accounts.Insert(0, account);
                      SelectedAccount = account;
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Account account = obj as Account;
                        if (account != null)
                        {
                            Accounts.Remove(account);
                        }
                    },
                    (obj) => Accounts.Count > 0));
            }
        }

        public RelayCommand? doubleCommand;
        public RelayCommand DoubleCommand
        {
            get
            {
                return doubleCommand ??
                    (doubleCommand = new RelayCommand(obj =>
                    {
                        Account? account = obj as Account;
                        if (account != null)
                        {
                            Account phoneCopy = new Account(account.Login, account.Password);
                            Accounts.Insert(0, phoneCopy);
                        }
                    }));
            }
        }
        #endregion


        #region Constructor     
        public MainViewModel()
        {
        }
        #endregion
    }
}
