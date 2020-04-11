using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
     public partial class MainForm : Form
    {
        public enum FieldType
        {
            None = -1,
            Desert = 0,
            Pasture = 1,
            Field = 2,
            Forest = 3,
            Mountain = 4,
            Hill = 5

        }

        public class CatanFieldElement
        {
            public string Name = "";
            public int id = 0;
            public int Num = 0;
            public FieldType Type = FieldType.None;
        }

        CatanFieldElement[] CatanField = new CatanFieldElement[18];

        public class CatanFieldClass
        {
            public CatanFieldElement[] FieldItems = new CatanFieldElement[18];


        }


        List<FieldType> AvailableFields;
        List<int> AvailableNums;

        public class CoorPair
        {
            public int x;
            public int y;

            public CoorPair(int extx, int exty)
            {
                x = extx;
                y = exty;
            }
        }

        Dictionary<int, Tuple<int, int>> Coordinates_old = new Dictionary<int, Tuple<int, int>>();
        Dictionary<int, CoorPair> Coordinates = new Dictionary<int, CoorPair>();

        public MainForm()
        {
            InitializeComponent();

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            InitFields();

            GenerateField();

            DrawPannels();
        }

        void InitFields()
        {
            AvailableFields = new List<FieldType>()
            {
                FieldType.Pasture, FieldType.Pasture, FieldType.Pasture, FieldType.Pasture,
                FieldType.Field, FieldType.Field, FieldType.Field, FieldType.Field,
                FieldType.Forest, FieldType.Forest, FieldType.Forest, FieldType.Forest,
                FieldType.Mountain, FieldType.Mountain, FieldType.Mountain,
                FieldType.Hill,FieldType.Hill,FieldType.Hill
            };

            AvailableNums = new List<int>()
            {
                1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6,
                8, 8, 9, 9, 10, 10, 11, 11, 12
            };


            Coordinates_old.Add(0, new Tuple<int, int>(2, 0));
            Coordinates_old.Add(1, new Tuple<int, int>(4, 0));
            Coordinates_old.Add(2, new Tuple<int, int>(6, 0));

            Coordinates.Add(0,  new CoorPair(2, 0));
            Coordinates.Add(1,  new CoorPair(4, 0));
            Coordinates.Add(2,  new CoorPair(6, 0));

            Coordinates.Add(11, new CoorPair(1, 2));
            Coordinates.Add(12, new CoorPair(3, 2));
            Coordinates.Add(13, new CoorPair(5, 2));
            Coordinates.Add(3,  new CoorPair(7, 2));

            Coordinates.Add(10, new CoorPair(0, 4));
            Coordinates.Add(17, new CoorPair(2, 4));
            Coordinates.Add(18, new CoorPair(4, 4));
            Coordinates.Add(14, new CoorPair(6, 4));
            Coordinates.Add(4,  new CoorPair(8, 4));

            Coordinates.Add(9,  new CoorPair(1, 6));
            Coordinates.Add(16, new CoorPair(3, 6));
            Coordinates.Add(15, new CoorPair(5, 6));
            Coordinates.Add(5,  new CoorPair(7, 6));

            Coordinates.Add(8,  new CoorPair(2, 8));
            Coordinates.Add(7,  new CoorPair(4, 8));
            Coordinates.Add(6,  new CoorPair(6, 8));

        }

        void GenerateField()
        {
            var fldidx = 0;
            FieldType curField = FieldType.None;
            Random Rnd = new Random();

            var numidx = 0;
            int curNum = 0;

            for (int i = 0; i < 18; i++)
            {
                fldidx = Rnd.Next(0, AvailableFields.Count - 1);
                curField = AvailableFields[fldidx];
                AvailableFields.RemoveAt(fldidx);

                numidx = Rnd.Next(0, AvailableNums.Count - 1);
                curNum = AvailableNums[numidx];
                AvailableNums.RemoveAt(numidx);

                CatanField[i] = new CatanFieldElement
                {
                    id = i,
                    Num = curNum,
                    Type = curField
                };
            }
        }

        private void DrawPannels()
        {
            for (int i = 0; i < 18; i++)
            {
                this.Controls["field"+(i+1).ToString()].Text = CatanField[i].Num.ToString() + Environment.NewLine + CatanField[i].Type.ToString();
                this.Controls["field" + (i + 1).ToString()].BackColor = getFieldColor (CatanField[i].Type);
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

        private double CalcDistance (CoorPair P1, CoorPair P2)
        {
            double dist = Math.Sqrt((P2.x - P1.x) * (P2.x - P1.x) + (P2.y - P1.y) * (P2.y - P1.y));
            return dist;
        }
    }
}
