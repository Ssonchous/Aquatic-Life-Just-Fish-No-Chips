﻿using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators
{
    public class HunterDecorator: BaseFishDecorator
    {
        public int BiteStrength { get; set; } = 20;

        public HunterDecorator(BaseFish fish, int biteStrength) : base(fish) 
        {
            BiteStrength = biteStrength;
        }

        public int Bite(BaseFish prey)
        {
            int sut = (int)Math.Min(BiteStrength, prey.Health);
            MessageBox.Show($"Кусь {prey.Name} ");
            prey.Health -= BiteStrength;
            return sut;

        }

    }
}
