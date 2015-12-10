using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HappySantaSadSanta.Resources;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HappySantaSadSanta
{
    public partial class MainPage : PhoneApplicationPage
    {

        Ellipse face;
        ArcSegment seg1;

        // Konstruktor
        public MainPage()
        {
            InitializeComponent();

            printFaceOutline();
            // Beispielcode zur Lokalisierung der ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        void printFaceOutline()
        {
            face = new Ellipse();

            SolidColorBrush scb = new SolidColorBrush();
            scb.Color = Color.FromArgb(255, 255, 255, 0);

            face.Width = 400;
            face.Height = 400;
            face.Fill = scb;
            
            myCanvas.Children.Add(face);
            Canvas.SetLeft(face, myCanvas.Width / 2 - face.Width / 2);
            Canvas.SetTop(face, myCanvas.Height / 2 - face.Height / 2);


            SolidColorBrush bb = new SolidColorBrush();
            bb.Color = Color.FromArgb(255, 0,0, 0);

            Ellipse e1 = new Ellipse();
            e1.Width = 50;
            e1.Height = 50;
            e1.Fill = bb;
            myCanvas.Children.Add(e1);
            Canvas.SetLeft(e1, myCanvas.Width / 2 - e1.Width / 2 - 70);
            Canvas.SetTop(e1, myCanvas.Height / 2 - e1.Height / 2 - 90);


            Ellipse e2 = new Ellipse();
            e2.Width = 50;
            e2.Height = 50;
            e2.Fill = bb;
            myCanvas.Children.Add(e2);
            Canvas.SetLeft(e2, myCanvas.Width / 2 - e2.Width / 2 + 70);
            Canvas.SetTop(e2, myCanvas.Height / 2 - e2.Height / 2 - 90);


            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(0, 0); // Point(myCanvas.Width / 2 - e2.Width / 2 + 70, myCanvas.Height / 2 - e2.Height / 2 - 90);
            
            seg1 = new ArcSegment();
            
            seg1.Point = new Point(250, 0);
            seg1.Size = new System.Windows.Size(250, 0);
            
            //seg1.Point2 = new Point(250, 0);

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(seg1);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            
            Path myPath = new Path();
            myPath.Stroke = bb;
            myPath.StrokeThickness = 20;
            myPath.Data = myPathGeometry;

            myCanvas.Children.Add(myPath);

            Canvas.SetLeft(myPath, myCanvas.Width / 2 - 125);
            Canvas.SetTop(myPath, myCanvas.Height / 2 + 50);
        }

        private void myCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            int y = (int)e.GetPosition(sender as UIElement).Y - (int)(myCanvas.Height / 2);
            
            int height = 0;
            height = Math.Min(Math.Abs((int)(1000 * (y / myCanvas.Height * 2))),1000);

            seg1.Size = new System.Windows.Size(250, height);
            if (y < 0)
            {
                if(seg1.SweepDirection == SweepDirection.Counterclockwise)
                    seg1.SweepDirection = SweepDirection.Clockwise;
                face.Fill = getColor(height,true);
            }
            else
            {
                seg1.SweepDirection = SweepDirection.Counterclockwise;
                face.Fill = getColor(height,false);
            }

        }

        private SolidColorBrush getColor(int height, bool good)
        {
            double rel = Math.Min(1,height / 667.0);
            byte r;
            byte g;
            
            if (good)
            {
                r = (byte)(255);
                g = (byte)(255 - rel * 255);
            }
            else
            {
                r = (byte)(255 - rel * 255);
                g = (byte)(255);
            }

            Color myColor = Color.FromArgb(255, r, g, 0);

            SolidColorBrush toReturn = new SolidColorBrush();
            toReturn.Color = myColor;
            return toReturn;
        }

        // Beispielcode zur Erstellung einer lokalisierten ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // ApplicationBar der Seite einer neuen Instanz von ApplicationBar zuweisen
        //    ApplicationBar = new ApplicationBar();

        //    // Eine neue Schaltfläche erstellen und als Text die lokalisierte Zeichenfolge aus AppResources zuweisen.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Ein neues Menüelement mit der lokalisierten Zeichenfolge aus AppResources erstellen
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}