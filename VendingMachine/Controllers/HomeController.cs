using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_Fajar_INSPIRO.Controllers
{
    public class HomeController : Controller
    {
        public System.Web.Mvc.ActionResult Index()
        {
            new CLASS.DataBaseTXT().Create_Initial();
            ViewData["listMakanan"] = new SelectList(GetListNamaMakanan(), "Value", "Text");
            ViewData["AcceptMoney"] = new SelectList(AcceptMoney(), "Value", "Text");
            return View();
        }

        public System.Web.Mvc.ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
                      return View();
        }

        public System.Web.Mvc.ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetData()
        {
            CLASS.Makanan makanan = CLASS.Makanan.GetmakananFromDataSource();
            List<CLASS.JenisMakanan> l = makanan.Jenis;
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        private List<System.Web.Mvc.SelectListItem> GetListNamaMakanan()
        {
            List<System.Web.Mvc.SelectListItem> l = new List<SelectListItem>();

            CLASS.Makanan makanan = CLASS.Makanan.GetmakananFromDataSource();
            List<CLASS.JenisMakanan> lm = makanan.Jenis;
            foreach (var x in lm)
            {
                System.Web.Mvc.SelectListItem item = new SelectListItem();
                item.Text = x.nama;
                item.Value = x.nama;
                l.Add(item);
            }
            return l;
        }

        private List<System.Web.Mvc.SelectListItem> AcceptMoney()
        {
            List<System.Web.Mvc.SelectListItem> l = new List<SelectListItem>();
           
            CLASS.Makanan makanan = CLASS.Makanan.GetmakananFromDataSource();
            List<int> mylist = makanan.Pecahan;
            foreach (var aOrB in mylist)
            {
                System.Web.Mvc.SelectListItem item = new SelectListItem();
                item.Text =""+(int)aOrB;
                item.Value = "" + (int)aOrB;
                l.Add(item);
            }
            return l;
        }

        public JsonResult BeliMakanan([FromBody] CLASS.JenisMakanan c)
        {
            //Msgbox(titlex,text_,error_or_success,button_text_)
            object obj =new { title = "Konfirmasi",text="BadRequest",error_success="warning",button="Close It" };
            CLASS.Makanan makanan = CLASS.Makanan.GetmakananFromDataSource();
            List<CLASS.JenisMakanan> lm = makanan.Jenis;

            var MakananPrice=lm.Where(opt => opt.nama == c.nama).Select(x => x.price).FirstOrDefault();

            // cek bila uang kurang
            var sisa_uang = c.price - MakananPrice;

            if (sisa_uang < 0)
            {
                obj = new { title = "Konfirmasi", text = "Price < Petty Cash", error_success = "warning", button = "Close It" };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            //cek stok barang
            var stok_ = CLASS.DataBaseTXT.CekStok(c.nama);
            if (stok_<=0)
            {
                obj = new { title = "Konfirmasi", text = "Produk="+c.nama +"  "+"Stok="+stok_, error_success = "warning", button = "Close It" };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            // potong stok
            stok_ -= 1;

            // Update Database
            CLASS.DataBaseTXT.UpdateData(new CLASS.DataBaseTXT { barang_nama = c.nama, barang_stok = stok_ });

            obj = new { title = "Konfirmasi", text = "Produk=" + c.nama + "  " + "Stok Sisa=" + stok_ +" Change_Cash="+sisa_uang, error_success = "success", button = "Close It" };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}