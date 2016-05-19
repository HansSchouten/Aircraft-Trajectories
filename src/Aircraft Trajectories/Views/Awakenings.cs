using AircraftTrajectories.Models.TemporalGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{
    public partial class Awakenings : Form
    {
        public Awakenings()
        {
            InitializeComponent();
        }

        private void Awakenings_Load(object sender, EventArgs e)
        {
        }

        private double getNoiseValue(int x, int y, Grid grid)
        {
            int stepX = 125;
            int stepY = 125;
            int gridX = (int) Math.Floor((decimal) x / stepX);
            int gridY = (int) Math.Floor((decimal) y / stepY);
            return grid.Data[gridY][gridX];
        }

        private double getChance(double noiseValue)
        {
            return 0.0087 * Math.Pow(noiseValue - 50.5, 1.79);
        }

        private Boolean randomBool(double chance)
        {
            return (chance > (new Random()).NextDouble());
        }
    }
}
