using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
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

    public class CatanFieldElement
    {
        public string Name = "";
        public int id = 0;
        public int Num = 0;
        public FieldType Type = FieldType.None;
        public CoorPair Coord;

    }

    public class CatanFieldClass
    {
        public CatanFieldElement[] FieldItems = new CatanFieldElement[18];

        List<FieldType> AvailableFields;
        List<int> AvailableNums;

        public CatanFieldClass()
        {
            //Init Field
            for (int i = 0; i < 18; i++)
            {
                FieldItems[i] = new CatanFieldElement();
            }

            //Init field elements coordinates 
            FieldItems[0].Coord = new CoorPair(2, 0);
            FieldItems[1].Coord = new CoorPair(4, 0);
            FieldItems[2].Coord = new CoorPair(6, 0);

            FieldItems[11].Coord = new CoorPair(1, 2);
            FieldItems[12].Coord = new CoorPair(3, 2);
            FieldItems[13].Coord = new CoorPair(5, 2);
            FieldItems[3].Coord = new CoorPair(7, 2);

            FieldItems[0].Coord = new CoorPair(0, 4);
            FieldItems[0].Coord = new CoorPair(2, 4);
            FieldItems[0].Coord = new CoorPair(4, 4);
            FieldItems[0].Coord = new CoorPair(6, 4);
            FieldItems[0].Coord = new CoorPair(8, 4);

            FieldItems[0].Coord = new CoorPair(1, 6);
            FieldItems[0].Coord = new CoorPair(3, 6);
            FieldItems[0].Coord = new CoorPair(5, 6);
            FieldItems[0].Coord = new CoorPair(7, 6);

            FieldItems[0].Coord = new CoorPair(2, 8);
            FieldItems[0].Coord = new CoorPair(4, 8);
            FieldItems[0].Coord = new CoorPair(6, 8);

        }

        /// <summary>
        /// Private function to init cards arrays
        /// </summary>
        private void InitFieldCards()
        {
            //clear prev instance
            AvailableFields = null;
            AvailableNums = null;

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
        }

        public void Generate()
        {
            InitFieldCards();


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

                FieldItems[i] = new CatanFieldElement
                {
                    id = i,
                    Num = curNum,
                    Type = curField
                };
            }
        }

        private double CalcDistance(CoorPair P1, CoorPair P2)
        {
            double dist = Math.Sqrt((P2.x - P1.x) * (P2.x - P1.x) + (P2.y - P1.y) * (P2.y - P1.y));
            return dist;
        }

    }

}
