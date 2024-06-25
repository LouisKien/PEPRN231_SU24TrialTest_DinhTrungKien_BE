using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Implements;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Controllers
{
    public class WatercolorsPaintingController : ODataController
    {
        private readonly IWatercolorsPaintingService _watercolorsPaintingService;

        public WatercolorsPaintingController(IWatercolorsPaintingService watercolorsPaintingService)
        {
            _watercolorsPaintingService = watercolorsPaintingService;
        }

        [Authorize(Policy = "RequireStaffOrManagerRole")]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var paintings = await _watercolorsPaintingService.Get();
                return Ok(paintings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize(Policy = "RequireStaffOrManagerRole")]
        [EnableQuery]
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            try
            {
                var paintings = await _watercolorsPaintingService.Get(key);
                if(paintings == null)
                {
                    return NotFound();
                } else
                {
                    return Ok(paintings);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> Post([FromBody] WatercolorsPainting painting)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _watercolorsPaintingService.Post(painting);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> Put([FromRoute] string key, [FromBody] WatercolorsPainting request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var painting = await _watercolorsPaintingService.Get(key);
                if (painting == null)
                {
                    return NotFound();
                }
                painting.PaintingName = request.PaintingName;
                painting.PaintingDescription = request.PaintingDescription;
                painting.PaintingAuthor = request.PaintingAuthor;
                painting.Price = request.Price;
                painting.PublishYear = request.PublishYear;
                painting.CreatedDate = request.CreatedDate;
                painting.StyleId = request.StyleId;
                await _watercolorsPaintingService.Put(painting);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            try
            {
                var painting = await _watercolorsPaintingService.Get(key);
                if(painting == null)
                {
                    return NotFound();
                }
                await _watercolorsPaintingService.Delete(painting);
                return Ok();
            }
            catch (Exception ex) { 
                return StatusCode(500, $"Internal Server Error: {ex.Message}");

            }
        }

        [Authorize(Policy = "RequireStaffOrManagerRole")]
        [HttpGet("Search")]
        [EnableQuery]
        public async Task<IActionResult> Search([FromQuery] string author, [FromQuery] int? year)
        {
            try
            {
                var response = await _watercolorsPaintingService.Search(author, year);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");

            }
            
        }
    }
}
