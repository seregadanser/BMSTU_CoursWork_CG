using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    class ObjReader
    {
        StreamReader f;
        List<PointComponent> pointsObj;
        List<PolygonComponent> polygonsObj;
        public ObjReader(string s)
        {
            pointsObj = new List<PointComponent>();
            polygonsObj = new List<PolygonComponent>();
            f = new StreamReader(@s);
        }

        public Model ReadModel()
        {
            Model M = new Model();
            while (true)
            {
                string temp = f.ReadLine();

                if (temp == null) break;

                string[] str =  temp.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (str.Length != 0)
                {
                    if (str[0] == "v")
                    {
                        NewPoint(str);
                    }
                    if (str[0] == "f")
                    {
                        NewPolygon(str);
                    }
                }
            }

            foreach (PointComponent p in pointsObj)
                M.AddPoint(p);
            foreach (PolygonComponent p in polygonsObj)
                M.AddPolygons(p);
            return M;
        }

        void NewPoint(string[] str)
        {
            string g = Convert.ToString(pointsObj.Count + 1);
            Id i = new Id("Point", g);
            double a, b, c;
            double.TryParse(str[1], NumberStyles.Any, CultureInfo.InvariantCulture, out a);
            double.TryParse(str[2], NumberStyles.Any, CultureInfo.InvariantCulture, out b);
            double.TryParse(str[3], NumberStyles.Any, CultureInfo.InvariantCulture, out c);
            pointsObj.Add(new PointComponent(a,b, c,i));
        }

        void NewPolygon(string[] str)
        {
            List<int> p = new List<int>();
            for(int i =1; i<str.Length; i++)
            {
                string[] tmp = str[i].Split("/",StringSplitOptions.RemoveEmptyEntries);
                p.Add(Convert.ToInt32(tmp[0]));
            }

            if(p.Count == 3)
            {
                polygonsObj.Add(new PolygonComponent(pointsObj[p[0]-1], pointsObj[p[1] - 1], pointsObj[p[2] - 1]));
            }
            if(p.Count == 4)
            {
                polygonsObj.Add(new PolygonComponent(pointsObj[p[0] - 1], pointsObj[p[1] - 1], pointsObj[p[2] - 1]));
                polygonsObj.Add(new PolygonComponent(pointsObj[p[1] - 1], pointsObj[p[2] - 1], pointsObj[p[3] - 1]));
            }
        }
    }
}
