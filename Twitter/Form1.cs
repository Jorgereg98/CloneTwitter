using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twitter.Builder;
using Twitter.DB;
using Twitter.Design_patterns.Observer;
using Twitter.Design_patterns.Proxy;
using Twitter.Models;
using Twitter.Strategy;

namespace Twitter
{
    public partial class Form1 : Form
    {
        private static UserService _service;
        Password password = new Password();
        User logged_user;
        List<int> following;
        List<int> followers;
        int siguiendo;
        int seguidores;
        bool b = false;
        bool c = false;
        List<CountryObject> lugares = new List<CountryObject>();

        private readonly ISubject _sensores;
        private IObserver _display;

        public Form1()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ToString();
            _service = new UserService(connectionString);

            _sensores = new MedidorCaracteres(textTweet.Text.Length);
            _display = new ObserverAlerta(_sensores);
            monitorear.Enabled = false;

            IProxyCountry proxy = new ProxyCountry();
            var proxyCountry = proxy.country();
            List<string> countries = new List<string>();

            foreach(var c in proxyCountry)
            {
                if (c.name != "")
                {
                    lugares.Add(c);
                    countries.Add(c.name);
                }
                
            }

            comboLugares.DataSource = new BindingSource { DataSource = countries };
            
            

        }

        public void Validate()
        {
            if (textBox1.Text == "") label5.Visible = true;
            else label5.Visible = false;

            if (textBox2.Text == "") label6.Visible = true;
            else label6.Visible = false;

            bool x = false;
            if (textBox3.Text == "")
            {
                label7.Visible = true;
                label7.Text = "* Campo obligatorio";
                x = false;
            }
            else
            {

                var val1 = new MailValidation(new AtValidation(), textBox3.Text);
                var result1 = val1.Verification();

                var val2 = new MailValidation(new DomainValidation(), textBox3.Text);
                var result2 = val2.Verification();

                if (result1 == true && result2 == true)
                {
                    x = true;

                    var result = _service.ExistMail(textBox3.Text);
                    if (result == "No existe")
                    {
                        label7.Visible = true;
                        label7.Text = "";
                        x = true;
                    }
                    else if (result == "Existe")
                    {
                        label7.Text = "Ya fue utilizado el correo";
                        x = false;
                    }

                }
                else
                {
                    label7.Visible = true;
                    label7.Text = "* Su correo no tiene el formato correcto";
                    x = false;
                }
            }

            bool y = false;
            if (textBox8.Text == "")
            {
                label11.Visible = true;
                label11.Text = "* Campo obligatorio";
                y = false;
            }
            else
            {
                
                var result = _service.ExistUsername(textBox8.Text);
                if (result == "No existe")
                {
                    label11.Visible = true;
                    label11.Text = "";
                    y = true;
                }
                else if (result == "Existe")
                {
                    label11.Visible = true;
                    label11.Text = "* Usuario ya usado, intente con otro.";
                    y = false;
                }
            }

            if (textBox4.Text == "")
            {
                label8.Visible = true;
                label8.Text = "* Campo obligatorio";
                label8.ForeColor = Color.Crimson;
            }

            if (textBox1.Text != "" &&
                textBox2.Text != "" &&
                x == true &&
                y == true &&
                label8.ForeColor != Color.Crimson) b = true;
            else b = false;

            return;
        }

        public void ValidateUpdate()
        {
            if (textBox12.Text == "") label23.Visible = true;
            else label23.Visible = false;

            if (textBox11.Text == "") label22.Visible = true;
            else label22.Visible = false;

            

            if (textBox9.Text == "")
            {
                label20.Visible = true;
                label20.Text = "* Campo obligatorio";
                label20.ForeColor = Color.Crimson;
            }

            if (textBox12.Text != "" &&
                textBox11.Text != "" &&
                label20.ForeColor != Color.Crimson) c = true;
            else c = false;

            return;
        }

        public void CargarTweets()
        {
            following = _service.GetFollowing(logged_user.id);
            List<Tweet> following_tweets = new List<Tweet>();
            for (int i = 0; i < following.Count; i++)
            {
                List<Tweet> user_tweets = _service.GetUserTweets(following[i]);
                for (int j = 0; j < user_tweets.Count; j++)
                {
                    following_tweets.Add(user_tweets[j]);
                }
            }

            List<Tweet> actual = _service.GetUserTweets(logged_user.id);
            for(int i = 0; i < actual.Count; i++)
            {
                following_tweets.Add(actual[i]);
            }

            following_tweets = following_tweets.OrderBy(t => t.date.Date).ToList();
            tweetsGrid.DataSource = new BindingSource { DataSource = following_tweets };

            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home.Visible = false;
            register.Visible = true;

            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label11.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home.Visible = true;
            register.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Validate();

            if (b)
            {
                var user = new User();

                
                user.name = textBox1.Text;
                user.lastName = textBox2.Text;
                user.mail = textBox3.Text;
                user.username = textBox8.Text;
                user.pasword = textBox4.Text;

                _service.AddUser(user);
                MessageBox.Show("Registrado");

                home.Visible = true;
                register.Visible = false;
            }
            else
            {
                MessageBox.Show("Alguno de tus campos no es válido.");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = true;
            if (textBox4.Text.Any(char.IsDigit) && textBox4.Text.Any(char.IsLower) && textBox4.Text.Any(char.IsUpper) && textBox4.Text.Length >= 12)
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Very_Strong)
                   .Finish();
                label8.ForeColor = Color.ForestGreen;
                label8.Text = password.ToString();
            }
            else if (textBox4.Text.Any(char.IsDigit) && textBox4.Text.Any(char.IsLower) && textBox4.Text.Any(char.IsUpper) && textBox4.Text.Length < 12 && textBox4.Text.Length >= 7)
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Strong)
                   .Finish();
                label8.ForeColor = Color.YellowGreen;
                label8.Text = password.ToString();
            }
            else if (textBox4.Text.Any(char.IsDigit) && textBox4.Text.Any(char.IsLower) && textBox4.Text.Any(char.IsUpper) && textBox4.Text.Length < 7)
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Regular)
                   .Finish();
                label8.ForeColor = Color.Gold;
                label8.Text = password.ToString();
            }
            else
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Weak)
                   .Finish();
                label8.ForeColor = Color.Crimson;
                label8.Text = password.ToString();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            log.Visible = false;
            register.Visible = true;

            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            home.Visible = false;
            log.Visible = true;
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            binicio.ForeColor = Color.DodgerBlue;
            bperfil.ForeColor = Color.Black;
            buscar.ForeColor = Color.Black;

            var result = _service.Exist(textBox5.Text, textBox5.Text);

            

            if (result == "No existe")
            {
                if(textBox5.Text == "")
                {

                }
                else
                {
                    textBox5.Text = "";
                    textBox6.Text = "";

                    MessageBox.Show("Cuenta no registrada, únete!");
                }

            }
            else if (result == "Existe")
            {
                result = _service.Verify(textBox5.Text, textBox5.Text, textBox6.Text);

                if (result == "Incorrecto")
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                    MessageBox.Show("Contraseña incorrecta!");
                }
                if (result == "Correcto")
                {
                    logged_user = _service.GetUser(textBox5.Text, textBox5.Text, textBox6.Text);
                    following = _service.GetFollowing(logged_user.id);
                    followers = _service.GetFollowers(logged_user.id);
                    siguiendo = following.Count;
                    seguidores = followers.Count;

                    textBox5.Text = "";
                    textBox6.Text = "";
                    MessageBox.Show("Bienvenido!");
                    start.Visible = true;
                    log.Visible = false;

                    //tweetsGrid.DataSource = new BindingSource { DataSource = _service.GetUserTweets(logged_user.id) };
                    CargarTweets();
                }

                pinicio.Visible = true;

                




            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            home.Visible = false;
            log.Visible = true;
            textBox5.Text = "";
            textBox6.Text = "";

        }

        private void button7_Click(object sender, EventArgs e)
        {

            var tweet = new Tweet();
            tweet.text = textTweet.Text;
            tweet.date = DateTime.Now;
            tweet.likes = 0;
            tweet.idUser = logged_user.id;


            var result = _service.AddTweet(tweet);
            if(result == "Tweet Add Successfully")
            {
                MessageBox.Show("Tweet Add Successfully");
            }
            else if(result == "Error Adding Tweet")
            {
                MessageBox.Show("Error Adding Tweet");
            }

            textTweet.Text = "";
            //aparecen los del usuario faltan poner los de los seguidores
            CargarTweets();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            logged_user = null;
            start.Visible = false;
            home.Visible = true;


            pinicio.Visible = false;
            pperfil.Visible = false;
            pbuscar.Visible = false;
            peditar.Visible = false;

            encontrados.DataSource = new BindingSource { DataSource = null };
            usuarioBuscar.Text = "";
        }

        private void bperfil_Click(object sender, EventArgs e)
        {
            pinicio.Visible = false;
            pperfil.Visible = true;
            pbuscar.Visible = false;
            peditar.Visible = false;

            binicio.ForeColor = Color.Black;
            bperfil.ForeColor = Color.DodgerBlue;
            buscar.ForeColor = Color.Black;

            nombre.Text = logged_user.name;
            apellido.Text = logged_user.lastName;
            username.Text = "@" + logged_user.username;

            mytweets.DataSource = new BindingSource { DataSource = _service.GetUserTweets(logged_user.id) };

            label17.Text = siguiendo.ToString();
            label16.Text = seguidores.ToString();

            encontrados.DataSource = new BindingSource { DataSource = null };
            usuarioBuscar.Text = "";
        }

        private void binicio_Click(object sender, EventArgs e)
        {
            pinicio.Visible = true;
            pperfil.Visible = false;
            pbuscar.Visible = false;
            peditar.Visible = false;

            binicio.ForeColor = Color.DodgerBlue;
            bperfil.ForeColor = Color.Black;
            buscar.ForeColor = Color.Black;

            CargarTweets();


            encontrados.DataSource = new BindingSource { DataSource = null };
            usuarioBuscar.Text = "";
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            pinicio.Visible = false;
            pperfil.Visible = false;
            pbuscar.Visible = true;
            peditar.Visible = false;

            binicio.ForeColor = Color.Black;
            bperfil.ForeColor = Color.Black;
            buscar.ForeColor = Color.DodgerBlue;
        }

        private void buscarusuario_Click(object sender, EventArgs e)
        {
            encontrados.DataSource = new BindingSource { DataSource = _service.Search(logged_user.id, usuarioBuscar.Text)};
            //DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            //col.HeaderText = "Follow";
            //col.Name = "follow_user";
            //col.Text = "Follow";
            //col.UseColumnTextForButtonValue = true;
            //encontrados.Columns.Add(col);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            home.Visible = true;
            log.Visible = false;
        }

        private void textTweet_TextChanged(object sender, EventArgs e)
        {
            ((MedidorCaracteres)_sensores).NumeroCaracteres = textTweet.Text.Length;

            if(textTweet.Text.Length <= 280)
            {
                twittear.Enabled = true;
            }
            else if (ObserverAlerta.color == "rojo")
            {
                twittear.Enabled = false;
            }
            else if (ObserverAlerta.color == "verde")
            {
                twittear.Enabled = true;

                //if (textTweet.Text.Length > 280)
                //{
                //    twittear.Enabled = false;
                //}
                //else
                //{
                //    twittear.Enabled = true;
                //}
            }
            
            
        }

        private void NoMonitorear_Click(object sender, EventArgs e)
        {
            _sensores.EliminarObserver(_display);
            monitorear.Enabled = true;
            NoMonitorear.Enabled = false;
        }

        private void monitorear_Click(object sender, EventArgs e)
        {
            _display = new ObserverAlerta(_sensores);
            monitorear.Enabled = false;
            NoMonitorear.Enabled = true;
        }

        private void Seguir_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = encontrados.CurrentCell.RowIndex;
                var result = _service.AddFollower(logged_user.id, (int)encontrados.Rows[rowindex].Cells[0].Value);

                if (result == "Follower Add Successfully")
                {
                    MessageBox.Show("Ahora sigues a " + encontrados.Rows[rowindex].Cells[1].Value.ToString() + "!");
                }
                if (result == "Error Adding Follower")
                {
                    MessageBox.Show("Ya seguias a " + encontrados.Rows[rowindex].Cells[1].Value.ToString() + " anteriormente!");
                }

                following = _service.GetFollowing(logged_user.id);
                siguiendo = following.Count;
                followers = _service.GetFollowers(logged_user.id);
                seguidores = followers.Count;
            }
            catch
            {

            }
            

        }

        private void DejarSeguir_Click(object sender, EventArgs e)
        {
            try
            {
                int rowindex = encontrados.CurrentCell.RowIndex;

                var result = _service.DeleteFollower(logged_user.id, (int)encontrados.Rows[rowindex].Cells[0].Value);

                if (result == "Follower Removed Successfully")
                {
                    MessageBox.Show("Dejaste de seguir a " + encontrados.Rows[rowindex].Cells[1].Value.ToString() + "!");
                }
                if (result == "Error Removing Follower")
                {
                    MessageBox.Show("Aun no sigues a " + encontrados.Rows[rowindex].Cells[1].Value.ToString() + "!");
                }

                following = _service.GetFollowing(logged_user.id);
                siguiendo = following.Count;
                followers = _service.GetFollowers(logged_user.id);
                seguidores = followers.Count;
            }
            catch
            {

            }
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            ValidateUpdate();

            if (c)
            {
                var user = new User();


                user.name = textBox12.Text;
                user.lastName = textBox11.Text;
                user.mail = logged_user.mail;
                user.username = logged_user.username;
                user.pasword = textBox9.Text;

                var result = _service.UpdateUser(user.name,user.lastName,user.pasword,user.mail, user.username);

                if (result == "User Updated Successfully")
                {
                    MessageBox.Show("Datos actualizados correctamente!");
                    logged_user = _service.GetUser(logged_user.mail, logged_user.username, textBox9.Text);
                }
                if (result == "Error Updating User")
                {
                    MessageBox.Show("No se actualizaron los datos.");
                }

            }
            else
            {
                MessageBox.Show("Alguno de tus campos no es válido.");
            }

            peditar.Visible = false;
            textBox11.Text = "";
            textBox12.Text = "";
            textBox9.Text = "";
            nombre.Text = logged_user.name;
            apellido.Text = logged_user.lastName;
            
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            label20.Visible = true;
            if (textBox9.Text.Any(char.IsDigit) && textBox9.Text.Any(char.IsLower) && textBox9.Text.Any(char.IsUpper) && textBox9.Text.Length >= 12)
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Very_Strong)
                   .Finish();
                label20.ForeColor = Color.ForestGreen;
                label20.Text = password.ToString();
            }
            else if (textBox9.Text.Any(char.IsDigit) && textBox9.Text.Any(char.IsLower) && textBox9.Text.Any(char.IsUpper) && textBox9.Text.Length < 12 && textBox9.Text.Length >= 7)
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Strong)
                   .Finish();
                label20.ForeColor = Color.YellowGreen;
                label20.Text = password.ToString();
            }
            else if (textBox9.Text.Any(char.IsDigit) && textBox9.Text.Any(char.IsLower) && textBox9.Text.Any(char.IsUpper) && textBox9.Text.Length < 7)
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Regular)
                   .Finish();
                label20.ForeColor = Color.Gold;
                label20.Text = password.ToString();
            }
            else
            {
                password = PasswordFluentBuilder.Create(DifficultyEnum.Weak)
                   .Finish();
                label20.ForeColor = Color.Crimson;
                label20.Text = password.ToString();
            }
        }

        private void editar_Click(object sender, EventArgs e)
        {
            peditar.Visible = true;
        }

        private void label20_TextChanged(object sender, EventArgs e)
        {

        }

        private void info_Click(object sender, EventArgs e)
        {
            CountryObject c = lugares[comboLugares.SelectedIndex];
            trends.Visible = true;
            trends.Text = "Tendencias de " + c.name;
        }
    }
}
