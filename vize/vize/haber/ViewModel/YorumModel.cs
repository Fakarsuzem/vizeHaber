using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using haber.Models;

namespace haber.ViewModel
{
  public class YorumModel
  {
        public int YorumId { get; set; }
        public string Yorum1 { get; set; }
        public Nullable<int> Kullanici { get; set; }
        public string Puan { get; set; }
        public virtual Kullanici Kullanici1 { get; set; }

  }
}
