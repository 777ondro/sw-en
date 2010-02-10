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

        double d_t_c;

        double d_t_min;
        double d_t_max;
        
        double d_t_1_min;
        double d_t_1_max;

        double d_t_2_min;
        double d_t_2_max;

        double d_t_1;
        double d_t_2;
        
        bool b_index_REPLACE;
        bool b_index_SOLID;
        bool b_index_PLATES32;
        bool b_index_PLATES21;



        // Schemes


        // Scheme 1
        double d_a_p1;
        double d_c_p1;

        // Scheme 2
        double d_t_p2;
        double d_d0_p2;

        double d_03d0_p2;
        double d_075d0_p2;
        double d_1d0_p2;
        double d_13d0_p2;
        double d_16d0_p2;
        double d_25d0_p2;


        // Check ratios
        double d_ratio_1;
        double d_ratio_2;
        double d_ratio_3;
        double d_ratio_4;
        double d_ratio_5;
        double d_ratio_6;

        double d_ratio_7_pin;

        // kN to N
        // KILO
        int i_ratio_kilo = 1000;
        // MPa to Pa
        // MEGA
        double d_ratio_mega = 0.000001;
        // mm to m
        double d_ratio_mili = 0.001;
        // Percent
        double d_ratio_percent = 0.01;


        public EN1993_1_8Form_PIN()
        {
            InitializeComponent();
            // Load steel grades into comboboxes
            for (int i = 0; i < steel_grades.Length; i++)
            {
                this.comboBox_Steel_PIN.Items.Add(steel_grades[i]);
                this.comboBox_Steel_PLATE.Items.Add(steel_grades[i]);
            }

            // Set default values in dialog
            this.Set_data_default();
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


public void Set_data_default ()
{
    b_index_REPLACE = false;
    b_index_SOLID = true;
    b_index_PLATES32 = true;
    b_index_PLATES21 = false;

    d_d_textB.Text = Convert.ToString(100);
    d_d0_textB.Text = Convert.ToString(105);
    d_din_textB.Text = Convert.ToString(40);

    d_t11_textB.Text = Convert.ToString(50);
    d_t12_textB.Text = Convert.ToString(50);

    d_t21_textB.Text = Convert.ToString(25);
    d_t22_textB.Text = Convert.ToString(50);
    d_t23_textB.Text = Convert.ToString(25);

    d_tc_textB.Text = Convert.ToString(3);

    d_FEd_textB.Text = Convert.ToString(2500);
    d_FEd_ser_textB.Text = Convert.ToString(2000);

    comboBox_Steel_PLATE.Text = "S 355";
    comboBox_Steel_PLATE.SelectedIndex = 2;

    comboBox_Steel_PIN.Text = "30CrNiMo8v";
    comboBox_Steel_PIN.SelectedIndex = 15;

}

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
    try
    {
        d_d_in = Convert.ToDouble(d_din_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }

    try
    {
        d_t_11 = Convert.ToDouble(d_t11_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_t_12 = Convert.ToDouble(d_t12_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_t_21 = Convert.ToDouble(d_t21_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_t_22 = Convert.ToDouble(d_t22_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_t_23 = Convert.ToDouble(d_t23_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_t_c = Convert.ToDouble(d_tc_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_F_Ed = Convert.ToDouble(d_FEd_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
    try
    {
        d_F_Ed_ser = Convert.ToDouble(d_FEd_ser_textB.Text.ToString());
    }
    catch
    {
        MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
    }
}
// Metoda načítava údaje o oceli z poľa
public void Load_data2_Steel()
{
    // Material Plates
    int mat1_id = comboBox_Steel_PLATE.SelectedIndex;
    d_f_y = steel_properties[mat1_id, 0];
    d_f_u = steel_properties[mat1_id, 1];

    d_gamma_M0 = steel_properties[mat1_id, 4];
    d_gamma_M1 = steel_properties[mat1_id, 5];
    d_gamma_M2 = steel_properties[mat1_id, 6];

    d_gamma_M6_ser = 1.0; // !!! constant

    // Material Pin
    int mat2_id = comboBox_Steel_PIN.SelectedIndex;

    d_f_yp = steel_properties[mat2_id, 0];
    d_f_up = steel_properties[mat2_id, 1];


    // Minimum yield strength  ( pin and plates)
    d_f_y = Math.Min(d_f_y, d_f_yp);


    // Young modulus

    d_E = steel_properties[mat2_id, 9]; // for pin ???
}
// Metoda meni jednotky načítaných dát na SI sústavu
public void Convert_data_units()
{


    // Dimensions

    d_d *= d_ratio_mili;
    d_d_0 *= d_ratio_mili;
    d_d_in *= d_ratio_mili;
    d_t_11 *= d_ratio_mili;
    d_t_12 *= d_ratio_mili;
    d_t_21 *= d_ratio_mili;
    d_t_22 *= d_ratio_mili;
    d_t_23 *= d_ratio_mili;
    d_t_c *= d_ratio_mili;

    // Calculation 
    d_t_min = MathF.Min(d_t_11, d_t_12, d_t_21, d_t_22, d_t_23);
    d_t_max = MathF.Max(d_t_11, d_t_12, d_t_21, d_t_22, d_t_23);


    d_t_1_min = MathF.Min(d_t_11, d_t_12);
    d_t_1_max = MathF.Max(d_t_11, d_t_12);

    d_t_2_min = MathF.Min(d_t_21, d_t_22, d_t_23);
    d_t_2_max = MathF.Min(d_t_21, d_t_22, d_t_23);

    d_t_1 = d_t_11 + d_t_12;
    d_t_2 = d_t_21 + d_t_22 + d_t_23;

    // Solved properties

    // Pin area
    d_A = Math.PI * (Math.Pow(d_d / 2, 2) - Math.Pow(d_d_in / 2, 2));
    // Pin Elastic Modulus
    d_W_el = (Math.PI * ((Math.Pow(d_d, 4) - (Math.Pow(d_d_in, 4))))) / (d_d / 2);

    // Conversion
    // Loaded data unit conversion

    // Design Force
    d_F_Ed *= i_ratio_kilo;
    d_F_Ed_ser *= i_ratio_kilo;


    // Steel strength - conversion
    d_f_y /= d_ratio_mega;
    d_f_u /= d_ratio_mega;

    d_f_yp /= d_ratio_mega;
    d_f_up /= d_ratio_mega;

    d_E /= d_ratio_mega;

}

public void Control_Message_SI_Units()
{
    MessageBox.Show((

"d = " + d_d.ToString() + " m " + " \n" +
"d0 = " + d_d_0.ToString() + " m " + " \n" +
"din = " + d_d_in.ToString() + " m " + " \n" +
"t11 = " + d_t_11.ToString() + " m " + " \n" +
"t12 = " + d_t_12.ToString() + " m " + " \n" +
"t21 = " + d_t_21.ToString() + " m " + " \n" +
"t22 = " + d_t_22.ToString() + " m " + " \n" +
"t23 = " + d_t_23.ToString() + " m " + " \n" +
"tc = " + d_t_c.ToString() + " m " + " \n" +

"tmin = " + d_t_min.ToString() + " m " + " \n" +
"tmax = " + d_t_max.ToString() + " m " + " \n" +
"t1min = " + d_t_1_min.ToString() + " m " + " \n" +
"t1max = " + d_t_1_max.ToString() + " m " + " \n" +
"t2min = " + d_t_2_min.ToString() + " m " + " \n" +
"t2max = " + d_t_2_max.ToString() + " m " + " \n" +
"t1 = " + d_t_1.ToString() + " m " + " \n" +
"t2 = " + d_t_2.ToString() + " m " + " \n" +

"A =" + Math.Round(d_A, 6).ToString() + " m2 " + " \n" +
"Wel =" + Math.Round(d_W_el, 6).ToString() + " m3 " + " \n" +
"FEd =" + d_F_Ed.ToString() + " N " + " \n" +
"FEd,ser =" + d_F_Ed_ser.ToString() + " N " + " \n" +
"fy =" + d_f_y.ToString() + " Pa " + " \n" +
"fu =" + d_f_u.ToString() + " Pa " + " \n" +
"fyp =" + d_f_yp.ToString() + " Pa " + " \n" +
"fup =" + d_f_up.ToString() + " Pa " + " \n" +
"E =" + d_E.ToString() + " Pa "
       ) 
       ,
       // Message Header
       "SI Units control");

}

// Main method
public void EN1993_1_8_Main()
{
    // Total Force/ number of cuts ( 5 plates)
    d_F_v_Ed = d_F_Ed / 4;
    
    // Sum of all plates in one dirrection
    // ULS
    d_F_b_Ed = d_F_Ed;
    // SLS
    d_F_b_Ed_ser = d_F_Ed_ser;

    // Figure 3.11: Bending moment in a pin
    // ULS
    double d_M_Ed_1 = d_Calc_M_Ed(d_t_21, d_t_11, d_t_c, 0.5 * d_F_Ed);
    double d_M_Ed_2 = d_Calc_M_Ed(d_t_12, d_t_22, d_t_c, 0.5 * d_F_Ed);
    d_M_Ed = MathF.Min(d_M_Ed_1, d_M_Ed_2);
    // SLS
    double d_M_Ed_1_ser = d_Calc_M_Ed(d_t_21, d_t_11, d_t_c, 0.5 * d_F_Ed_ser);
    double d_M_Ed_2_ser = d_Calc_M_Ed(d_t_12, d_t_22, d_t_c, 0.5 * d_F_Ed_ser);
    d_M_Ed_ser = MathF.Min(d_M_Ed_1_ser, d_M_Ed_2_ser);



    // Table 3.10 Design criteria for pin connections
    // Shear resistance of the pin
    d_F_v_Rd = 0.6 * d_A * (d_f_up / d_gamma_M2);
    // Bearing resistance of the plate and the pin
    d_F_b_Rd = 1.5 * Math.Min(d_t_1,d_t_2) * d_d * (d_f_y / d_gamma_M0);
    // Bearing resistance of the plate and the pin
    // If the pin is intended to be replaceable this requirement should also be satisfied.
    d_F_b_Rd_ser = 0.6 * Math.Min(d_t_1, d_t_2) * d_d * (d_f_y / d_gamma_M6_ser);
    // Bending resistance of the pin
    d_M_Rd = 1.5 * d_W_el * (d_f_yp / d_gamma_M0);
    // Bending resistance of the pin
    // If the pin is intended to be replaceable this requirement should also be satisfied.
    d_M_Rd_ser = 0.8 * d_W_el * (d_f_yp / d_gamma_M6_ser);
    // Combined shear and bending resistance of the pin
    d_ratio_7_pin = (Math.Pow(d_M_Ed / d_M_Rd, 2) + Math.Pow(d_F_v_Ed / d_F_v_Rd, 2)) / 1;


    // (3) If the pin is intended to be replaceable, in addition to the provisions given in 3.13.1 to 3.13.2, the contact bearing stress should satisfy

    // (3.15)
    d_Sigma_h_Ed = 0.591 * Math.Sqrt((d_E * d_F_Ed_ser * (d_d_0 - d_d)) / (Math.Pow(d_d, 2) * Math.Min(d_t_1, d_t_2)));
    // (3.16)
    d_f_h_Ed = 2.5 * d_f_y / d_gamma_M6_ser;


    // Check ratios

    //(3.14)

    d_ratio_1 = d_Sigma_h_Ed / d_f_h_Ed;
    // Table 3.10: Design criteria for pin connections
    d_ratio_2 = d_F_v_Ed / d_F_v_Rd;
    d_ratio_3 = d_F_b_Ed / d_F_b_Rd;
    d_ratio_4 = d_F_b_Ed_ser / d_F_b_Rd_ser;
    d_ratio_5 = d_M_Ed / d_M_Rd;
    d_ratio_6 = d_M_Ed_ser / d_M_Rd_ser;


    
    // Table 3.9: Geometrical requirements for pin ended members
    // (1) Type A: Given thickness t
    d_a_p1 = ((0.5 * d_F_Ed * d_gamma_M0) / (2 * d_t_1_min * d_f_y)) + ((2 * d_d_0) / 3);
    d_c_p1 = ((0.5 * d_F_Ed * d_gamma_M0) / (2 * d_t_1_min * d_f_y)) + (d_d_0 / 3);
    // (2) Type B: Given geometry
    d_t_p2 = 0.7 * Math.Sqrt((0.5 * d_F_Ed * d_gamma_M0) / d_f_y);
    d_d0_p2 = 2.5 * d_t_1_min;

    d_03d0_p2 = 0.30 * d_d_0;
    d_075d0_p2 = 0.75 * d_d_0;
    d_1d0_p2 = 1.00 * d_d_0;
    d_13d0_p2 = 1.3 * d_d_0;
    d_16d0_p2 = 1.6 * d_d_0;
    d_25d0_p2 = 2.5 * d_d_0;


}
// Auxiliary method for Main
double d_Calc_M_Ed(double d_a, double d_b, double d_c, double d_F)
{
    // Figure 3.11: Bending moment in a pin
    double d_M = d_F / 8 * (d_b + 4 * d_c + 2 * d_a);
    return d_M;
}
// Metoda - Nastaví vypocitane hodnoty v textboxoch
public void Set_data()
{
    // Prevod na vystupne jednotky

    d_A /= Math.Pow(d_ratio_mili,2);
    d_W_el /= Math.Pow(d_ratio_mili,3);

    d_f_y *= d_ratio_mega;
    d_f_u *= d_ratio_mega;

    d_f_yp *= d_ratio_mega;
    d_f_up *= d_ratio_mega;



    d_a_p1 /= d_ratio_mili;
    d_c_p1 /= d_ratio_mili;

    d_t_p2 /= d_ratio_mili;
    d_d0_p2 /= d_ratio_mili;

    d_03d0_p2 /= d_ratio_mili;
    d_075d0_p2 /= d_ratio_mili;
    d_1d0_p2 /= d_ratio_mili;
    d_13d0_p2 /= d_ratio_mili;
    d_16d0_p2 /= d_ratio_mili;
    d_25d0_p2 /= d_ratio_mili;



    d_ratio_1 /= d_ratio_percent;
    d_ratio_2 /= d_ratio_percent;
    d_ratio_3 /= d_ratio_percent;
    d_ratio_4 /= d_ratio_percent;
    d_ratio_5 /= d_ratio_percent;
    d_ratio_6 /= d_ratio_percent;
    d_ratio_7_pin /= d_ratio_percent;



    // Nastavia sa načítané a vypocitane hodnoty (skonvetovane z double na string)

    int decimal_pos1 = 1;

    d_A_textB.Text = Math.Round(d_A,decimal_pos1).ToString();
    d_Wel_textB.Text = Math.Round(d_W_el,decimal_pos1).ToString();


    d_dfy_textB.Text = d_f_y.ToString();
    d_dfu_textB.Text = d_f_u.ToString();

    d_dfyp_textB.Text = d_f_yp.ToString();
    d_dfup_textB.Text = d_f_up.ToString();


    d_a_p1_textB.Text = Math.Round(d_a_p1,decimal_pos1).ToString();
    d_c_p1_textB.Text = Math.Round(d_c_p1,decimal_pos1).ToString();



    d_t_p2_textB.Text = Math.Round(d_d0_p2,decimal_pos1).ToString();
    d_d0_p2_textB.Text = Math.Round(d_d0_p2,decimal_pos1).ToString();

    d_03d0_p2_textB.Text = Math.Round(d_03d0_p2,decimal_pos1).ToString();
    d_075d0_p2_textB.Text = Math.Round(d_075d0_p2,decimal_pos1).ToString();
    d_1d0_p2_textB.Text = Math.Round(d_1d0_p2,decimal_pos1).ToString();
    d_13d0_p2_textB.Text = Math.Round(d_13d0_p2,decimal_pos1).ToString();
    d_16d0_p2_textB.Text = Math.Round(d_16d0_p2,decimal_pos1).ToString();
    d_25d0_p2_textB.Text = Math.Round(d_25d0_p2, decimal_pos1).ToString();


    int decimal_pos2 = 1;

    d_ratio_1_textB.Text = Math.Round(d_ratio_1,decimal_pos2).ToString();
    d_ratio_2_textB.Text = Math.Round(d_ratio_2,decimal_pos2).ToString();
    d_ratio_3_textB.Text = Math.Round(d_ratio_3,decimal_pos2).ToString();
    d_ratio_4_textB.Text = Math.Round(d_ratio_4,decimal_pos2).ToString();
    d_ratio_5_textB.Text = Math.Round(d_ratio_5,decimal_pos2).ToString();
    d_ratio_6_textB.Text = Math.Round(d_ratio_6,decimal_pos2).ToString();
    d_ratio_7_textB.Text = Math.Round(d_ratio_7_pin, decimal_pos2).ToString();

}
// Metoda ktora sa spusti po stlaceni tlacidla calculate
private void Calculate_Click_1(object sender, EventArgs e)
{

    // Načítanie dat
    this.Load_data();
    // Načítanie dát pre oceľ
    this.Load_data2_Steel();
    // Uprava jednotek na SI
    this.Convert_data_units();
    // Conctrol messsage - SI UNITS
    this.Control_Message_SI_Units();
    // Vypocet
    this.EN1993_1_8_Main();
    // MessageBox.Show("Vysledky v EN 1992_1_1 Form \n " + (" A = " + D_A + " mm2 \n Iy = " + D_I_y + " mm4 \n Iz = " + D_I_z + " mm4"));
    // zapísanie výsledkov do READONLY textboxov
    this.Set_data();


}
// Cancel dialog
private void button1_Click(object sender, EventArgs e)
{
    this.DialogResult = DialogResult.Cancel;
}
private void b_index_REPLACE_checkbox_CheckedChanged(object sender, EventArgs e)
{
    b_index_REPLACE = b_index_REPLACE_checkbox.Checked;
}
private void b_index_SOLID_checkbox_CheckedChanged(object sender, EventArgs e)
{
    b_index_SOLID = b_index_SOLID_checkbox.Checked;
}
private void b_plates32_radioB_CheckedChanged(object sender, EventArgs e)
{
    b_index_PLATES32 = b_plates32_radioB.Checked;
    if (b_index_PLATES32 == true)
        b_plates21_radioB.Checked = false;
    else b_plates21_radioB.Checked = true;
}
private void b_plates21_radioB_CheckedChanged(object sender, EventArgs e)
{
    b_index_PLATES21 = b_plates21_radioB.Checked;
    if (b_index_PLATES21 == true)
        b_plates32_radioB.Checked = false;
    else b_plates32_radioB.Checked = true;
}











    }
}
