using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using haber.Models;

namespace haber.ViewModel
{
  public class HaberModel
  {
        public int HaberId { get; set; }
        public string HaberBaslik { get; set; }
        public Nullable<int> Kategori { get; set; }
        public string HaberIcerik { get; set; }
        public Nullable<int> Yazar { get; set; }
        public string YayinlanmaTarihi { get; set; }
  }
}
