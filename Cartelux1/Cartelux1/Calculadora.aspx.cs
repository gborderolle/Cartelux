using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1
{
    public partial class Calculadora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Static Methods

        [WebMethod]
        public static _DataMaterialImportes[] GetData_BaseCalculos(string dummy)
        {
            List<_DataMaterialImportes> _DataMaterialImportes_list = new List<_DataMaterialImportes>();

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                List<config_material_importes> config_material_importes_elements = context.config_material_importes.ToList();
                foreach (config_material_importes _config_material_importes in config_material_importes_elements)
                {
                    if (_config_material_importes != null)
                    {
                        // Códigos:
                        /*
                        1	Lona front Mate
                        2	Lona doble faz
                        3	Vinilo
                        4	Vinilo Canvas
                        5	Vinilo Microperforado
                        6	Tela bandera
                        7	Fondo de color extra
                        8	Banners c/ojalillos
                        9	Banners c/perfiles
                         * */

                        _DataMaterialImportes _DataMaterialImporte1 = new Cartelux1.Calculadora._DataMaterialImportes();
                        _DataMaterialImporte1.Codigo = _config_material_importes.Codigo;
                        _DataMaterialImporte1.Precio = _config_material_importes.Precio;
                        _DataMaterialImporte1.Costo = _config_material_importes.Costo;

                        _DataMaterialImportes_list.Add(_DataMaterialImporte1);
                    }

                } // foreach
            }
            return _DataMaterialImportes_list.ToArray();
        }


        public class _DataMaterialImportes
        {
            public int Codigo { get; set; }
            public decimal? Precio { get; set; }
            public decimal? Costo { get; set; }
        }


        #endregion
    }
}