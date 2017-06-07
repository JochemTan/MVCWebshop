using MVCWebshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MVCWebshop.Managers
{
    public static class SessionManager
    {
        public static List<ShoppingCartModel> CartList
        {
            get { return (List<ShoppingCartModel>)HttpContext.Current.Session["cart"]; }
            set { HttpContext.Current.Session["cart"] = value; }
        }

    }

    // making a custom class so that the order with the corresponding quantity can be stored in 1 list
   


    
}