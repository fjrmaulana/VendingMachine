using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Fajar_INSPIRO.CLASS
{
    public class Makanan
    {
        public System.Collections.Generic.List<int> Pecahan { get; set; }
        public System.Collections.Generic.List<JenisMakanan> Jenis { get; set; }

        public Makanan()
        {

        }

        public static Makanan GetmakananFromDataSource()
        {
            System.Collections.Generic.List<JenisMakanan> l = new System.Collections.Generic.List<JenisMakanan>();
            l.Add(new JenisMakanan { nama = "Biskuit", price = 6000, stok=DataBaseTXT. CekStok("Biskuit") });
            l.Add(new JenisMakanan { nama = "Chips", price = 8000, stok = DataBaseTXT.CekStok("Chips") });
            l.Add(new JenisMakanan { nama = "Oreo", price = 10000, stok = DataBaseTXT.CekStok("Oreo") });
            l.Add(new JenisMakanan { nama = "Tango", price = 12000, stok = DataBaseTXT.CekStok("Tango") });
            l.Add(new JenisMakanan { nama = "Coklat", price = 15000, stok = DataBaseTXT.CekStok("Coklat") });
            var pecahan_ = new System.Collections.Generic.List<int>() { 2000, 5000, 10000, 20000, 50000 };
            return new Makanan { Jenis = l, Pecahan = pecahan_ };
        }

        public static bool IsAccept_Money(string Money)
        {
            bool return_ = false;
            if (string.IsNullOrEmpty( Money)) return_ = false;
            if (!CLASS.Helper.IsNumeric(Money)) return_ = false;

            return return_;
        }

        public static RequestMessage Trx(JenisMakanan jenisMakanan)
        {
            RequestMessage r = new RequestMessage { errMessage = "Stok Habis", errNumber = "01" };

            return r;
        }
    }

    public class RequestMessage
    {
        public string errMessage { get; set; }
        public string errNumber { get; set; }
        public RequestMessage()
        {

        }
    }

    public class JenisMakanan
    {
        public int price { get; set; }
        public string nama { get; set; }
        public int stok { get; set; }
    }
}