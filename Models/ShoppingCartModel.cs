using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebshop.Models
{
    public class ShoppingCartModel
    {
            public Article Article { get; set; }
            public int Amount { get; set; }
    }
}