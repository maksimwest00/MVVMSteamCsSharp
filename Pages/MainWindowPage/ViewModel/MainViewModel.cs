using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Windows.Controls;

namespace MVVM
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Public Properties
        public string Title { get; set; } = "Hello AgNet";

        IFileService FileService = new JsonFileService();
        IDialogService DialogService = new DefaultDialogService();

        public Account SelectedAccount { get; set; }

        public string SearchQuery { get; set; }

        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>
        {
            new Account("antonsukharev2016", "samsung9300duos"),
            new Account("rus_maxon00", "samsung9300duos"),
            new Account("sharyla1", "samsung9300duos"),
            new Account("7k2hlu4nw", "samsung9300duos"),
            new Account("tyqmrwlsc", "samsung9300duos"),
            new Account("93s66qbc0", "samsung9300duos"),
            new Account("arv9v0b5f", "samsung9300duos"),
            new Account("b0574ombz", "samsung9300duos"),
        };


        #endregion

        public void ScrollSelectedItem(object obj)
        {
            if(obj is ListBox listBox)
            {
                listBox.ScrollIntoView(SelectedAccount);
            }
        }
        public void SearchAccFromQuery(object obj)
        {
            if (obj is string query)
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    SelectedAccount = Accounts.FirstOrDefault(acc => acc.Login.StartsWith(query));
                }
                else
                {
                    SelectedAccount = null;
                }
            }
        }


        // Переписать команды тк написаны не читабельно без фоди проперти ченджед
        #region Commands     

        // команда для поиска по аккаунтам

        public RelayCommand SearchAccount
        {
            get
            {
                return new RelayCommand(SearchAccFromQuery);
            }
        }

        // скролл
        public RelayCommand ScrollSelectedItemCommand
        {
            get
            {
                return new RelayCommand(ScrollSelectedItem);
            }
        }



        // команда открытия нового окна
        public RelayCommand AddAccountOpenWindow
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    DialogService.AddAccountsWindow();
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
                          if (DialogService.SaveFileDialog() == true)
                          {
                              FileService.Save(DialogService.FilePath, Accounts.ToList());
                              DialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          DialogService.ShowMessage(ex.Message);
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
                          if (DialogService.OpenFileDialog() == true)
                          {
                              List<Account> accounts = FileService.Open<Account>(DialogService.FilePath);
                              Accounts.Clear();
                              foreach (var acc in accounts)
                              {
                                  Accounts.Add(acc);
                              }
                              DialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          DialogService.ShowMessage(ex.Message);
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
