using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private LuckySpinDataContext _dbc;
        Random random;

        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinDataContext db)
        {
            random = new Random();
            _dbc = db;
        }

        /***
         * Entry Page Action
         **/

        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }

        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (!ModelState.IsValid) { return View(); }

            _dbc.Players.Add(player);
            _dbc.SaveChanges();

            SpinViewModel spin = new SpinViewModel
            {
                FirstName = player.FirstName,
                Luck = player.Luck,
                Balance = player.Balance
            };
            // TODO: BONUS: Build a new SpinItViewModel object with data from the Player and pass it to the View

            return RedirectToAction("SpinIt", spin);
        }

        /***
         * Spin Action
         **/  
               
         public IActionResult SpinIt(SpinViewModel spin)
        {

            spin.A = random.Next(1, 10);
            spin.B = random.Next(1, 10);
            spin.C = random.Next(1, 10);



            spin.IsWinning = (spin.A == spin.Luck || spin.B == spin.Luck || spin.C == spin.Luck);

            Spin spinspin = new Spin()
            {
                IsWinning = spin.IsWinning
            };
            _dbc.Spins.Add(spinspin);
            _dbc.SaveChanges();

            //Add to Spin Repository
            //repository.AddSpin(spin);

            //Prepare the View
            if(spin.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";

            //ViewBag.FirstName = player.FirstName;

            return View("SpinIt", spin);
        }

        /***
         * ListSpins Action
         **/

         public IActionResult LuckList()
        {
                return View();
        }

    }
}

