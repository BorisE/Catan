using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catan
{
     public partial class MainForm : Form
     {
        CatanFieldClass CatanField;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CatanField = new CatanFieldClass();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            CatanField.Generate();

            DrawPannels();
        }

 



        private void DrawPannels()
        {
            for (int i = 0; i < 18; i++)
            {
                this.Controls["field"+(i+1).ToString()].Text = CatanField.FieldItems[i].Num.ToString() + Environment.NewLine + CatanField.FieldItems[i].Type.ToString();
                this.Controls["field" + (i + 1).ToString()].BackColor = getFieldColor (CatanField.FieldItems[i].Type);
            }
        }

        
        private Color getFieldColor(FieldType Fld)
        {
            Color Res;

            if (Fld == FieldType.Desert)
            {
                Res = Color.LightYellow;
            }
            else if (Fld == FieldType.Pasture)
            {
                Res = Color.LightGreen;
            }
            else if (Fld == FieldType.Field)
            {
                Res = Color.Yellow;
            }
            else if (Fld == FieldType.Forest)
            {
                Res = Color.ForestGreen;
            }
            else if (Fld == FieldType.Mountain)
            {
                Res = Color.Silver;
            }
            else if (Fld == FieldType.Hill)
            {
                Res = Color.Brown;
            }
            else
            {
                Res = Color.Gray;
            }

            return Res;
        }


    }
}
