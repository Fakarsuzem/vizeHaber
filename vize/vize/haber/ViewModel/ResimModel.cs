﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haber.ViewModel
{
    public class ResimModel
    {
        public int ResimId { get; set; }
        public string ResimAdi { get; set; }
        public Nullable<int> HaberId { get; set; }

    }
}