using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolloVerde.Controllers
{
    public class ReportController : Controller
    {
        [Authorize(Roles = "administrador")]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult reporteSucursal()
        {

            ReportDocument rp = new ReportDocument();
            rp.Load(Path.Combine(Server.MapPath("~/Reportes"), "VentasxSucursal.rpt"));
            rp.SetDatabaseLogon(null, null, "localhost", "PolloVerde");
            rp.VerifyDatabase();
            rp.Refresh();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "VentasPorSucursal.pdf");

        }

        public ActionResult reporteRegion()
        {

            ReportDocument rp = new ReportDocument();
            rp.Load(Path.Combine(Server.MapPath("~/Reportes"), "VentasxRegion.rpt"));
            rp.SetDatabaseLogon(null, null, "localhost", "PolloVerde");
            rp.VerifyDatabase();
            rp.Refresh();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "VentasPorRegion.pdf");

        }

        public ActionResult reporteProducto()
        {

            ReportDocument rp = new ReportDocument();
            rp.Load(Path.Combine(Server.MapPath("~/Reportes"), "VentasxTipoProducto.rpt"));
            rp.SetDatabaseLogon(null, null, "localhost", "PolloVerde");
            rp.VerifyDatabase();
            rp.Refresh();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "VentasPorTipoProducto.pdf");

        }

        public ActionResult reportePago()
        {

            ReportDocument rp = new ReportDocument();
            rp.Load(Path.Combine(Server.MapPath("~/Reportes"), "VentasxTipoPago.rpt"));
            rp.SetDatabaseLogon(null, null, "localhost", "PolloVerde");
            rp.VerifyDatabase();
            rp.Refresh();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "VentasPorTipoPago.pdf");

        }

        public ActionResult reporteTopTen()
        {

            ReportDocument rp = new ReportDocument();
            rp.Load(Path.Combine(Server.MapPath("~/Reportes"), "TopTenSucursales.rpt"));
            rp.SetDatabaseLogon(null, null, "localhost", "PolloVerde");
            rp.VerifyDatabase();
            rp.Refresh();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "TopTenSucursales.pdf");

        }

        //REPORTE PARA LAS FACTURAS
        [HttpGet]
        public ActionResult crearFactura(int idfactura)
        {

            ReportDocument rp = new ReportDocument();
            rp.Load(Path.Combine(Server.MapPath("~/Reportes"), "ReporteFactura.rpt"));
            rp.SetDatabaseLogon(null, null, "localhost", "PolloVerde");
            rp.SetParameterValue("@factura", idfactura);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "ReporteFactura.pdf");

        }
    }
}