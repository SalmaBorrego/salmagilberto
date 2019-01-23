using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_1.Properties;

namespace Tarea_1
{
    class miconexion
    {
        public static string ObtenerStringdeConexion()
        {
            return Settings.Default.setting;
        }
        public static string conexion = ObtenerStringdeConexion();
    }
}
