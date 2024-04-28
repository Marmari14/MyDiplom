using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vers0.Model;

namespace Vers0.ViewModel
{
    public class AuthorizationViewModel: INotifyPropertyChanged
    {
        inventory_managementEntities db = new inventory_managementEntities();
        AuthorizationWindow authorizationWindow;

        private users user;
        public users User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User)); // Метод для уведомления об изменении свойства
            }
        }

        public ObservableCollection<contractor> contractors { get; set; }
        public ObservableCollection<product> products { get; set; }
        public ObservableCollection<products_in_order> products_In_Orders { get; set; }



        public AuthorizationViewModel(AuthorizationWindow m)
        {
            authorizationWindow = m;
        }
        //public AuthorizationViewModel() {}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand logInCommand;
        public RelayCommand LogInCommand
        {
            get
            {
                return logInCommand ??
                  (logInCommand = new RelayCommand(obj =>
                  {
                      // Выполнение функции из базы данных
                      if (db.UserAuthorization(authorizationWindow.login.Text, authorizationWindow.password.Password))
                      {
                          User = db.users.Find(authorizationWindow.login.Text);                          
                          authorizationWindow.Close();
                      }
                      else
                      {
                          MessageBox.Show("Данные введены неверно!");
                          authorizationWindow.login.Text = "";
                          authorizationWindow.password.Password = "";
                      }

                  },
                  (obj) => !string.IsNullOrEmpty(authorizationWindow.login.Text) && !string.IsNullOrEmpty(authorizationWindow.password.Password)));
            }
        }

        private RelayCommand exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                  (exitCommand = new RelayCommand(obj =>
                  {
                      Application.Current.Shutdown(); // Закрыть текущее приложение
                      System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                  }));
            }
        }
    }
}
