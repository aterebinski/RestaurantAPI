using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDTO dto)
        {
            int dishId = _service.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/dish/{dishId}",null);
        }


    }
}
