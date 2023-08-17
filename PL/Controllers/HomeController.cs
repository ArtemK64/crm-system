using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _repository;

        public HomeController(IHomeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var table = _repository.GetAllUsers();
            return View(table);
        }

        [HttpPost]
        public IActionResult Index(int id, string firstName, string middleName, string lastName, DateTime birthday)
        {
            _repository.UpdateUsers(id, firstName, middleName, lastName, birthday);
            return RedirectToAction("Index");
        }

        public IActionResult Orders()
        {
            var table = _repository.GetAllOrders();
            return View(table);
        }

        [HttpPost]
        public IActionResult Orders(int id, int userId, int totalPrice, DateTime dateOfOrder)
        {
            _repository.UpdateOrders(id, userId, totalPrice, dateOfOrder);
            return RedirectToAction("Orders");
        }
    }
}