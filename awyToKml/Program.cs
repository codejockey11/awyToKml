using System;
using System.IO;
using System.Collections.Generic;
using aviationLib;
using System.Linq;

namespace awyToKml
{
    class AirwayData
    {
        public String airway;
        public String pointName;
        public String magCourse;
        public String magCourseOpposite;
        public String mea;
        public String moca;

        public LatLon latLon;

        public AirwayData(String airway, String pointName, String magCourse, String magCourseOpposite, String mea, String moca, String lat, String lon)
        {
            this.airway = airway;
            this.pointName = pointName;
            this.magCourse = magCourse;
            this.magCourseOpposite = magCourseOpposite;
            this.mea = mea;
            this.moca = moca;

            this.latLon = new LatLon(lat, lon);
        }
    }

    class Program
    {
        static StreamReader rowset;
        static String row;
        static String[] columns;

        static String processingType;

        static String prevAirway;
        static String prevType;

        static StreamWriter awyRouteA;
        static StreamWriter awyRouteAT;
        static StreamWriter awyRouteB;
        static StreamWriter awyRouteBF;
        static StreamWriter awyRouteG;
        static StreamWriter awyRouteJ;
        static StreamWriter awyRoutePA;
        static StreamWriter awyRoutePR;
        static StreamWriter awyRouteQ;
        static StreamWriter awyRouteR;
        static StreamWriter awyRouteT;
        static StreamWriter awyRouteV;

        static StreamWriter routeAirway;

        static AirwayData airwayData;

        static readonly List<AirwayData> airwayDatas = new List<AirwayData>();

        
        static void WriteKml(StreamWriter sw)
        {
            for (Int32 i = 0; i < airwayDatas.Count - 1; i++)
            {
                sw.WriteLine("\t<Placemark>");

                sw.Write("\t\t<name>" + airwayDatas[i].airway);

                if (airwayDatas[i].mea.Length > 0)
                {
                    sw.Write(":" + airwayDatas[i].mea);
                }

                if (airwayDatas[i].moca.Length > 0)
                {
                    sw.Write(":" + airwayDatas[i].moca);
                }

                if (airwayDatas[i].magCourse.Length > 0)
                {
                    sw.Write("\n" + airwayDatas[i].magCourse + ":" + airwayDatas[i].magCourseOpposite);
                }

                sw.WriteLine("</name>");


                switch (prevType)
                {
                case "A":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteA</styleUrl>");

                    break;
                }

                case "AT":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteAT</styleUrl>");

                    break;
                }

                case "B":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteB</styleUrl>");

                    break;
                }

                case "BF":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteBF</styleUrl>");

                    break;
                }

                case "G":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteG</styleUrl>");

                    break;
                }

                case "J":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteJ</styleUrl>");

                    break;
                }

                case "PA":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRoutePA</styleUrl>");

                    break;
                }

                case "PR":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRoutePR</styleUrl>");

                    break;
                }

                case "Q":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteQ</styleUrl>");

                    break;
                }

                case "R":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteR</styleUrl>");

                    break;
                }

                case "T":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteT</styleUrl>");

                    break;
                }

                case "V":
                {
                    sw.WriteLine("\t\t<styleUrl>#awyRouteV</styleUrl>");

                    break;
                }
                }

                sw.WriteLine("\t\t<LineString>");

                sw.Write("\t\t\t<coordinates>");
                sw.Write(airwayDatas[i].latLon.decimalLon.ToString("F6") + "," + airwayDatas[i].latLon.decimalLat.ToString("F6") + ",100");

                i++;

                sw.Write(" " + airwayDatas[i].latLon.decimalLon.ToString("F6") + "," + airwayDatas[i].latLon.decimalLat.ToString("F6") + ",100");

                i--;

                sw.WriteLine("</coordinates>");

                sw.WriteLine("\t\t</LineString>");
                sw.WriteLine("\t</Placemark>");
            }
        }

        static StreamWriter InitNewWriter(String name)
        {
            StreamWriter writer = new StreamWriter(name);

            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            writer.WriteLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\">");
            writer.WriteLine("<Document>");

            writer.WriteLine("\t<Style id=\"awyRouteA\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF00BFFF</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteAT\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF00BFFF</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteB\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FFFF0000</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteBF\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FFFF0000</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteG\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF00FF00</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteJ\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF000000</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRoutePA\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF00BFFF</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRoutePR\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF0000FF</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteQ\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FFFF0000</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteR\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF0000FF</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteT\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FFFF0000</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            writer.WriteLine("\t<Style id=\"awyRouteV\">");
            writer.WriteLine("\t\t<LineStyle>");
            writer.WriteLine("\t\t\t<color>FF000000</color>");
            writer.WriteLine("\t\t\t<width>2</width>");
            writer.WriteLine("\t\t</LineStyle>");
            writer.WriteLine("\t</Style>");

            return writer;
        }

        static void CloseWriter(StreamWriter writer)
        {
            writer.WriteLine("</Document>");
            writer.WriteLine("</kml>");

            writer.Close();
        }

        static void ProcessRow(String row)
        {
            columns = row.Split('~');

            if (prevAirway != columns[1])
            {
                if (processingType == "0")
                {
                    switch (prevType)
                    {
                    case "A":
                    {
                        WriteKml(awyRouteA);

                        break;
                    }

                    case "AT":
                    {
                        WriteKml(awyRouteAT);

                        break;
                    }

                    case "B":
                    {
                        WriteKml(awyRouteB);

                        break;
                    }

                    case "BF":
                    {
                        WriteKml(awyRouteBF);

                        break;
                    }

                    case "G":
                    {
                        WriteKml(awyRouteG);

                        break;
                    }

                    case "J":
                    {
                        WriteKml(awyRouteJ);

                        break;
                    }

                    case "PA":
                    {
                        WriteKml(awyRoutePA);

                        break;
                    }

                    case "PR":
                    {
                        WriteKml(awyRoutePR);

                        break;
                    }

                    case "Q":
                    {
                        WriteKml(awyRouteQ);

                        break;
                    }

                    case "R":
                    {
                        WriteKml(awyRouteR);

                        break;
                    }

                    case "T":
                    {
                        WriteKml(awyRouteT);

                        break;
                    }

                    case "V":
                    {
                        WriteKml(awyRouteV);

                        break;
                    }
                    }
                }
                else
                {
                    WriteKml(routeAirway);
                }


                prevAirway = columns[1];

                prevType = columns[2];

                airwayDatas.Clear();
            }


            airwayData = new AirwayData(columns[1], columns[4], columns[7], columns[8], columns[9], columns[11], columns[12], columns[13]);

            airwayDatas.Add(airwayData);
        }

        static void Main(string[] args)
        {
            rowset = new StreamReader(args[0]);

            processingType = args[1];

            if (processingType == "0")
            {
                awyRouteA = InitNewWriter("awyRouteA.kml");
                awyRouteAT = InitNewWriter("awyRouteAT.kml");
                awyRouteB = InitNewWriter("awyRouteB.kml");
                awyRouteBF = InitNewWriter("awyRouteBF.kml");
                awyRouteG = InitNewWriter("awyRouteG.kml");
                awyRouteJ = InitNewWriter("awyRouteJ.kml");
                awyRoutePA = InitNewWriter("awyRoutePA.kml");
                awyRoutePR = InitNewWriter("awyRoutePR.kml");
                awyRouteQ = InitNewWriter("awyRouteQ.kml");
                awyRouteR = InitNewWriter("awyRouteR.kml");
                awyRouteT = InitNewWriter("awyRouteT.kml");
                awyRouteV = InitNewWriter("awyRouteV.kml");
            }
            else
            {
                routeAirway = InitNewWriter("route" + processingType + ".kml");
            }

            row = rowset.ReadLine();

            columns = row.Split('~');


            prevAirway = columns[1];

            prevType = columns[2];


            while (!rowset.EndOfStream)
            {
                ProcessRow(row);

                row = rowset.ReadLine();
            }

            ProcessRow(row);


            if (processingType == "0")
            {
                switch (columns[2])
                {
                case "A":
                {
                    WriteKml(awyRouteA);

                    break;
                }

                case "AT":
                {
                    WriteKml(awyRouteAT);

                    break;
                }

                case "B":
                {
                    WriteKml(awyRouteB);

                    break;
                }

                case "BF":
                {
                    WriteKml(awyRouteBF);

                    break;
                }

                case "G":
                {
                    WriteKml(awyRouteG);

                    break;
                }

                case "J":
                {
                    WriteKml(awyRouteJ);

                    break;
                }

                case "PA":
                {
                    WriteKml(awyRoutePA);

                    break;
                }

                case "PR":
                {
                    WriteKml(awyRoutePR);

                    break;
                }

                case "Q":
                {
                    WriteKml(awyRouteQ);

                    break;
                }

                case "R":
                {
                    WriteKml(awyRouteR);

                    break;
                }

                case "T":
                {
                    WriteKml(awyRouteT);

                    break;
                }

                case "V":
                {
                    WriteKml(awyRouteV);

                    break;
                }
                }

                CloseWriter(awyRouteA);
                CloseWriter(awyRouteAT);
                CloseWriter(awyRouteB);
                CloseWriter(awyRouteBF);
                CloseWriter(awyRouteG);
                CloseWriter(awyRouteJ);
                CloseWriter(awyRoutePA);
                CloseWriter(awyRoutePR);
                CloseWriter(awyRouteQ);
                CloseWriter(awyRouteR);
                CloseWriter(awyRouteT);
                CloseWriter(awyRouteV);
            }
            else
            {
                WriteKml(routeAirway);
                CloseWriter(routeAirway);
            }

            rowset.Close();
        }
    }
}
