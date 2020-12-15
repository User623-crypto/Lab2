using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private double rezultati = 0;
        private double vleraFillestare = 0;
        
        private String vleraTekst = "";
        private String assignedOperator = "";
        private bool finishedEv = false;
        private bool presjaUShtyp = false;
        private bool gershera = false;
        public Form1()
        {
            InitializeComponent();
        }

        private double calculate(String s,double a,double b)
        {
            switch (s)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "X":
                    return a * b;
                case "÷":
                    return a / b;
               
            }
            return 0.0;
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numberButtonPushed(object sender, EventArgs e)
        {
            Button pushed = (Button)sender;
            if (pushed.Text == "0" && vleraTekst == "0")
                return;

            if (!finishedEv)
            {
                vleraTekst += pushed.Text;
                
            }
            else {
                finishedEv = false;
                vleraTekst = pushed.Text;
                }
            
            display.Text = vleraTekst;

            vleraFillestare = Double.Parse(vleraTekst);


        }

        private void specialButtonPushed(object sender, EventArgs e)
        {
            if (vleraTekst == "")
                return;
            Button pushed = (Button)sender;

            if (pushed.Text == "±")
            {
                Console.WriteLine("" + Double.Parse(vleraTekst));
                if (Double.Parse(vleraTekst) > 0)
                    vleraTekst = "-" + vleraTekst;
                else
                {
                    Console.WriteLine("Ekzekutohet");

                    vleraTekst = vleraTekst.Remove(0,1);
                }
            }

            if(pushed.Text == "✂" && !gershera)
            {
                
                Char iFundit = vleraTekst.ElementAt(vleraTekst.Length - 1);
                vleraTekst = vleraTekst.Remove(vleraTekst.Length - 1, 1);
                if (vleraTekst == "-")
                    vleraTekst = "";
                if (iFundit == '.')
                    presjaUShtyp = false;

            }

            if(pushed.Text == ".")
            {
                if (presjaUShtyp)
                    return;
                vleraTekst += ".";
                presjaUShtyp = true;
            }

            if (vleraTekst != "")
                display.Text = vleraTekst;
            else
                display.Text = "0";
           
        }

        private void operatorButtonPushed(object sender,EventArgs e)
        {
            if(vleraTekst == "" && assignedOperator !="")
            {
                assignedOperator = ((Button)sender).Text;
                opDisplay.Text = opDisplay.Text.Remove(opDisplay.Text.Length-1);
                opDisplay.Text += assignedOperator;
                return;
            }
            try
            {
                vleraFillestare = Double.Parse(vleraTekst);
            }catch(Exception err)
            {
                vleraFillestare = 0;
            }
            if (assignedOperator == "")
            { rezultati = vleraFillestare; }
            else
            {
                rezultati = calculate(assignedOperator, rezultati, vleraFillestare);

            }
            opDisplay.Text += "" + vleraFillestare;
            display.Text =""+ rezultati;
            clearVleraTekst();
            assignedOperator = ((Button)sender).Text;
            opDisplay.Text += assignedOperator;
            gershera = false;


        }
        private void clearVleraTekst()
        {
            vleraTekst = "";
            presjaUShtyp = false;
            vleraFillestare = 0;
        }
        private void reset(object sender,EventArgs e)
        {
            vleraTekst = "";
            vleraFillestare = 0;
            display.Text = "0";
            opDisplay.Text = "";
            rezultati = 0;
            presjaUShtyp = false;
            assignedOperator = "";
        }

        private void claculateTotal(object sender,EventArgs e)
        {
            Console.WriteLine("Logohet kjo");
            if (assignedOperator == "") return;

            if (vleraTekst != "")
            { rezultati = calculate(assignedOperator, rezultati, Double.Parse(vleraTekst));
                opDisplay.Text += vleraTekst;
            }
            

            display.Text = "" + rezultati;
            clearVleraTekst();
            memoryText.AppendText(opDisplay.Text);
            memoryText.AppendText(Environment.NewLine);
            memoryText.AppendText(rezultati+"");
            memoryText.AppendText(Environment.NewLine); memoryText.AppendText(Environment.NewLine);

            opDisplay.Text = "";
            
            vleraFillestare = rezultati;
            vleraTekst = "" + vleraFillestare;
            rezultati = 0;
            finishedEv = true;
            presjaUShtyp = false;
            assignedOperator = "";
        }

        private void selfContainedOperator(object sender,EventArgs e)
        {
            KeyValuePair<Double, String> map ;
            String selfContained = ((Button)sender).Text;
            if(vleraTekst == "" )
            {

                map = calculateContained(selfContained, rezultati);
                display.Text = "" + rezultati;
                opDisplay.Text = map.Value+"(" + opDisplay.Text + ") ";

            }
            else
            {
                vleraFillestare = Double.Parse(vleraTekst);
                map = calculateContained(selfContained, vleraFillestare);
                vleraFillestare = map.Key;
                Console.WriteLine(vleraFillestare);
                opDisplay.Text += map.Value+"(" + vleraTekst + ") ";
                vleraTekst = vleraFillestare + "";
                display.Text = vleraTekst;
                gershera = true;
                
            }
        }

        private KeyValuePair<Double,String> calculateContained(String s,double rez)
        {
            
            if(s == "x²")
            {
                Console.WriteLine("Ekzekutohet");
                rez = rez * rez;
                return new KeyValuePair<Double,String>(rez, "srq");
            }else if(s == "1/x")
            {
                rez = 1 / rez;
                return new KeyValuePair<Double, String>(rez, "1");

            }
            else if (s == "√x")
            {
                rez = Math.Sqrt(rez);
                return new KeyValuePair<Double, String>(rez, "√");

            }
            return new KeyValuePair<Double, String>(rez, "");



        }
    }
}
