using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EntityFrameworkAuth.Models;
using EntityFrameworkAuth.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace EntityFrameworkAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            ClaimsPrincipal currentUser = this.User;
            var userData = _context.Users.Where(user => user.UserName == currentUser.Identity.Name).SingleOrDefault();

            var hotels = _context.Hotels.ToList();
            var rooms = _context.Rooms.ToList();
            ViewBag.Rooms = rooms;
            ViewBag.Hotels = hotels;
            return View();
        }

        public IActionResult CreateHotel(HotelDataModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            _context.Hotels.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult CreateRoom(int number, string hotelId, bool isBooked)
        {
            var room = new RoomDataModel();
            room.Id = Guid.NewGuid().ToString();
            room.Number = number;
            room.IsBooked = isBooked;

            var hotel = _context.Hotels.Where(hotel => hotel.Id == hotelId).SingleOrDefault();

            var rooms = new List<RoomDataModel>();
            rooms.Add(room);
            hotel.Rooms = rooms;

            _context.Hotels.Update(hotel);
            _context.SaveChanges();

            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult DeleteRoom(string roomId)
        {
            var room = _context.Rooms.Where(room => room.Id == roomId).SingleOrDefault();

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult DeleteHotel(string hotelId)
        {
            var hotel = _context.Hotels.Where(hotel => hotel.Id == hotelId).SingleOrDefault();
            var rooms = _context.Rooms.Where(room => room.HotelDataModelId == hotelId).ToList();

            foreach (var room in rooms)
            {
                _context.Rooms.Remove(room);
            }

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();

            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult UpdateHotel(string hotelId, string name)
        {
            var hotel = _context.Hotels.Where(hotel => hotel.Id == hotelId).SingleOrDefault();

            hotel.Name = name;
            _context.Hotels.Update(hotel);
            _context.SaveChanges();

            return RedirectToAction("Privacy", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
