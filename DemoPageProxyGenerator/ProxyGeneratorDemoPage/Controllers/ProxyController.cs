﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProxyGenerator.ProxyTypeAttributes;
using ProxyGeneratorDemoPage.Helper;
using ProxyGeneratorDemoPage.Models.Person.Models;

namespace ProxyGeneratorDemoPage.Controllers
{
    public class ProxyController : Controller
    {
        #region Views
        public ActionResult AngularCalls()
        {
            return View();
        }
        #endregion

        #region AngularJs Proxy Methods Examples
        [CreateAngularJsProxy]
        public JsonResult AddJsEntryOnly(Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult AddJsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke =  name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult AddJsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult ClearJsCall()
        {
            return Json("ClearJsCall was Called", JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult LoadJsCallById(int id)
        {
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult LoadJsCallByParams(string name, string vorname, int alter)
        {
            return Json(vorname, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult LoadJsCallByParamsAndId(string name, string vorname, int alter, int id)
        {
            return Json(new Person() { Name = name, Id = id}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult LoadJsCallByParamsWithEnum(string name, string vorname, int alter, ClientAccess access)
        {
            return Json(new Person() { Name = name, Id = alter}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AngularTs Proxy Methods Examples
        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult AddTsEntryOnly(Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallById(int id)
        {
            return Json(new Person() { Id = id}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallByParams(string name, string vorname, int alter)
        {
            return Json(new Person() { Name = name, Id = alter}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult LoadTsCallByParamsAndId(string name, string vorname, int alter, int id)
        {
            return Json(new Auto() { Alter = alter, Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult LoadTsCallByParamsWithEnum(string name, string vorname, int alter, ClientAccess access)
        {
            return Json(new Auto() { Marke = name, Alter = alter}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(List<Auto>))]
        public JsonResult LoadAllAutosListe(string name)
        {
            return Json(new List<Auto>() { new Auto() { Marke = name}, new Auto() }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto[]))]
        public JsonResult LoadAllAutosArray(string name)
        {
            return Json(new List<Auto>() { new Auto() { Marke = name}, new Auto() }.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult ClearTsCall()
        {
            return Json(new Person(), JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(void))]
        public JsonResult VoidTsReturnType(string name)
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(string))]
        public JsonResult StringTsReturnType(string name)
        {
            return Json(name, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(int))]
        public JsonResult IntegerTsReturnType(int age)
        {
            return Json(age, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(DateTime))]
        public JsonResult DateTsReturnType(string name)
        {
            return Json(DateTime.Now, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Boolean))]
        public JsonResult BoolTsReturnType(bool boolValue)
        {
            return Json(boolValue, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
