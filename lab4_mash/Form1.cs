using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_mash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap image;
        Color RGB = new Color();
        string filePath;

        private void button1_Click(object sender, EventArgs e)
        {
            st.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox3.Clear();
            int ind = 0;
            byte[] Coding = Encoding.GetEncoding(1251).GetBytes(textBox5.Text + '#');
            foreach (byte i in Coding)
                textBox4.Text += i.ToString() + " ";
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    RGB = image.GetPixel(j, i);
                    image.SetPixel(j, i, WordBulder.ByteToColor(RGB, Coding[ind]));
                    if (Coding[ind] == 35)
                        break;
                    ind++;
                }
            }
            ind = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    image = new Bitmap(filePath);
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                    pictureBox1.Image = image;
                }
                else
                {
                    MessageBox.Show("Error open file, choise bmp file","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(dialog.FileName,ImageFormat.Bmp);
            }
        }
        public delegate void StateHandler();
        public StateHandler st;
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1 == sender)
            {
                st = Normal;
            }
            if(radioButton2 == sender)
            {
                st = Diagonal;
            }
            if(radioButton3 == sender)
            {
                st = OnebyOne;
            }

        }

        private void Normal()
        {
            byte[] answ = new byte[1];
            textBox4.Clear();
            textBox3.Clear();
            for (int i = 0; i < image.Height; i++)
            {
                bool stop = false;
                for (int j = 0; j < image.Width; j++)
                {
                    char a;
                    RGB = image.GetPixel(j, i);
                    answ[answ.Length - 1] = WordBulder.RGBinSTR(RGB, out a);
                    Array.Resize(ref answ, answ.Length + 1);
                    if (a == '#')
                    {
                        stop = true;
                        break;
                    }
                }
                if (stop)
                {
                    break;
                }

            }
            foreach (byte i in answ)
                textBox4.Text += i.ToString() + " ";
            textBox3.Text = Encoding.GetEncoding(1251).GetString(answ);
        }
        private void Diagonal()
        {
            byte[] answ = new byte[1];
            textBox4.Clear();
            textBox3.Clear();
            try
            {
                for (int i = 0; i < image.Width; i++)
                {
                    char a;
                    RGB = image.GetPixel(i, i);
                    answ[answ.Length - 1] = WordBulder.RGBinSTR(RGB, out a);
                    Array.Resize(ref answ, answ.Length + 1);
                    if (a == '#')
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error open file, choise bmp file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (byte i in answ)
                textBox4.Text += i.ToString() + " ";
            textBox3.Text = Encoding.GetEncoding(1251).GetString(answ);
        }
        private void OnebyOne()
        {
            byte[] answ = new byte[1];
            textBox4.Clear();
            textBox3.Clear();
            for (int i = 0; i < image.Height; i=i+2)
            {
                bool stop = false;
                for (int j = 0; j < image.Width; j=j+2)
                {
                    char a;
                    RGB = image.GetPixel(j, i);
                    answ[answ.Length - 1] = WordBulder.RGBinSTR(RGB, out a);
                    Array.Resize(ref answ, answ.Length + 1);
                    if (a == '#')
                    {
                        stop = true;
                        break;
                    }
                }
                if (stop)
                {
                    break;
                }

            }
            foreach (byte i in answ)
                textBox4.Text += i.ToString() + " ";
            textBox3.Text = Encoding.GetEncoding(1251).GetString(answ);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            st = Normal;
        }
    }
}
