using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskLine.CMS.Models.Constants
{
    public class ShippingStatus
    {
        public static string OrderReceived = "Order received";
        public static string DispatchingFromSource = "Dispatching from source";
        public static string Delivering = "Delivering";
        public static string Arrived = "Arrived";
    }
}