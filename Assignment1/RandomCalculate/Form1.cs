using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace RandomCalculate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int a, b,score=0,timeLeft;
        string op;
        double result;
        Random rnd = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            
            a = rnd.Next(9) + 1;
            b = rnd.Next(9) + 1;
            int c = rnd.Next(4);
            switch (c)          //生成随机符号
            {
                case 0: op = "+"; result = a + b; break;
                case 1: op = "-"; result = a - b; break;
                case 2: op = "*"; result = a * b; break;
                case 3: op = "/"; result = (int)(((double)a / b) * 100) * 1.0 / 100; break; //保留两位小数
            }
            label1.Text = a.ToString();
            label2.Text = op;
            label3.Text = b.ToString();
            textBox1.Text = " ";      //输出随机题目
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            double d = double.Parse(str);
            string disp = " " + a + op + b + "=" + str + " ";
            if (d == result)
            {
                disp += " correct!";
                score++;
            }
            else
                disp += " false!";
            listBox1.Items.Add(disp+" score="+score);       //在list中输出
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private bool CheckTheAnswer()
        {
            if ((a+b==result)
                && (a-b == result)
                && (a * b == result)
                )
                return true;
            else
                return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                Enabled = true;
                button1_Click(sender, e);
            }
            else if (timeLeft > 0)
            {
                // If CheckTheAnswer() returns false, keep counting
                // down. Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft = timeLeft - 1;
                label4.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                label4.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                Enabled=true;
                button1_Click(sender, e);
            }
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
