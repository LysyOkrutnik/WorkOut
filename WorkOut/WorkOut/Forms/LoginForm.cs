using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkOut.Helpers;
using WorkOut.Models;

namespace WorkOut.Forms
{
    public partial class LoginForm : Form
    {
        private List<User> users;

        public LoginForm()
        {
            InitializeComponent();
            users = JsonHandler.LoadUsers();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            foreach (var user in users)
            {
                if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password)
                {
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces");
                    return;
                }
            }

            MessageBox.Show("Nieprawidłowe dane logowania.", "Błąd");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Otwórz nowe okno rejestracji i zamknij bieżące
            RegisterForm registerForm = new RegisterForm();
            registerForm.StartPosition = this.StartPosition; // Ustaw pozycję nowego okna
            registerForm.Show();
            this.Hide(); // Ukryj bieżący formularz
            registerForm.FormClosed += (s, args) => this.Close(); // Zamknij bieżący formularz, gdy zamknięte zostanie nowe okno
        }
    }
}