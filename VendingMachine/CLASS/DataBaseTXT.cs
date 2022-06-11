
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Fajar_INSPIRO.CLASS
{
    public class DataBaseTXT
    {
        public string barang_nama { get; set; }
        public int barang_stok { get; set; }
        public DataBaseTXT()
        {

        }

        public static List<DataBaseTXT> GetBarangList()
        {
            List<DataBaseTXT> l = new List<DataBaseTXT>();
            l.Add(new DataBaseTXT { barang_stok = CekStok("Biskuit"), barang_nama = "Biskuit" });
            l.Add(new DataBaseTXT { barang_stok = CekStok("Chips"), barang_nama = "Chips" });
            l.Add(new DataBaseTXT { barang_stok = CekStok("Oreo"), barang_nama = "Oreo" });
            l.Add(new DataBaseTXT { barang_stok = CekStok("Tango"), barang_nama = "Tango" });
            l.Add(new DataBaseTXT { barang_stok = CekStok("Cokelat"), barang_nama = "Cokelat" });
            return l;
        }

        public void Create_Initial()
        {
            string directoryname = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/STOK");
            string emat_setting_txt = "stok.txt";
            string GuarnteedWritePath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(GuarnteedWritePath, directoryname)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(GuarnteedWritePath, directoryname));
            }

            string FilePath = System.IO.Path.Combine(GuarnteedWritePath, directoryname, emat_setting_txt);
            if (!System.IO.File.Exists(FilePath))
            {
               List<DataBaseTXT> e = DataBaseTXT.GetBarangList();
                using (System.IO.FileStream fs = System.IO.File.Create(FilePath))
                {
                    string dataasstring = Newtonsoft.Json.JsonConvert.SerializeObject(e); //your data
                    byte[] info = new System.Text.UTF8Encoding(true).GetBytes(dataasstring);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }
        }

        public static int CekStok(string namaBarang)
        {
            List<DataBaseTXT> lm = DataBaseTXT.GetTXT_LIST();
            var stok_ = lm.Where(opt => opt.barang_nama == namaBarang).Select(x => x.barang_stok).FirstOrDefault();
            return stok_;
        }

        public static List< DataBaseTXT> GetTXT_LIST()
        {
            List<DataBaseTXT> e = new List<DataBaseTXT>();
            string directoryname = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/STOK");
            string emat_setting_txt = "stok.txt";

            string jsontext = System.IO.File.ReadAllText(System.IO.Path.Combine(directoryname, emat_setting_txt));
            e = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataBaseTXT>>(jsontext);
            return e;
        }

        public static void UpdateData(DataBaseTXT s)
        {
            List<DataBaseTXT> r = DataBaseTXT.GetTXT_LIST();
            var match = r.FirstOrDefault(x => x.barang_nama.ToLower() == s.barang_nama.ToLower());
            if (match != null) r.Remove(match);
             r.Insert(0, s);
            SaveMakanan(r);
        }

        public static void SaveMakanan(List<DataBaseTXT> s)
        {
            string directoryname = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/STOK");
            string emat_setting_txt = "stok.txt";
            string GuarnteedWritePath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            try
            {
                using (System.IO.StreamWriter stream = new System.IO.StreamWriter(System.IO.Path.Combine(GuarnteedWritePath, directoryname, emat_setting_txt), false))
                {
                    stream.Write(Newtonsoft.Json.JsonConvert.SerializeObject(s));
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}