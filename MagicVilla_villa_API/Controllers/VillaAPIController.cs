using MagicVilla_villa_API.Data;
using MagicVilla_villa_API.Migrations;
using MagicVilla_villa_API.Models;
using MagicVilla_villa_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_villa_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //serilog.Asp.net core 
        private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationsDBContext _applicationsDBContext;
        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationsDBContext applicationsDBContext) {
            _logger = logger;
            _applicationsDBContext = applicationsDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Getting All villa list");
            return Ok(await _applicationsDBContext.villas.ToListAsync());
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        //[ProducesResponseType(200, Type = typeof(VillaDto)}]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation("Id is Zero");
                return BadRequest();
            }
            var villrecord = await _applicationsDBContext.villas.FirstOrDefaultAsync(u => u.id == id);
            if (villrecord == null)
            {
                _logger.LogInformation("Id is No Correct");
                return NotFound();
            }
            return Ok(villrecord);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDto>> CreateVIlla([FromBody]VillaDto villaDto)
        {
            //if (!ModelState.IsValid)  // Validations Based on Model State
            //{
            //    return BadRequest(ModelState);
            //}
            if(_applicationsDBContext.villas.FirstOrDefault(u=> u.name.ToLower() == villaDto.name.ToLower()) != null)
            {
                ModelState.AddModelError("NameValidation", "Name already exited");
                return BadRequest(ModelState);
            }
            if(villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(villaDto.id > 0)
            {
                return NotFound(villaDto);
            }
            //villaDto.id = _applicationsDBContext.villas.OrderByDescending(u => u.id).FirstOrDefault().id + 1;
            Villa model = new()
            {
                id = villaDto.id,
                name = villaDto.name,
                detalis = villaDto.detalis,
                rate = villaDto.rate,
                sqft = villaDto.sqft,
                imgurl = villaDto.imgurl,
                createdate = DateTime.Now,
                updateddate = DateTime.Now
                
            };
            _applicationsDBContext.villas.Add(model);
          await  _applicationsDBContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = villaDto.id}, villaDto);

        }
        [HttpDelete("{id:int}", Name ="Deleted")]
        public async Task< IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest(id);
            }
            var vill =  await _applicationsDBContext.villas.FirstOrDefaultAsync(u => u.id == id);
            if(vill == null)
            {
                return NotFound(vill);
            }
            _applicationsDBContext.villas.Remove(vill);
           await _applicationsDBContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}", Name ="UpdatedVilla")]
        public async Task<IActionResult> Update(int id, [FromBody] VillaDto villaDto)
        {
            if(villaDto == null || id != villaDto.id)
            {
                return BadRequest(id);
            }
            var villa = await _applicationsDBContext.villas.FirstOrDefaultAsync(u => u.id == id);
            if(villa == null)
            {
                return NotFound(villa); 
            }
            //villa.name = villaDto.name;
            Villa model = new()
            {
                id = villaDto.id,
                name = villaDto.name,
                detalis = villaDto.detalis,
                rate = villaDto.rate,
                sqft = villaDto.sqft,
                imgurl = villaDto.imgurl,
                createdate = DateTime.Now,
                updateddate = DateTime.Now

            };
            _applicationsDBContext.villas.Update(model);
            _applicationsDBContext.SaveChanges();
            return NoContent();
        }
        // If Working HttpPutch plz install below packges 
        //microsoft.aspnetcore.jsonpatch
        //microsoft.aspnetcore.NewtonsoftJson
        // Add in Program.cs files after addcontroll() method. (.AddNewtonsoftJson());
        //  below code exited line 
        //  {
        //    "path": "string", Propertyname 
        //    "op": "string", Replace
        //    "value": "string" values
        //  }
        //]
        [HttpPatch("{id:int}", Name = "UpdatedVillaPatch")]
        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument<VillaDto> PatchvillaDto)
        {
            if (PatchvillaDto == null || id == 0)
            {
                return BadRequest(id);
            }
            var villa = await _applicationsDBContext.villas.AsNoTracking().FirstOrDefaultAsync(u => u.id == id);
            VillaDto villaDto = new()
            {
                id = villa.id,
                name = villa.name,
                detalis = villa.detalis,
                rate = villa.rate,
                sqft = villa.sqft,
                imgurl = villa.imgurl,
            };
            if (villa == null)
            {
                return NotFound(villa);
            }
            PatchvillaDto.ApplyTo(villaDto, ModelState);
            Villa model = new()
            {
                id = villaDto.id,
                name = villaDto.name,
                detalis = villaDto.detalis,
                rate = villaDto.rate,
                sqft = villaDto.sqft,
                imgurl = villaDto.imgurl,
                updateddate = DateTime.Now

            };
            _applicationsDBContext.villas.Update(model);
           await _applicationsDBContext.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
