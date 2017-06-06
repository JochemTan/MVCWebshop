using MVCWebshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebshop.Controllers
{
    public class HomeController : Controller
    {
        ShopEntities db = new ShopEntities();


        public ActionResult Index()
        {
            HomeModel hm = new HomeModel();
            List<Article> ArticleList = (from article in db.Articles select article).Take(4).ToList();
            List<Category> CategoryList = (from category in db.Categories select category).Take(5).ToList();

            hm.ArticleList = ArticleList;
            hm.CategoryList = CategoryList;


            return View(hm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}