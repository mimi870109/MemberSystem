using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberSystem.Models
{
    public class Member
    {
        public int id { get; set; }
        public string txtname { get; set; }
        public string gender { get; set; }
        public string txttel { get; set; }
        public string txtaddress { get; set; }
    }
}