using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        //private MoviesDBModel _db = new MoviesDBModel();
       string Baseurl = "http://movieapiservice20171230012316.azurewebsites.net/";
       // string Baseurl = "http://localhost:56290/";
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Movie> lstMovie = new List<Movie>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Movie/GetMovieDetails");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    lstMovie = JsonConvert.DeserializeObject<List<Movie>>(EmpResponse);

                }
                //returning the employee list to view
                
            }

            return View(lstMovie);
        }

       // GET: Home/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Movie movieToView = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Movie/GetMovie/" + id.ToString());

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    movieToView = JsonConvert.DeserializeObject<Movie>(MovieResponse);

                }
            }

            return Redirect("http://www.imdb.com/find?ref_=nv_sr_fn&q=" + movieToView.Title + "&s=all");
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Create([Bind(Exclude = "Id")] Movie movieToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            //_db.Movies.Add(movieToCreate);

            //try
            //{ _db.SaveChanges(); }
            //catch (Exception ex)
            //{ }


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
              //  HttpResponseMessage Res = await client.GetAsync("api/Movie/AddMovie");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Movie/AddMovie", movieToCreate);
                response.EnsureSuccessStatusCode();
                //Checking the response is successful or not which is sent using HttpClient
                //if (Res.IsSuccessStatusCode)
                //{
                //    //Storing the response details recieved from web api 
                //    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //    //Deserializing the response recieved from web api and storing into the Employee list
                //    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);

                //}
                ////returning the employee list to view
                //return View(EmpInfo);
            }


            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //var movieToEdit = (from m in _db.Movies
            //                   where m.Id == id
            //                   select m).First();
            Movie movieToEdit=null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Movie/GetMovie/"+id.ToString());

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                   movieToEdit = JsonConvert.DeserializeObject<Movie>(MovieResponse);

                }
            }

            return View(movieToEdit);
        }

        // POST: Home/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public async  Task<ActionResult> Edit(Movie movieToEdit)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                //  HttpResponseMessage Res = await client.GetAsync("api/Movie/AddMovie");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Movie/UpdateMovie", movieToEdit);
                response.EnsureSuccessStatusCode();
                //Checking the response is successful or not which is sent using HttpClient
                //if (Res.IsSuccessStatusCode)
                //{
                //    //Storing the response details recieved from web api 
                //    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //    //Deserializing the response recieved from web api and storing into the Employee list
                //    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);

                //}
                ////returning the employee list to view
                //return View(EmpInfo);
            }
            return RedirectToAction("Index");

        }

        //GET: Home/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Delete(Movie movieTodel)
        {
            int id = movieTodel.Id;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Movie/DeleteMovie/" + id.ToString());

                //Checking the response is successful or not which is sent using HttpClient
                Res.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            Movie movieToDelete = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Movie/GetMovie/" + id.ToString());

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    movieToDelete = JsonConvert.DeserializeObject<Movie>(MovieResponse);

                }
            }
            return View(movieToDelete);
        }



        ////Currently unused. Might implement confirmation box later
        //// POST: Home/Delete/5
        //[HttpPost]
        //public ActionResult Delete()
        //{

        //    _db.Movies.Remove();


        //    return RedirectToAction("Index");
        //}
    }
}
