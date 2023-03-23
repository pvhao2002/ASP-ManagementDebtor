using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Management_Debt.Models;

namespace Management_Debt.Areas.Admin.Controllers
{
	public class AdminHomeController : Controller
	{
		// GET: Admin/AdminHome
		public ActionResult Index(int? page, int? status, string find)
		{
			using (DBContext1 db = new DBContext1())
			{
				page = page ?? 1;
				status = status ?? 3;
				find = find ?? "";
				var link = (from l in db.Debtor where l.full_name.Contains(find) select l).OrderBy(l => l.did);

				if (status == 0)
				{
					link = (from l in db.Debtor where l.status == 0 && l.full_name.Contains(find) select l).OrderBy(l => l.did);
				}
				else if (status == 1)
				{
					link = (from l in db.Debtor where l.status == 1 && l.full_name.Contains(find) select l).OrderBy(l => l.did);
				}
				Session["status"] = status;
				//Response.Write("<script>alert('" + status + "')</script>");
				int pageSize = 11;
				int pageNumber = (page ?? 1);
				return View(link.ToPagedList(pageNumber, pageSize));
			}
		}

		[HttpGet]
		public ActionResult addDebtor()
		{
			return View();
		}

		[HttpPost]
		public ActionResult addDebtor(Debtor debtor)
		{
			using (DBContext1 db = new DBContext1())
			{
				var item = db.Debtor.FirstOrDefault(m => m.did == debtor.did || m.email == debtor.email);
				if (item == null)
				{
					// Add data to the particular table
					ViewBag.message = "Thêm thành công!";
					ViewBag.m = "succ";
					db.Debtor.Add(debtor);
					db.SaveChanges();
				}
				else
				{
					ViewBag.message = "Người nợ này đã tồn tại!";
					ViewBag.m = "error";
				}
			}
			return View("addDebtor");
		}

		[HttpGet]
		public ActionResult updateDebtor(string id)
		{
			using (DBContext1 db = new DBContext1())
			{
				var item = db.Debtor.FirstOrDefault(m => m.did == id);

				return View(item);

			}
		}

		[HttpPost]
		public ActionResult updateDebtor(string id, Debtor debtor)
		{
			using (DBContext1 db = new DBContext1())
			{
				var checkDuplicate = db.Debtor.FirstOrDefault(z => z.did != id && z.email == debtor.email);
				if (checkDuplicate == null)
				{
					var item = db.Debtor.FirstOrDefault(m => m.did == id);
					item.full_name = debtor.full_name;
					item.status = debtor.status;
					item.address = debtor.address;
					item.birthday = debtor.birthday;
					db.SaveChanges();
				}
				return RedirectToAction("Index");

			}

		}
		[HttpGet]
		public ActionResult removeDebtor(string id)
		{
			using (DBContext1 db = new DBContext1())
			{
				var item = db.Debtor.FirstOrDefault(m => m.did == id);
				if (item != null)
				{
					db.Debtor.Remove(item);
					db.SaveChanges();
				}
				return RedirectToAction("Index");
	
			}
		}

		[HttpPost]
		public ActionResult SearchByName(FormCollection collection)
		{
			string find = collection["search"].ToString();
			return RedirectToAction("Index", new {find});
		}
	}
}