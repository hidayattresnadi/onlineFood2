using Microsoft.Extensions.Logging;
using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Models;
using System.Xml.Linq;

namespace OnlineFoodWebAPI.Services
{
    public class MenuService : IMenuService
    {
        private static List<Menu> Menus = new List<Menu>();

        public Menu AddMenu(Menu menu) {
            var newMenu = new Menu
            {
                Id = Guid.NewGuid(),
                Name = menu.Name,
                Price = menu.Price,
                Category = menu.Category,
                Rating = 0,
                CreatedAt = DateTime.Now,
                IsAvailable = true,
            };
            Menus.Add(newMenu);
            return newMenu;
        }
        public IEnumerable<Menu> GetAllMenu()
        {
            return Menus;
        }
        public Menu GetMenuById(Guid id)
        {
            Menu menu = Menus.FirstOrDefault(foundMenu => foundMenu.Id == id);
            return menu;
        }
        public Menu UpdateMenu(Menu updatedMenu, Guid id)
        {
            int indexMenu = Menus.FindIndex(filteredMenu => filteredMenu.Id == id);
            if (indexMenu == -1)
            {
                return null;
            }
            Menus[indexMenu].Name = updatedMenu.Name;
            Menus[indexMenu].Price = updatedMenu.Price;
            Menus[indexMenu].Category = updatedMenu.Category;
            Menus[indexMenu].IsAvailable = updatedMenu.IsAvailable;
            return Menus[indexMenu];
        }
        public string DeleteMenu(Guid id)
        {
            int indexMenu = Menus.FindIndex(filteredMenu => filteredMenu.Id == id);
            if (indexMenu == -1)
            {
                return "Menu tidak ditemukan";
            }
            Menus.RemoveAt(indexMenu);
            return "Menu Terhapus";
        }
        public string AddRating(Guid id, int rating)
        {
            int indexMenu = Menus.FindIndex(filteredMenu => filteredMenu.Id == id);
            if (indexMenu == -1)
            {
                return "menu tidak ditemukan";
            }
            if(rating < 0 || rating > 5)
            {
                return "gagal menambah rating";
            }
            Menus[indexMenu].RatingCustomers.Add(rating);
            decimal totalRatings = Menus[indexMenu].RatingCustomers.Count();
            decimal sum = Menus[indexMenu].RatingCustomers.Sum();
            Menus[indexMenu].Rating = sum/totalRatings;
            return "berhasil menambah rating";
        }
    }

}