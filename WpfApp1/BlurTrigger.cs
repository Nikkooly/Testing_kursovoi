using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace WpfApp1
{
    class BlurTrigger
    {
        readonly BlurEffect blur_Enter = new BlurEffect()
        {
            Radius = 5
        };
        readonly BlurEffect blur_Click = new BlurEffect()
        {
            Radius = 10
        };

        public BlurTrigger(List<Grid> grids)
        {
            foreach (Grid a in grids)
            {
                AddEvents(a);
            }
        }
        public BlurTrigger(Grid grid)
        {

            AddEvents(grid);

        }

        private void AddEvents(Grid grid)
        {
            grid.MouseEnter += Enter;
            grid.MouseLeave += Leave;
            grid.MouseDown += Click;
            grid.MouseUp += NoneClick;
        }
        private void Enter(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Effect = blur_Enter;

        }
        private void Leave(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Effect = null;

        }
        private void Click(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Effect = blur_Click;
        }
        private void NoneClick(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Effect = null;
        }
    }
}
