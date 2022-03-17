﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCareApp
{
    public class FlyoutPageMenuFlyoutMenuItem
    {
        public FlyoutPageMenuFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutPageMenuFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}