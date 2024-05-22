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
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        inventory_managementEntities db = new inventory_managementEntities();
        AuthorizationWindow authorizationWindow;

        public bool authorizationAccess { get; private set; } = false;

        private users user;
        public users User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public AuthorizationViewModel(AuthorizationWindow m)
        {
            authorizationWindow = m;
        }

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
                      if (db.UserAuthorization(authorizationWindow.login.Text, authorizationWindow.password.Password))
                      {
                          User = db.users.Find(authorizationWindow.login.Text);
                          authorizationAccess = true;
                          authorizationWindow.Close();
                      }
                      else
                      {
                          MessageBox.Show("Данные введены неверно!", "Вход запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                      Application.Current.Shutdown();
                      System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                  }));
            }
        }
    }
}
