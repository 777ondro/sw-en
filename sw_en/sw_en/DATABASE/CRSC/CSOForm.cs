using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CRSC
{
    public partial class CSOForm : Form
    {
        private List<int> ySuradnice;
        private List<int> zSuradnice;
        private List<int> ySuradniceDatagrid;
        private List<int> zSuradniceDatagrid;
        private List<int> tHodnoty;
        private List<int> idHodnoty;

        //Constructor
        public CSOForm()
        {
            InitializeComponent();
            ySuradnice = new List<int>(5);  //for counting
            zSuradnice = new List<int>(5);
            ySuradniceDatagrid = new List<int>(5); //for writing values to the picture
            zSuradniceDatagrid = new List<int>(5);
            tHodnoty = new List<int>(5);
            idHodnoty = new List<int>(5);

        }
        public void button1_Click(object sender, EventArgs e)
        {
            CSOClass CSOprop = new CSOClass();


            CSOprop.CSOMessage();


            




        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CSOClass CSOprop = new CSOClass();

            // Calculation of cross section attributes
            this.getListsFromDatagrid();
            CSO cso = new CSO(this.ySuradniceDatagrid, this.zSuradniceDatagrid, this.tHodnoty);



            // Round numerical values
            int dec_place_num1 = 1;
            int dec_place_num2 = 2;

            double d_A = Math.Round(cso.A, dec_place_num2);
            double d_A_vy = Math.Round(cso.d_A_vy, dec_place_num2);
            double d_A_vz = Math.Round(cso.d_A_vz, dec_place_num2);
            double d_y_gc = Math.Round(cso.D_y_gc, dec_place_num2);
            double d_z_gc = Math.Round(cso.D_z_gc, dec_place_num2);
            double d_S_y0 = Math.Round(cso.Sy0, dec_place_num2);
            double d_S_z0 = Math.Round(cso.Sz0, dec_place_num2);
            double d_I_y = Math.Round(cso.Iy, dec_place_num2);
            double d_I_z = Math.Round(cso.Iz, dec_place_num2);

            double d_Alpha = Math.Round(cso.Alfa, dec_place_num2);
            double d_I_yz = Math.Round(cso.Iyz, dec_place_num2);
            double d_I_eps = Math.Round(cso.Iepsilon, dec_place_num2);
            double d_I_eta = Math.Round(cso.Imikro, dec_place_num2);
            double d_I_ome = Math.Round(cso.Iomega, dec_place_num2);
            double d_ome_mean = Math.Round(cso.Omega_mean, dec_place_num2);
            double d_ome_max = Math.Round(cso.Omega_max, dec_place_num2);
            double d_I_y_ome = Math.Round(cso.Iy_omega, dec_place_num2);
            double d_I_z_ome = Math.Round(cso.Iz_omega, dec_place_num2);
            double d_I_ome_ome = Math.Round(cso.Iomega_omega, dec_place_num2);

            double d_y_s = Math.Round(cso.D_y_s, dec_place_num2);
            double d_z_s = Math.Round(cso.D_z_s, dec_place_num2);
            double d_I_p = Math.Round(cso.Ip, dec_place_num2);
            double d_y_j = Math.Round(cso.D_y_j, dec_place_num2);
            double d_z_j = Math.Round(cso.D_z_j, dec_place_num2);
            double d_I_w = Math.Round(cso.D_I_w, dec_place_num2);
            double d_W_w = Math.Round(cso.D_W_w, dec_place_num2);
            double d_I_t = Math.Round(cso.D_I_t, dec_place_num2);
            double d_W_t = Math.Round(cso.D_W_t, dec_place_num2);

            
            //vymazanie datagridview2
            dataGridView2.Rows.Clear();
            //Pridavanie Riadkov do Datagridview2 
            //Potrebne popridavat vsetky premenne,ktore chceme zobrazit
            dataGridView2.Rows.Add("Ag ="    , d_A    , "mm2", "Avy ="  , d_A_vy,  "mm2",  "Avz =", d_A_vz, "mm2");
            dataGridView2.Rows.Add("ygc ="   , d_y_gc, "mm" , "SyO ="  , d_S_y0,  "mm3",  "IyO =", d_I_y, "mm4");
            dataGridView2.Rows.Add("zgc ="   , d_z_gc, "mm", "SzO =", d_S_z0, "mm3", "Iz =",d_I_z, "mm4");
            dataGridView2.Rows.Add("alpha =" , d_Alpha , "rad", " " , " ", " ",  "Iyz =", d_I_yz, "mm4");
            dataGridView2.Rows.Add("Ieps ="  , d_I_eps, "mm4", "Ieta =", d_I_eta, "mm4", " " ," " , " ");
            dataGridView2.Rows.Add("Iomega =", d_I_ome, "mm6", "omega mean =", d_ome_mean, "mm2", d_ome_max, "mm2");
            dataGridView2.Rows.Add("Iyomega =", d_I_y_ome, "mm6", "Izomega =", d_I_z_ome, "mm6", "Iomega_omega =", d_I_ome_ome, "mm6");
            dataGridView2.Rows.Add("ys =", d_y_s, "mm", "zs =", d_z_s, "mm", "Ip =", d_I_p, "mm4");
            dataGridView2.Rows.Add("yj =", d_y_j, "mm", "zj =", d_z_j, "mm", " ", " ", " ");
            dataGridView2.Rows.Add("Iw =", d_I_w, "mm6", "Ww =", d_W_w, "mm3", " ", " ", " ");
            dataGridView2.Rows.Add("It =", d_I_t, "mm4", "Wt =", d_W_t, "mm3", " ", " ", " ");



        }

        private void getListsFromDatagrid()
        {
            int y, z, t,id;
            ySuradnice.Clear();
            zSuradnice.Clear();
            ySuradniceDatagrid.Clear();
            zSuradniceDatagrid.Clear();
            tHodnoty.Clear();
            idHodnoty.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                y = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                z = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                t = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                ySuradnice.Add(y);
                zSuradnice.Add(z);
                ySuradniceDatagrid.Add(y);
                zSuradniceDatagrid.Add(z);
                tHodnoty.Add(t);
                idHodnoty.Add(id);
            }
        }
        //funkcia na najdenie maxima v zozname int hodnot
        private int findMax(List<int> zoznam) 
        {
            int max = zoznam[0];
            for (int i = 1; i < zoznam.Count; i++) 
            {
                if (zoznam[i] > max) max = zoznam[i];
            }
            return max;
        }
        //funkcia na najdenie minima v zozname int hodnot
        private int findMin(List<int> zoznam)
        {
            int min = zoznam[0];
            for (int i = 1; i < zoznam.Count; i++)
            {
                if (zoznam[i] < min) min = zoznam[i];
            }
            return min;
        }
        private float countZoomForPicture(int xmax,int xmin,int ymax,int ymin) 
        {
            float pomer;
            int width = pictureBox1.Width - 60; //vynechane nejake miesto od okrajov
            int height = pictureBox1.Height - 60;
            if (width != height) MessageBox.Show("Nerovnake velkosti width a height obrazka");
            int maxDifference;
            try
            {
                maxDifference = Math.Max(xmax - xmin, ymax - ymin);
                pomer = (float)width / maxDifference;
                //MessageBox.Show("pomer:" + pomer);
            }
            catch (DivideByZeroException) { pomer = 1; }
                
            return pomer;
        }
        private void countPositionsForLists() 
        {
            int minx = this.findMin(ySuradnice);
            int miny = this.findMin(zSuradnice);
            int posun_x, posun_y;

            if (minx < 0) 
            {
                posun_x = Math.Abs(minx);
                for (int i = 0; i < ySuradnice.Count; i++)
                {
                    ySuradnice[i] += posun_x;
                }
            }
            if (minx > 0) 
            {
                posun_x = minx;
                for (int i = 0; i < ySuradnice.Count; i++)
                {
                    ySuradnice[i] -= posun_x;
                }
            }
            if (miny < 0)
            {
                posun_y = Math.Abs(miny);
                for (int i = 0; i < zSuradnice.Count; i++)
                {
                    zSuradnice[i] += posun_y;
                }
            }
            if (miny > 0)
            {
                posun_y = miny;
                for (int i = 0; i < zSuradnice.Count; i++)
                {
                    zSuradnice[i] -= posun_y;
                }
            }
           
        }

        private void drawPictureFromDatagrid()
        {
            Image image = Image.FromFile(@"biely.bmp");
            Bitmap myBitmap = new Bitmap(image, pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(myBitmap);
            Font font = new Font("Arial",10,FontStyle.Bold);
            Font font2 = new Font("Courier new", 8);
            Brush brush = new SolidBrush(Color.Black);
            

            Pen p = new Pen(Color.Red, 5);
            p.DashStyle = DashStyle.Solid;
            float y1, z1, t1, y2, z2, t2;
            int okraj = 30;

            //cast inicializacie a prepoctov suradnic pre maximalne vykreslenie
            try
            {
                this.getListsFromDatagrid();   //ziskanie zoznamov int suradnic 
            }
            catch (FormatException) { MessageBox.Show("Attributes in table in wrong format!"); return; }
            catch (NullReferenceException) { MessageBox.Show("Set all atributes in a row!"); return; }
            
            int ymax = this.findMax(ySuradnice);  //najdenie maximalnej hodnoty suradnice y
            int ymin = this.findMin(ySuradnice);
            int zmax = this.findMax(zSuradnice);
            int zmin = this.findMin(zSuradnice);
            float pomer;
            
            pomer = this.countZoomForPicture(ymax, ymin, zmax, zmin);  //vypocet pomeru 
            this.countPositionsForLists(); //nastavenie hodnot na pociatocny bod

            if (ySuradnice.Count > 1)
            {
                for (int i = 0; i < ySuradnice.Count - 1; i++)
                {
                    y1 = (ySuradnice[i] * pomer + okraj);
                    z1 = 400 - (zSuradnice[i] * pomer + okraj);
                    t1 = tHodnoty[i];
                    y2 = (ySuradnice[i + 1] * pomer + okraj);
                    z2 = 400 - (zSuradnice[i + 1] * pomer + okraj);
                    t2 = tHodnoty[i + 1];
                    p = new Pen(Color.Red, t2);
                    if (t2 > 0) g.DrawLine(p, y1, z1, y2, z2);
                    p = new Pen(Color.Black, 4);
                    g.DrawRectangle(p, y1, z1, 2, 2);
                    g.DrawString(idHodnoty[i].ToString(), font, brush, y1-5, z1+4 );
                    g.DrawString("["+(int)ySuradniceDatagrid[i]+","+(int)zSuradniceDatagrid[i]+"]", 
                                font2, brush, y1 - 30, z1 - 20);
                    g.DrawRectangle(p, y2, z2, 2, 2);
                    g.DrawString(idHodnoty[i+1].ToString(), font, brush, y2-5, z2+4 );
                    g.DrawString("[" + (int)ySuradniceDatagrid[i+1] + "," + (int)zSuradniceDatagrid[i+1] + "]",
                                font2, brush, y2 - 30, z2 - 20);
                    
                }
            }
            else
            {
                y1 = ySuradnice[0] + okraj;
                z1 = 400- (zSuradnice[0] + okraj);
                p = new Pen(Color.Black, 4);
                g.DrawRectangle(p, y1, z1, 2, 2);
                g.DrawString(idHodnoty[0].ToString(), font, brush, y1-5, z1+4);
                g.DrawString("[" + (int)ySuradniceDatagrid[0] + "," + (int)zSuradniceDatagrid[0] + "]",
                                font2, brush, y1 - 20, z1 - 20);
                
            }

            p.Dispose();
            g.Dispose();
            pictureBox1.Image = myBitmap;
        }
        private void buttonDraw_Click(object sender, EventArgs e)
        {
            try
            {
                this.drawPictureFromDatagrid();
                CSO cso = new CSO(ySuradnice, zSuradnice, tHodnoty);
            }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Set values to y,z,t in the table."); }
        }



        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.drawPictureFromDatagrid();
            }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Set values to y,z,t in the table."); }

        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i-1].Cells[0].Value = i;
            
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i-1].Cells[0].Value = i;
            
        }

        





    }
}




