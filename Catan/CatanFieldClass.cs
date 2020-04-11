using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public enum TerrainType
    {
        None = -1,
        Desert = 0,
        Pasture = 1,
        Field = 2,
        Forest = 3,
        Mountain = 4,
        Hill = 5

    }

    public class CoordinatePair
    {
        public int x;
        public int y;

        public CoordinatePair(int extx, int exty)
        {
            x = extx;
            y = exty;
        }
    }

    public class FieldElement
    {
        public string Name = "";
        public int id = 0;
        public int Num = 0;
        public TerrainType Type = TerrainType.None;
        public CoordinatePair Coord;

    }

    public class CatanFieldClass
    {
        public FieldElement[] FieldItems = new FieldElement[19];

        List<TerrainType> AvailableTerrains;
        List<int> AvailableNums;

        public CatanFieldClass()
        {
            //Init Field
            for (int i = 0; i < 19; i++)
            {
                FieldItems[i] = new FieldElement();
            }

            //Init field elements coordinates 
            FieldItems[0].Coord = new CoordinatePair(2, 0);
            FieldItems[1].Coord = new CoordinatePair(4, 0);
            FieldItems[2].Coord = new CoordinatePair(6, 0);

            FieldItems[11].Coord = new CoordinatePair(1, 2);
            FieldItems[12].Coord = new CoordinatePair(3, 2);
            FieldItems[13].Coord = new CoordinatePair(5, 2);
            FieldItems[3].Coord = new CoordinatePair(7, 2);

            FieldItems[10].Coord = new CoordinatePair(0, 4);
            FieldItems[17].Coord = new CoordinatePair(2, 4);
            FieldItems[18].Coord = new CoordinatePair(4, 4);
            FieldItems[14].Coord = new CoordinatePair(6, 4);
            FieldItems[4].Coord = new CoordinatePair(8, 4);

            FieldItems[9].Coord = new CoordinatePair(1, 6);
            FieldItems[16].Coord = new CoordinatePair(3, 6);
            FieldItems[15].Coord = new CoordinatePair(5, 6);
            FieldItems[5].Coord = new CoordinatePair(7, 6);

            FieldItems[8].Coord = new CoordinatePair(2, 8);
            FieldItems[7].Coord = new CoordinatePair(4, 8);
            FieldItems[6].Coord = new CoordinatePair(6, 8);

        }

        /// <summary>
        /// init available cards arrays
        /// </summary>
        private void InitAvailableCards()
        {
            //clear prev instance
            AvailableTerrains = null;
            AvailableNums = null;

            AvailableTerrains = new List<TerrainType>()
                {
                    TerrainType.Pasture, TerrainType.Pasture, TerrainType.Pasture, TerrainType.Pasture,
                    TerrainType.Field, TerrainType.Field, TerrainType.Field, TerrainType.Field,
                    TerrainType.Forest, TerrainType.Forest, TerrainType.Forest, TerrainType.Forest,
                    TerrainType.Mountain, TerrainType.Mountain, TerrainType.Mountain,
                    TerrainType.Hill,TerrainType.Hill,TerrainType.Hill
                };

            AvailableNums = new List<int>()
                {
                    1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6,
                    8, 8, 9, 9, 10, 10, 11, 11, 12
                };
        }

        /// <summary>
        /// Generate Game Field
        /// </summary>
        public void Generate()
        {
            InitAvailableCards();

            var fldidx = 0;
            TerrainType curField = TerrainType.None;
            Random Rnd = new Random();

            var numidx = 0;
            int curNum = 0;

            for (int i = 0; i < 18; i++)
            {
                fldidx = Rnd.Next(0, AvailableTerrains.Count - 1);
                curField = AvailableTerrains[fldidx];
                AvailableTerrains.RemoveAt(fldidx);

                numidx = Rnd.Next(0, AvailableNums.Count - 1);
                curNum = AvailableNums[numidx];
                AvailableNums.RemoveAt(numidx);

                FieldItems[i].id = i;
                FieldItems[i].Num = curNum;
                FieldItems[i].Type = curField;
            }
        }

        /// <summary>
        /// Get all Field Items of desired terrain type
        /// </summary>
        /// <param name="TT"></param>
        /// <returns></returns>
        public List<FieldElement> GetItemsByTerrainType(TerrainType TT)
        {
            List<FieldElement> RetItems = new List<FieldElement>();
            for (int i = 0; i < 18; i++)
            {
                if (FieldItems[i].Type == TT)
                {
                    RetItems.Add(FieldItems[i]);
                }
            }
            return RetItems;

        }

        /// <summary>
        /// Search for nearest item with the same terrain type
        /// </summary>
        /// <param name="AnalyzedFieldItem"></param>
        /// <returns></returns>
        public FieldElement GetNearestSameTerrainType(FieldElement AnalyzedFieldItem)
        {
            var dist = 0.0;
            var mindist = 1000.0;
            FieldElement retField = AnalyzedFieldItem;
            for (int i = 0; i < 18; i++)
            {
                if (FieldItems[i].Type == AnalyzedFieldItem.Type && AnalyzedFieldItem.id != i)
                {
                    dist = CalcDistance(AnalyzedFieldItem, FieldItems[i]);
                    if (dist < mindist)
                    {
                        mindist = dist;
                        retField = FieldItems[i];
                    }
                }
            }
            return retField;
        }

        /// <summary>
        /// Calc minimum distance for the same terrain types items (by terrain type)
        /// </summary>
        /// <param name="TT"></param>
        /// <returns></returns>
        public double GetNearestSameTerrainTypeMinDistance(TerrainType TT)
        {
            List<FieldElement> ItemsList;
            ItemsList = GetItemsByTerrainType(TT);

            var minDist = 100.0;
            foreach (var item in ItemsList)
            {
                FieldElement Nearest = GetNearestSameTerrainType(item);
                var dist = CalcDistance(item, Nearest);
                if (dist < minDist)
                {
                    minDist = dist;
                }
            }

            return minDist;
        }

        public double CalcDistance(FieldElement P1, FieldElement P2)
        {
            return CalcDistance(P1.Coord, P2.Coord);
        }
        public double CalcDistance(CoordinatePair P1, CoordinatePair P2)
        {
            double dist = Math.Sqrt((P2.x - P1.x) * (P2.x - P1.x) + (P2.y - P1.y) * (P2.y - P1.y));
            return dist;
        }

    }

}
