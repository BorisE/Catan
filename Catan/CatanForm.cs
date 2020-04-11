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
            bool bHillsFlag = false, bMountFlag = false, bPastureFlag = false, bForestFlag=false, bFieldFlag = false;
            bool bSolutionFound = false;
            int i =0;
            for (i = 0; i < 1000; i++)
            {
                CatanField.Generate();
                
                //Check HILLS
                if (chkHillsFar.Checked)
                {
                    var minDist = CatanField.GetNearestSameTerrainTypeMinDistance(TerrainType.Hill);
                    txtInfo.Text += "Min distance for type: " + TerrainType.Hill.ToString() + " is " + minDist.ToString("0.00") + Environment.NewLine;
                    bHillsFlag = (minDist > 2.4);
                }
                else
                {
                    bHillsFlag = true;
                }

                //Check Mountains
                if (chkMoutainFar.Checked)
                {
                    var minDist = CatanField.GetNearestSameTerrainTypeMinDistance(TerrainType.Mountain);
                    txtInfo.Text += "Min distance for type: " + TerrainType.Mountain.ToString() + " is " + minDist.ToString("0.00") + Environment.NewLine;

                    bMountFlag = (minDist > 2.4);
                }
                else
                {
                    bMountFlag = true;
                }

                //Check Pasture
                if (chkPastureFar.Checked)
                {
                    var minDist = CatanField.GetNearestSameTerrainTypeMinDistance(TerrainType.Pasture);
                    txtInfo.Text += "Min distance for type: " + TerrainType.Pasture.ToString() + " is " + minDist.ToString("0.00") + Environment.NewLine;

                    bPastureFlag = (minDist > 2.4);
                }
                else
                {
                    bPastureFlag = true;
                }

                //Check Forest
                if (chkForestFar.Checked)
                {
                    var minDist = CatanField.GetNearestSameTerrainTypeMinDistance(TerrainType.Forest);
                    txtInfo.Text += "Min distance for type: " + TerrainType.Forest.ToString() + " is " + minDist.ToString("0.00") + Environment.NewLine;

                    bForestFlag = (minDist > 2.4);

                }
                else
                {
                    bForestFlag = true;
                }

                //Check Field
                if (chkFieldsFar.Checked)
                {
                    var minDist = CatanField.GetNearestSameTerrainTypeMinDistance(TerrainType.Field);
                    txtInfo.Text += "Min distance for type : " + TerrainType.Field.ToString() + " is " + minDist.ToString("0.00") + Environment.NewLine;

                    bFieldFlag = (minDist > 2.4);

                }
                else
                {
                    bFieldFlag = true;
                }


                bSolutionFound = bHillsFlag && bMountFlag && bPastureFlag && bForestFlag && bFieldFlag;
                if (bSolutionFound)
                    break;
            }
            DrawPannels();
            toolStripStatusLabel_Tries.Text = "Tries : " + i ;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            List<FieldElement> HillsList;
            HillsList = CatanField.GetItemsByTerrainType(TerrainType.Hill);
            txtInfo.Text += "HillsList.Count: " + HillsList.Count + Environment.NewLine;

            var minDist = 100.0;
            foreach (var item in HillsList)
            {
                FieldElement Nearest = CatanField.GetNearestSameTerrainType(item);
                var dist = CatanField.CalcDistance(item, Nearest);
                txtInfo.Text += "Current: " + item.id.ToString() + ", dist: "+ dist + " to nearest: " + Nearest.id + " of type: " + Nearest.Type.ToString() + Environment.NewLine;
                if (dist < minDist)
                {
                    minDist = dist;
                }
            }
            txtInfo.Text += "Min distance for type : " + HillsList[0].Type.ToString() + "is " + minDist + Environment.NewLine;


            double Dist = CatanField.CalcDistance(CatanField.FieldItems[0].Coord, CatanField.FieldItems[18].Coord);
            txtInfo.Text+= Dist + Environment.NewLine;
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInfo.Text = "";
        }

        private void DrawPannels()
        {
            for (int i = 0; i < 18; i++)
            {
                this.Controls["field"+(i+1).ToString()].Text = CatanField.FieldItems[i].Num.ToString() + Environment.NewLine + CatanField.FieldItems[i].Type.ToString() + Environment.NewLine + "id:" + CatanField.FieldItems[i].id.ToString();
                this.Controls["field" + (i + 1).ToString()].BackColor = getFieldColor (CatanField.FieldItems[i].Type);
            }
        }

        
        private Color getFieldColor(TerrainType Fld)
        {
            Color Res;

            if (Fld == TerrainType.Desert)
            {
                Res = Color.LightYellow;
            }
            else if (Fld == TerrainType.Pasture)
            {
                Res = Color.LightGreen;
            }
            else if (Fld == TerrainType.Field)
            {
                Res = Color.Yellow;
            }
            else if (Fld == TerrainType.Forest)
            {
                Res = Color.ForestGreen;
            }
            else if (Fld == TerrainType.Mountain)
            {
                Res = Color.Silver;
            }
            else if (Fld == TerrainType.Hill)
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
