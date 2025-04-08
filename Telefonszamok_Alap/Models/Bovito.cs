using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Telefonszamok_Alap.Models
{
    public static class Bovito
    {
        public static string Telefonszamok(this enSzemely enSzemely)
        {
            var s = "";
            foreach (var x in enSzemely.enTelefonszamok)
            {
                s = s + x.Szam;
                if (x != enSzemely.enTelefonszamok.Last())
                {
                    s = s + ", ";
                }
            }
            return s;
        }
    }
}
