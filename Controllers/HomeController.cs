using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UrlShorterner.DAL;
using UrlShorterner.Helpers;
using UrlShorterner.Models;

namespace UrlShorterner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        #region View Methods
        public IActionResult Index()
        {
            //get 10 most recent urls 
            var top10 = GetRecentUrls(5); 
            return View(top10);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Url Methods
        [HttpPost]
        public IActionResult CreateUrl(string url)
        {
            //check valid url: can also be checked on front end 
            Uri uriResult;
            if (url == null)
            {
                return Json(new
                {
                    validUrl = false,
                    message = "Please enter a URL",
                });
            }
            else if (!Uri.TryCreate(url, UriKind.Absolute, out uriResult) ||
    !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return Json(new
                {
                    validUrl = false,
                    message = "Please enter a valid Url and remember to include HTTP / HTTPS",
                    originalUrl = url
                });
            }

            var newUrl = this.SaveUrl(url);
            return Json(new { validUrl = true, url = newUrl });
        }

      

        public Url SaveUrl(string url)
        {
            // create url object
            var newUrl = new Url
            {
                OriginalUrl = url,
                CreatedDate = DateTime.UtcNow
            };
            //add to db 
            _db.Urls.Add(newUrl);
            _db.SaveChanges();
            //add shortUrl 
            var encodedId = EncodingHelper.Encode(newUrl.UrlID);
            newUrl.ShortUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{encodedId}";

            _db.Urls.Update(newUrl);
            _db.SaveChanges();
            return newUrl;
        }

        public void ProcessShortUrl()
        {
            //get short url subdirectory from httpcontext
            var encodedValue = HttpContext.Request.Path.ToUriComponent().Trim('/');
            var urlId = EncodingHelper.Decode(encodedValue);
            //get full url 
            var urlEntry = _db.Urls.Find(urlId);
            //redirect to long url 
            if (urlEntry != null)
            {
                HttpContext.Response.Redirect(urlEntry.OriginalUrl);
            }
            else
            {
                HttpContext.Response.Redirect("/");
            }
        }

        public List<Url> GetRecentUrls(int numUrl)
        {
            return _db.Urls.OrderByDescending(d => d.CreatedDate).Take(numUrl).ToList();
        }
        #endregion

      
    }
}
