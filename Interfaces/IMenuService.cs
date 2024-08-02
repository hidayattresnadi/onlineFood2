using OnlineFoodWebAPI.Models;

namespace OnlineFoodWebAPI.Interfaces
{
    public interface IMenuService
    {
        Menu AddMenu(Menu menu);
        IEnumerable<Menu> GetAllMenu();
        Menu GetMenuById(Guid id);
        Menu UpdateMenu(Menu menu, Guid id); 
        string DeleteMenu(Guid id);
        string AddRating(Guid id, int rating);
    }
}
