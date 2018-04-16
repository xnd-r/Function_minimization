using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace function_minimization
{
    public partial class Form1 : Form
    {
        double A, B, C, D;
        double _eps;
        int steps_amount, FNumSteps = 0;
        double r, x_1, x_2;
        List<double> list_points = new List<double>();
        _point Res;
        int k;

        global method = null;

        public Form1()
        {
            InitializeComponent();

        }

        public void _init()
        {
            A = Convert.ToDouble(textBoxA.Text);
            B = Convert.ToDouble(textBoxB.Text);
            C = Convert.ToDouble(textBoxC.Text);
            D = Convert.ToDouble(textBoxD.Text);
            r = Convert.ToDouble(textBoxR.Text);
            x_1 = Convert.ToDouble(textBoxX1.Text);
            x_2 = Convert.ToDouble(textBoxX2.Text);
            _eps = Convert.ToDouble(textBoxEps.Text);
            steps_amount = Convert.ToInt32(textBoxNumSteps.Text);

            k = comboBox1.SelectedIndex;
            switch (k)
            {
                case 0:
                method = new serial_iteration(A, B, C, D, x_1, x_2, steps_amount, _eps);
                break;
                case 1:
                method = new Pijavsky(r, A, B, C, D, x_1, x_2, steps_amount, _eps);
                break;
                case 2:
                method = new Strongin(r, A, B, C, D, x_1, x_2, steps_amount, _eps);
                break;
            }
        }

        private void get_graph()
        {
            FNumSteps = method.res_step_amount();
            list_points = method.GetPoints();
            int n = 1000;
            double[] x = new double[n], y = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = i * (x_2 - x_1) / n + x_1;
                y[i] = A * Math.Sin(B * x[i]) + C * Math.Cos(D * x[i]);
            }

            System.Windows.Forms.DataVisualization.Charting.Series s 
                = new System.Windows.Forms.DataVisualization.Charting.Series();
            s.Points.DataBindXY(x, y);
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            chart1.Series.Clear();
            chart1.Series.Add(s);
            chart1.ChartAreas[0].AxisX.Minimum = x_1;
            chart1.ChartAreas[0].AxisX.Maximum = x_2;
            chart1.ChartAreas[0].AxisY.Minimum = -2d;// y.Min();
            chart1.ChartAreas[0].AxisY.Maximum = y.Max();
            

        }

        private void get_res()
        {
            textBox1.Text = Convert.ToString(Res.x);
            textBox2.Text = Convert.ToString(Res.y);
            textBox3.Text = Convert.ToString(FNumSteps);
        }

        private void get_points()
        {
            double[] x_canvas = new double[list_points.Count], y_canvas = new double[list_points.Count];
            for (int i = 0; i < list_points.Count; i++)
            {
                x_canvas[i] = list_points[i];
                y_canvas[i] = 0;
            }

            System.Windows.Forms.DataVisualization.Charting.Series s1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            for (int i = 0; i < list_points.Count; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.DataPoint d = new System.Windows.Forms.DataVisualization.Charting.DataPoint(list_points[i], 0);
                d.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
                d.Color = Color.Red;
                s1.Points.Add(d);
            }
            s1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            chart1.Series.Add(s1);

            System.Windows.Forms.DataVisualization.Charting.Series s2
                = new System.Windows.Forms.DataVisualization.Charting.Series();
            s2.Color = Color.Black;
            chart1.Series.Add(s2);
            s2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Series3"].Points.AddXY(x_1, 0);
            chart1.Series["Series3"].Points.AddXY(x_2, 0);

        }
        
            private void button1_Click(object sender, EventArgs e)
        {
              _init();
              Res = method.FindMin();
              get_graph();
              get_points();
              get_res();
        }
    }
}
