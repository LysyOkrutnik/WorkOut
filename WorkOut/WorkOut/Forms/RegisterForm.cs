using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkOut.Helpers;
using WorkOut.Models;

namespace WorkOut.Forms
{
    public partial class RegisterForm : Form
    {
        private List<User> users;

        public RegisterForm()
        {
            InitializeComponent();
            users = JsonHandler.LoadUsers();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Nazwa użytkownika nie może być pusta!", "Błąd");
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Hasło musi mieć co najmniej 8 znaków, w tym dużą literę, małą literę, cyfrę i znak specjalny.", "Błąd");
                return;
            }

            foreach (var user in users)
            {
                if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Użytkownik o takiej nazwie już istnieje!", "Błąd");
                    return;
                }
            }

            users.Add(new User(username, password));
            JsonHandler.SaveUsers(users);

            MessageBox.Show("Zarejestrowano pomyślnie!", "Sukces");
            this.Close();
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            // Otwórz nowe okno logowania i zamknij bieżące
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = this.StartPosition; // Ustaw pozycję nowego okna
            loginForm.Show();
            this.Hide(); // Ukryj bieżący formularz
            loginForm.FormClosed += (s, args) => this.Close(); // Zamknij bieżący formularz, gdy zamknięte zostanie nowe okno
        }

        private bool IsValidPassword(string password)
        {
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }
    }
}