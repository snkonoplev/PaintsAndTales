using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Code
{
	public class Generator
	{
		private readonly SoftDeletedApplicationContext _context;
		public Generator(SoftDeletedApplicationContext context)
		{
			_context = context;
		}
		public IReadOnlyCollection<string> GetSitemapNodes(IUrlHelper urlHelper)
		{
			string scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;

			List<string> nodes = new List<string>
			{
				urlHelper.Action("Index", "Home", null, scheme),
				urlHelper.Action("Contacts", "Home", null, scheme),
				urlHelper.Action("Shop", "Shop", null, scheme)
			};
			
			foreach (var product in _context.Set<Product>().ToArray())
			{
				nodes.Add(urlHelper.Action("Product", "Product", new { id = product.Id }, scheme));
			}

			return nodes;
		}

		public string GetSitemapDocument(IEnumerable<string> sitemapNodes)
		{
			XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
			XElement root = new XElement(xmlns + "urlset");

			foreach (string sitemapNode in sitemapNodes)
			{
				XElement urlElement = new XElement(
					xmlns + "url",
					new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode)));
				root.Add(urlElement);
			}

			XDocument document = new XDocument(root);
			return document.ToString();
		}
	}
}
