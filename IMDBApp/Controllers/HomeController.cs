using DataAccess;
using IMDBApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDBApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                Dictionary<string, DataTable> lstTables = new Dictionary<string, DataTable>();
                DataTable allData = DA.ExecStoreProc("GetMoviesData");
                List<DataTable> groupedData = allData.AsEnumerable().GroupBy(row => row.Field<string>("Genre")).Select(g => g.CopyToDataTable()).ToList();

                for (int table = 0; table < groupedData.Count; table++)
                {
                    string groupName = Convert.ToString(groupedData[table].Rows[0][3]);
                    groupedData[table].Columns.Remove("Genre");
                    lstTables.Add(groupName, groupedData[table]);
                }
                return View(lstTables);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Actors()
        {
            DataTable grps = DA.ExecStoreProc("GetActors");
            return View(grps);
        }

        public ActionResult Producers()
        {
            DataTable grps = DA.ExecStoreProc("GetProducers");
            return View(grps);
        }

        //public ActionResult CreateActor()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateActor(Actor objAct)
        //{
        //    try
        //    {
        //        int res = DA.InsertActor(objAct.Name, objAct.Sex, objAct.DOB, objAct.BIO);
        //        if (res == 1)
        //        {
        //            ViewBag.Res = 1;
        //            return View();
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public ActionResult CreateProducer()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateProducer(Actor objAct)
        //{
        //    try
        //    {
        //        int res = DA.InsertActor(objAct.Name, objAct.Sex, objAct.DOB, objAct.BIO, "producer");
        //        if (res == 1)
        //        {
        //            ViewBag.Res = 1;
        //            return View();
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public ActionResult CreateMovie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMovie(Movie objMovie)
        {
            try
            {
                int movieId = 0;
                int res = DA.InsertMovie(objMovie.Name, objMovie.Year, objMovie.Plot, objMovie.Poster, objMovie.Producer, out movieId);
                if (res == 1 && movieId > 0)
                {
                    res = -1;
                    res = DA.InsertMovieActors(movieId, objMovie.Actors);
                    if (res > 0)
                        ViewBag.Res = 1;
                    else
                        ViewBag.Res = -1;
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult PopulateActors()
        {
            try
            {
                var ResName = (from DataRow row in DA.ExecStoreProc("spGetActorList").Rows
                               select new Actor()
                               {
                                   ID = row["ActorID"].ToString(),
                                   Name = row["Name"].ToString()
                               }).ToList();
                return Json(ResName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult PopulateProducers()
        {
            try
            {
                var ResName = (from DataRow row in DA.ExecStoreProc("spGetProducersList").Rows
                               select new Actor()
                               {
                                   ID = row["ProducerID"].ToString(),
                                   Name = row["Name"].ToString()
                               }).ToList();
                return Json(ResName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult CreateActor(string name, string sex, string dob, string bio)
        {
            try
            {
                int res = DA.InsertActor(name, sex, dob, bio);
                if (res == 1)
                {
                    return Json('1', JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json('0', JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult CreateProducer(string name, string sex, string dob, string bio)
        {
            try
            {
                int res = DA.InsertActor(name, sex, dob, bio,"producer");
                if (res == 1)
                {
                    return Json('1', JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json('0', JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}