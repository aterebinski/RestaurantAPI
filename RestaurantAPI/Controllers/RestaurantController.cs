using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Linq;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize(Policy = "HasNationality")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(RestaurantService restaurantService) {
            _service = restaurantService;
        }

        //[Authorize(Policy = "AtLeast2RestaurantsCreatedByUser")]
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll([FromQuery] string? searchPhrase)
        {
            var restaurantsDTO = _service.GetAll(searchPhrase);
            return StatusCode(200, restaurantsDTO);

        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<RestaurantDTO> Get([FromRoute] int id)
        {
            var restaurant = _service.GetById(id);

            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult CreateRestautant([FromBody] CreateRestaurantDTO dto)
        {
            int userId = int.Parse(User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value);
            int restaurantId = _service.Create(dto);
            return Created($"/api/restaurant/{restaurantId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _service.Delete(id);
                return NoContent();

        }

        [HttpPut("{Id}")]
        public ActionResult Update([FromBody] RestaurantDTO dto, [FromRoute] int Id)
        {
            _service.Update(Id, dto);
            return Ok();
        }
    }
}
