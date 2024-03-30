using System;

namespace MarketAutomation.model
{
    public class Urun
    {
        public string id { get; set; }
        public string qrKod { get; set; }
        public string barkod { get; set; }
        public DateTime olusturulmaTarih { get; set; }
        public DateTime guncellenmeTarih { get; set; }
        public string urunIsim { get; set; }
        public int kilo { get; set; }
        public int fiyat { get; set; }
        public int ciro { get; set; }
    }
}
