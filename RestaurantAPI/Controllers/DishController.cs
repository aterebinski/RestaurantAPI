using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService service)
        {
            _dishService = service;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDTO dto)
        {
            int dishId = _dishService.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/dish/{dishId}",null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDTO> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDTO dishDto = _dishService.getDishById(restaurantId, dishId);
            return Ok(dishDto);
        }

        [HttpGet] 
        public ActionResult<IEnumerable<DishDTO>> GetAll([FromRoute] int restaurantId){
            IEnumerable<DishDTO> dishes = _dishService.GetAll(restaurantId);
            return Ok(dishes);
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int restaurantId)
        {
            bool isDeleted = _dishService.DeleteAll(restaurantId);
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _dishService.Delete(restaurantId, dishId);
            return NoContent();
        }
        
    }
}
