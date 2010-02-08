using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CENEX.GENERAL.MATH;

namespace CENEX.MODULES._03_STEEL.EC3
{
    public partial class EN1993_1_8Form_PIN : Form
    {



        double d_Sigma_h_Ed;
        double d_E;
        double d_F_Ed;
        double d_F_Ed_ser;
        // the diameter of the pin;
        double d_d;         // outside diameter
        double d_d_in;      // inside diameter
        double d_d_0;
        
        // the lower of the design strengths of the pin and the connected part;
        double d_f_y;
        double d_f_u;
        double d_f_h_Ed;



        double d_F_v_Rd;
        double d_F_b_Rd;
        double d_F_b_Rd_ser;
        double d_M_Rd;
        double d_M_Rd_ser;
        // the yield strength of the pin;
        double d_f_yp;
        // the ultimate tensile strength of the pin
        double d_f_up;
        double d_gamma_M0;
        double d_gamma_M1;
        double d_gamma_M2;

        double d_gamma_M6_ser;
        // the cross-sectional area of a pin.
        double d_A;

        // the thickness of the connected part;
        double d_t_11;
        double d_t_12;
        double d_t_21;
        double d_t_22;
        double d_t_23;


        double d_W_el;

        double d_F_v_Ed;
        double d_F_b_Ed;
        double d_F_b_Ed_ser;
        double d_M_Ed;
        double d_M_Ed_ser;

        double d_ta;
        double d_tb;
        double d_tc;

        double d_t_min;
        double d_t_max;


        double d_t_1_min;
        double d_t_1_max;

        double d_t_2_min;
        double d_t_2_max;


        double d_ratio_pin;



    







        public EN1993_1_8Form_PIN()
        {
            InitializeComponent();
        }









 // Array of Steel properties


string [] steel_grades  = {
"S 235",       
"S 275",
"S 355",
"S 450",
"S 275 N/NL",
"S 355 N/NL",
"S 420 N/NL",
"S 460 N/NL",
"S 275 M/ML",
"S 355 M/ML",
"S 420 M/ML",
"S 460 M/ML",
"S 235 W",
"S 355 W",
"S 460 Q/QL/QL1",
"30CrNiMo8v"
    };

double[,] steel_properties = {
{235,	360,	215,	360,	1.00,	1.00,	1.25,	0.80,	1.2,	210000,	80769,	0.3,	1.20E-05},
{275,	430,	255,	410,	1.00,	1.00,	1.25,	0.85,	1.2,	210000,	80769,	0.3,	1.20E-05},
{355,	510,	335,	470,	1.00,	1.00,	1.25,	0.90,	1.2,	210000,	80769,	0.3,	1.20E-05},
{440,	550,	410,	550,	1.00,	1.00,	1.25,	1.00,	1.2,	210000,	80769,	0.3,	1.20E-05},
{275,	390,	255,	370,	1.00,	1.00,	1.25,	0.85,	1.2,	210000,	80769,	0.3,	1.20E-05},
{355,	490,	335,	470,	1.00,	1.00,	1.25,	0.90,	1.2,	210000,	80769,	0.3,	1.20E-05},
{420,	520,    390,    520,	1.00,	1.00,	1.25,	1.00,	1.2,	210000,	80769,	0.3,    1.20E-05},
{460,	540,    430,    540,	1.00,	1.00,	1.25,	1.00,	1.2,	210000,	80769,	0.3,	1.20E-05},
{375,	370,	255,    360,	1.00,	1.00,	1.25,	0.85,	1.2,	210000,	80769,	0.3,	1.20E-05},
{355,	470,	335,	450,	1.00,	1.00,	1.25,	0.90,	1.2,	210000,	80769,	0.3,	1.20E-05},
{420,	520,	390,	500,	1.00,	1.00,	1.25,	1.00,	1.2,	210000,	80769,	0.3,	1.20E-05},
{460,	540,	430,	530,	1.00,	1.00,	1.25,	1.00,	1.2,	210000,	80769,	0.3,	1.20E-05},
{235,	360,	215,	340,	1.00,	1.00,	1.25,	0.80,	1.2,	210000,	80769,	0.3,	1.20E-05},
{355,	510,	335,	490,	1.00,	1.00,	1.25,	0.90,	1.2,	210000,	80769,	0.3,	1.20E-05},
{460,	570,	440,	550,	1.00,	1.00,	1.25,	1.00,	1.2,	210000,	80769,	0.3,	1.20E-05},
// High strength steel  - pin
{900,	1000,	900,	1000,	1.00,	1.00,	1.25,	1.00,	1.2,	205000,	80769,	0.3,	1.20E-05},
        };

        





       

        // Metoda - Load data from textboxes
        // Tato metoda nacita udaje z textboxov a skonvertuje na cislo
        public void Load_data()
        {

            try
            {
                d_d = Convert.ToDouble(d_d_textB.Text.ToString());
            }
            catch
            {
                MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
            }

            try
            {
                d_d_0 = Convert.ToDouble(d_d0_textB.Text.ToString());
            }
            catch
            {
                MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
            }














        }


        public void Load_data2_Steel()
        {
       // Material Plates
            int mat1_id = comboBox_Steel_PLATE.SelectedIndex;
            d_f_y = steel_properties[mat1_id, 0];
            d_f_u = steel_properties[mat1_id, 1];


        // Material Pin
            int mat2_id = comboBox_Steel_PIN.SelectedIndex;

            d_f_yp = steel_properties [mat2_id, 0];
            d_f_up = steel_properties [mat2_id, 1];

        }







        // Metoda - Nastaví vypocitane hodnoty v textboxoch
        public void Set_data()
        {

            // Nastavia sa vypocitane hodnoty (skonvetovane z double na string)

            d_A_textB.Text = d_A.ToString();
            d_Wel_textB.Text = d_W_el.ToString();
            

        }



























// Main method
        public void EN1993_1_8_Main ()
{


    d_t_min = MathF.Min (d_t_11, d_t_12,d_t_21, d_t_22, d_t_23);
    d_t_max = MathF.Max (d_t_11, d_t_12,d_t_21, d_t_22, d_t_23);


    d_t_1_min = MathF.Min(d_t_11, d_t_12);
    d_t_1_max = MathF.Max(d_t_11, d_t_12);

    d_t_2_min = MathF.Min(d_t_21, d_t_22, d_t_23);
    d_t_2_max = MathF.Min(d_t_21, d_t_22, d_t_23);



// Pin area
    d_A = Math.PI * (Math.Pow(d_d / 2, 2) - Math.Pow(d_d_in / 2, 2));


    // Table 3.10 Design criteria for pin connections
    // Shear resistance of the pin
    d_F_v_Rd = 0.6 * d_A * d_f_up / d_gamma_M2;
    // Bearing resistance of the plate and the pin
    d_F_b_Rd = 1.5 * d_t_min * d_d * d_f_y / d_gamma_M0;
    // Bearing resistance of the plate and the pin
    // If the pin is intended to be replaceable this requirement should also be satisfied.
    d_F_b_Rd_ser = 0.6 * d_t_min * d_d * d_f_y / d_gamma_M6_ser;
    // Bending resistance of the pin
    d_M_Rd = 1.5 * d_W_el * d_f_yp / d_gamma_M0;
    // Bending resistance of the pin
    // If the pin is intended to be replaceable this requirement should also be satisfied.
    d_M_Rd_ser = 0.8 * d_W_el * d_f_yp / d_gamma_M6_ser;
    // Combined shear and bending resistance of the pin
    d_ratio_pin = (Math.Pow(d_M_Ed / d_M_Rd, 2) + Math.Pow(d_F_v_Ed / d_F_v_Rd, 2)) / 1;

    // Figure 3.11: Bending moment in a pin
    double d_M_Ed_1 = d_Calc_M_Ed(d_t_21, d_t_11, d_tc, 0.5 * d_F_Ed);
    double d_M_Ed_2 = d_Calc_M_Ed(d_t_12, d_t_22, d_tc, 0.5 * d_F_Ed);
    d_M_Ed = MathF.Min(d_M_Ed_1, d_M_Ed_2);





// (3) If the pin is intended to be replaceable, in addition to the provisions given in 3.13.1 to 3.13.2, the contact bearing stress should satisfy







}

        double d_Calc_M_Ed(double d_a, double d_b, double d_c, double d_F)
        {
            // Figure 3.11: Bending moment in a pin
            double d_M = d_F / 8 * (d_b + 4 * d_c + 2 * d_a);
            return d_M;
        }


        // Metoda ktora sa spusti po stlaceni tlacidla calculate
        private void Calculate_Click_1(object sender, EventArgs e)
        {

                // Načítanie dat
            this.Load_data();
            // Vypocet vysledkov
            this.EN1993_1_8_Main();
            // MessageBox.Show("Vysledky v EN 1992_1_1 Form \n " + (" A = " + D_A + " mm2 \n Iy = " + D_I_y + " mm4 \n Iz = " + D_I_z + " mm4"));
            // zapísanie výsledkov do READONLY textboxov
            this.Set_data();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }











    }
}
