using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CENEX
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
            CSOClass CSOprop = new CSOClass();

            // Calculation of cross section attributes


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
            this.drawPictureFromDatagrid();
            CSO cso = new CSO(ySuradnice,zSuradnice,tHodnoty);
        }



        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.drawPictureFromDatagrid();

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




