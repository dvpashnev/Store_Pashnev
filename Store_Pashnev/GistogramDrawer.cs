using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Pashnev
{
  static class GistogramDrawer
  {
    private static Graphics graphics;

    static float sidesOffset = 30;

    static private float relation;

    static Font font = new Font("Times New Roman", 10);
    static Pen penCoordStepLines = new Pen(new SolidBrush(Color.Black), 0.5F);
    
    static float height;
    static float width;

    static List<Color> colors;

    public class PairForGraphic
    {
      public string name { set; get; }
      public decimal value { set; get; }
    }

    public class Pair2ForGraphic
    {
      public string name { set; get; }
      public decimal value0 { set; get; }
      public decimal value1 { set; get; }
    }

    private static float GetStepNumber(float number)
    {
      int step = (int)number;
      int iter = 0;
      do
      {
        step /= 10;
        iter++;
      } while (step > 9);

      int digits = 1;
      for (int i = 0; i < iter; i++)
      {
        digits *= 10;
      }

      if ((step * digits) + 50 < number)
      {
        step++;
      }
      return step * digits;
    }

    private static void GetColors()
    {
      Array colorsArray = Enum.GetValues(typeof(KnownColor));
      KnownColor[] allColors = new KnownColor[colorsArray.Length];
      Array.Copy(colorsArray, allColors, colorsArray.Length);
      colors = new List<Color>();

      for (int c = 0; c < allColors.Length; c++)
      {
        if (allColors[c].ToString().IndexOf("Black") != -1
            || allColors[c].ToString().IndexOf("White") != -1
            || allColors[c].ToString().IndexOf("Text") != -1
            || allColors[c].ToString().IndexOf("Menu") != -1
            || allColors[c].ToString().IndexOf("Window") != -1
            || allColors[c].ToString().IndexOf("Bar") != -1
            || allColors[c].ToString().IndexOf("Info") != -1
            || allColors[c].ToString().IndexOf("Border") != -1
            || allColors[c].ToString().IndexOf("Control") != -1
            || allColors[c].ToString().IndexOf("Azure") != -1
            || allColors[c].ToString().IndexOf("Inactive") != -1
            || allColors[c].ToString().IndexOf("Dark") != -1
            || allColors[c].ToString().IndexOf("Desktop") != -1
            || allColors[c].ToString().IndexOf("Honeydew") != -1
            || allColors[c].ToString().IndexOf("Light") != -1
            || allColors[c].ToString().IndexOf("Medium") != -1
            || allColors[c].ToString().IndexOf("Cornsilk") != -1
            || allColors[c].ToString().IndexOf("Gainsboro") != -1
            || allColors[c].ToString().IndexOf("Alice") != -1
            || allColors[c].ToString().IndexOf("Beige") != -1
            || allColors[c].ToString().IndexOf("Bisque") != -1
            || allColors[c].ToString().IndexOf("BlanchedAlmond") != -1
            || allColors[c].ToString().IndexOf("Lavender") != -1
            || allColors[c].ToString().IndexOf("Transparent") != -1
            || allColors[c].ToString().IndexOf("0") != -1
            || allColors[c].ToString().IndexOf("Ivory") != -1
            || allColors[c].ToString().IndexOf("LemonChiffon") != -1
            || allColors[c].ToString().IndexOf("Linen") != -1
            || allColors[c].ToString().IndexOf("Button") != -1
            || allColors[c].ToString().IndexOf("Snow") != -1
            || allColors[c].ToString().IndexOf("SeaShell") != -1
            || allColors[c].ToString().IndexOf("MintCream") != -1
            || allColors[c].ToString().IndexOf("OldLace") != -1
            || allColors[c].ToString().IndexOf("MistyRose") != -1
            || allColors[c].ToString().IndexOf("Moccasin") != -1
            || allColors[c].ToString().IndexOf("PaleGoldenrod") != -1
            || allColors[c].ToString().IndexOf("PapayaWhip") != -1
            || allColors[c].ToString().IndexOf("BurlyWood") != -1)
        {
          continue;
        }
        colors.Add(Color.FromName(allColors[c].ToString()));
      }
    }

    private static void DrawCoordLines()
    {
      Pen penCoordLines = new Pen(new SolidBrush(Color.Black), 2);
      penCoordLines.EndCap = LineCap.ArrowAnchor;
      penCoordLines.CustomEndCap = new AdjustableArrowCap(5, 5);
      graphics.DrawLine(penCoordLines, sidesOffset, height - sidesOffset, sidesOffset, sidesOffset);
      graphics.DrawLine(penCoordLines, sidesOffset, height - sidesOffset, width - sidesOffset, height - sidesOffset);

      float coordLineYDigitsStep = GetStepNumber((float)Math.Floor((height / relation + sidesOffset * 5) / 20));
      float coordLineYStep = coordLineYDigitsStep * relation;

      for (float y = height - sidesOffset, YDigit = 0;
        y > sidesOffset;
        y -= coordLineYStep, YDigit += coordLineYDigitsStep)
      {
        graphics.DrawLine(penCoordStepLines, sidesOffset - 2, y, sidesOffset + 2, y);
        graphics.DrawString(YDigit.ToString(), font, new SolidBrush(Color.Black), new PointF(0, y - 5));
      }
    }

    public static void DrawGistogram(List<PairForGraphic> dataList, Control ctrl)
    {
      graphics = ctrl.CreateGraphics();
      graphics.Clear(Color.FromName(KnownColor.Control.ToString()));

      height = ctrl.Height;
      width = ctrl.Width;

      double maxValue = Convert.ToDouble(dataList.Max(r => r.value));

      relation = (float)((height - sidesOffset * 2) / maxValue);

      DrawCoordLines();

      if (colors == null)
      {
        GetColors();
      }
      
      float coordLineXStep = (width - sidesOffset * 3) / dataList.Count();

      float rectWidth = coordLineXStep - (coordLineXStep/100*20);
      float x = sidesOffset + rectWidth / 2 + 25;

      foreach (var order in dataList)
      {
        graphics.DrawLine(penCoordStepLines, x, height - sidesOffset + 2, x, height - sidesOffset - 2);
        graphics.DrawString(order.name, font, new SolidBrush(Color.Black),
          new PointF(x - (font.SizeInPoints * order.name.Length / 2), height - (sidesOffset/100*90)));
        x += coordLineXStep;
      }

      x = sidesOffset + rectWidth / 2 + 25;

      int colorIndex = 15;
      
      Matrix matrix = new Matrix();

      foreach (var order in dataList)
      {
        float Y = ((float)order.value * relation);

        Brush YRect = new SolidBrush(colors[colorIndex == colors.Count ? colorIndex = 0 : colorIndex++]);

        matrix.Translate(x - (rectWidth - 5) / 2, height - sidesOffset);
        matrix.Rotate(-90);
        graphics.Transform = matrix;

        RectangleF rect = new RectangleF(0, 0, Y, rectWidth - 5);
        graphics.FillRectangle(YRect, rect);

        RectangleF rectForString = new RectangleF(0, rectWidth / 2 - font.SizeInPoints, height, font.Size * 2);

        graphics.DrawString(order.name + " : " + order.value, font,
          new SolidBrush(Color.Black), rectForString);

        matrix.Rotate(90);
        matrix.Translate(-(x - (rectWidth - 5) / 2), -(height - sidesOffset));
        graphics.Transform = matrix;

        x += coordLineXStep;
      }
    }

    public static void DrawGistogramWithAccumulation(List<Pair2ForGraphic> dataList, List<string> marksNames, Control ctrl)
    {
      graphics = ctrl.CreateGraphics();
      graphics.Clear(Color.FromName(KnownColor.Control.ToString()));

      height = ctrl.Height;
      width = ctrl.Width;

      double maxValue = Convert.ToDouble(dataList.Max(r => r.value0 + r.value1));

      relation = (float)((height - sidesOffset * 2) / maxValue);

      DrawCoordLines();

      if (colors == null)
      {
        GetColors();
      }

      float coordLineXStep = (width - sidesOffset * 3) / dataList.Count();

      float rectWidth = coordLineXStep - 50;
      float x = sidesOffset + rectWidth / 2 + 25;

      foreach (var order in dataList)
      {
        graphics.DrawLine(penCoordStepLines, x, height - sidesOffset + 2, x, height - sidesOffset - 2);
        graphics.DrawString(order.name, font, new SolidBrush(Color.Black),
          new PointF(x - sidesOffset / 3, height - 15));
        x += coordLineXStep;
      }

      int[] colorIndeces = {5, 9};

      int ci = 0;
      float step = font.SizeInPoints*2;
      float xSing = font.SizeInPoints;
      int maxSing = marksNames.Max(_ => _.Length);
      foreach (string marksName in marksNames)
      {
        RectangleF rectSign = new RectangleF(width - maxSing * font.SizeInPoints - 15, xSing, 10, 10);
        graphics.FillRectangle(new SolidBrush(colors[colorIndeces[ci++]]), rectSign);

        RectangleF rectForStringSign = new RectangleF(width - maxSing * font.SizeInPoints, xSing, maxSing * font.SizeInPoints, font.SizeInPoints * 2);

        graphics.DrawString(marksName, font,
          new SolidBrush(Color.Black), rectForStringSign);

        xSing += step;
      }

      x = sidesOffset + rectWidth / 2 + 25;
      
      Matrix matrix = new Matrix();

      foreach (var order in dataList)
      {
        matrix.Translate(x - rectWidth / 2, height - sidesOffset);
        matrix.Rotate(-90);
        graphics.Transform = matrix;

        float Y0 = ((float)order.value0 * relation);

        Brush Rect0Brush = new SolidBrush(colors[colorIndeces[0]]);

        RectangleF rect0 = new RectangleF(0, 0, Y0, rectWidth);
        graphics.FillRectangle(Rect0Brush, rect0);

        RectangleF rect0ForString = new RectangleF(0, font.Size, height, font.Size * 2);

        graphics.DrawString(order.value0.ToString(), font,
          new SolidBrush(Color.Black), rect0ForString);

        float Y1 = ((float)order.value1 * relation);

        Brush Rect1Brush = new SolidBrush(colors[colorIndeces[1]]);

        RectangleF rect1 = new RectangleF(Y0, 0, Y1, rectWidth);
        graphics.FillRectangle(Rect1Brush, rect1);

        RectangleF rect1ForString = new RectangleF(Y0, font.Size, height, font.Size * 2);

        graphics.DrawString(order.value1.ToString(), font,
          new SolidBrush(Color.Black), rect1ForString);

        matrix.Rotate(90);
        matrix.Translate(-(x - rectWidth / 2), -(height - sidesOffset));
        graphics.Transform = matrix;

        x += coordLineXStep;
      }
    }

  }
}
