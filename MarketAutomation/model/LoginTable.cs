namespace MarketAutomation.model
{
    public class LoginTable
    {
        public int id { get; set; }
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }
        public string yetki { get; set; }
        public string emailAdres { get; set; }
        public string guvenlikSorusu { get; set; }
        public string guvenlikCevabi { get; set; }
    }
}
