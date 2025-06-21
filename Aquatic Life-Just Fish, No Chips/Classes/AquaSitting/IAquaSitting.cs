using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting
{
    public interface IAquaSitting
    {
        bool IsAlive { get; set; }
        Point Position { get; set; }
        Size Size { get; set; }
        ImageSource Image { get; set; }
        void UpdatePosition();
    }
}
