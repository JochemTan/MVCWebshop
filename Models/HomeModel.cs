using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebshop.Models
{
 

    public class HomeModel
    {
        public List<Article> ArticleList { get; set; }
        public List<Category> CategoryList { get; set; }
    }

    
}