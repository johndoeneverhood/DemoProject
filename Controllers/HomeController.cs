using DemoProject.Models.Common;
using DemoProject.Models.IRepo;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DemoProject.Controllers;
public class HomeController : Controller
{
    IHomeRepo _repo;
    public HomeController(IHomeRepo homeRepo)
    {
        _repo = homeRepo;
    }

    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> Index(string detailNum)
    {
        List<GroupBrandItem> Model = new List<GroupBrandItem>();

        if (detailNum != null)
        {
            Model = await _repo.GetOriginalGroups(detailNum);
            ViewBag.DetailNum = detailNum;

            if (Model?.Count() == 0)
            {
                ViewBag.Error = "ничего не найдено или ошибка авторизации";
            }
        }

        return View(Model);
    }
}
