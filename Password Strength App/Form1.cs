namespace Password_Strength_App
{
    public partial class Form1 : Form
    {
        PasswordValidator PasswordValidator = new PasswordValidator();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty) return;
            string message = string.Empty;
            if (PasswordValidator.IsStrong(textBox1.Text, out message))
            {
                if (string.IsNullOrEmpty(message)) 
                {
                    msgLabel.Text = "Strong Password";
                }
                else
                {
                    msgLabel.Text = message;
                }
            }
            else
            {
                msgLabel.Text = message;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
            }
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult about;
            about = MessageBox.Show("This program was developped by: Didy and Carissa " + " " + "\n" +
                                    "Date: 5 May 2023", "Password Strength Analyzer ");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult exit;
            exit = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void msgLabel_Click(object sender, EventArgs e)
        {

        }
    }
}