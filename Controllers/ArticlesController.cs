using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWebshop.Models;
using MVCWebshop.Managers;

namespace MVCWebshop.Controllers
{
    public class ArticlesController : Controller
    {
        private ShopEntities db = new ShopEntities();
        

        // GET: Articles
        public ActionResult Index()
        {
            HomeModel hm = new HomeModel();
            hm.ArticleList = (from article in db.Articles select article).ToList();
            hm.CategoryList = (from category in db.Categories select category).ToList();
            //var articles = db.Articles.Include(a => a.Category).Include(a => a.Supplier);
            return View(hm);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        


        // Filter
        [HttpPost]
        public ActionResult FilterForm(int categoryId, int sortId)
        {
            HomeModel hm = new HomeModel();
            int categoryID = categoryId;
            int sortingID = sortId;



            hm.ArticleList = null;
            hm.CategoryList = (from category in db.Categories select category).ToList();

            if (categoryID == 0 && sortingID == 0)
                hm.ArticleList = (from a in db.Articles select a).ToList();

            else
            {
                // if sorting is > 1 but category = 0 it crashes
                switch (sortingID)
                {
                    case 0:
                        hm.ArticleList = (from article in db.Articles where article.CategoryID == categoryID select article).ToList();
                        break;
                    case 1:
                        if(categoryID > 0)
                            hm.ArticleList = (from article in db.Articles where article.CategoryID == categoryID select article).OrderBy(a => a.Price).ToList();
                        else
                            hm.ArticleList = (from article in db.Articles select article).OrderBy(a => a.Price).ToList();
                        break;
                    case 2:
                        if(categoryID > 0)
                            hm.ArticleList = (from article in db.Articles where article.CategoryID == categoryID select article).OrderByDescending(a => a.Price).ToList();
                        else
                            hm.ArticleList = (from article in db.Articles select article).OrderByDescending(a => a.Price).ToList();
                        break;
                    case 3:
                        if(categoryID > 0)
                            hm.ArticleList = (from article in db.Articles where article.CategoryID == categoryID select article).OrderBy(a => a.Name).ToList();
                        else
                            hm.ArticleList = (from article in db.Articles select article).OrderBy(a => a.Name).ToList();
                        break;
                    case 4:
                        if(categoryID > 0)
                            hm.ArticleList = (from article in db.Articles where article.CategoryID == categoryID select article).OrderByDescending(a => a.Price).ToList();
                        else
                            hm.ArticleList = (from article in db.Articles  select article).OrderByDescending(a => a.Price).ToList();
                        break;
                    default:
                        break;
                }

            }


            return View("Index", hm);
        }

        [HttpPost]
        public ActionResult addToCart(int quantity, int ArticleID)
        {
            // this list will contain all the articles that are in the current session
            List<Article> articleList = new List<Article>();
            List<ShoppingCartModel> scmList = new List<ShoppingCartModel>();
            Article article = db.Articles.Find(ArticleID);


            // loops through the current session and places the articles in the article list
            

            if(SessionManager.CartList != null)
            {
                foreach (var cart in SessionManager.CartList)
                {
                    articleList.Add(cart.Article);
                }

                bool containItem = articleList.Any(item => item.ID == article.ID);
                if (containItem)
                {
                    // if the article exists within the session
                    SessionManager.CartList.FirstOrDefault(session => session.Article.ID == article.ID).Amount += quantity;
                    
                }
                else
                {
                   
                    ShoppingCartModel scm = new ShoppingCartModel();
                    scm.Amount = quantity;
                    scm.Article = article;

                    scmList.Add(scm);

                    SessionManager.CartList = scmList;
                }

            }
            else
            {
                ShoppingCartModel scm = new ShoppingCartModel();
                scm.Amount = quantity;
                scm.Article = article;

                scmList.Add(scm);
                SessionManager.CartList = scmList;
                
            }

            var s = SessionManager.CartList;
            // create session object here
            return View("Details",article);
        }
       

    

    }
}
