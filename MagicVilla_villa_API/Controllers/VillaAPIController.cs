using AutoMapper;
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
        private readonly IMapper _mapper;
        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationsDBContext applicationsDBContext, IMapper mapper) {
            _logger = logger;
            _applicationsDBContext = applicationsDBContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Getting All villa list");
            IEnumerable<Villa> villlist = await _applicationsDBContext.villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villlist));
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
            return Ok(_mapper.Map<VillaDto>(villrecord));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDto>> CreateVIlla([FromBody]VillaDto CreateDTO)
        {
            //if (!ModelState.IsValid)  // Validations Based on Model State
            //{
            //    return BadRequest(ModelState);
            //}
            if(_applicationsDBContext.villas.FirstOrDefault(u=> u.name.ToLower() == CreateDTO.name.ToLower()) != null)
            {
                ModelState.AddModelError("NameValidation", "Name already exited");
                return BadRequest(ModelState);
            }
            if(CreateDTO == null)
            {
                return BadRequest(CreateDTO);
            }
            if(CreateDTO.id > 0)
            {
                return NotFound(CreateDTO);
            }
            //villaDto.id = _applicationsDBContext.villas.OrderByDescending(u => u.id).FirstOrDefault().id + 1;
            Villa model = _mapper.Map<Villa>(CreateDTO); 
            //Villa model = new()
            //{
            //    id = CreateDTO.id,
            //    name = CreateDTO.name,
            //    detalis = CreateDTO.detalis,
            //    rate = CreateDTO.rate,
            //    sqft = CreateDTO.sqft,
            //    imgurl = CreateDTO.imgurl,
            //    createdate = DateTime.Now,
            //    updateddate = DateTime.Now
                
            //};
            _applicationsDBContext.villas.Add(model);
          await  _applicationsDBContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.id}, model);

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
        public async Task<IActionResult> Update(int id, [FromBody] VillaDto UpdatedDTO)
        {
            if(UpdatedDTO == null || id != UpdatedDTO.id)
            {
                return BadRequest(id);
            }
            var villa = await _applicationsDBContext.villas.FirstOrDefaultAsync(u => u.id == id);
            if(villa == null)
            {
                return NotFound(villa); 
            }
            Villa model = _mapper.Map<Villa>(UpdatedDTO);
            //Villa model = new()
            //{
            //    id = UpdatedDTO.id,
            //    name = UpdatedDTO.name,
            //    detalis = UpdatedDTO.detalis,
            //    rate = UpdatedDTO.rate,
            //    sqft = UpdatedDTO.sqft,
            //    imgurl = UpdatedDTO.imgurl,
            //    createdate = DateTime.Now,
            //    updateddate = DateTime.Now

            //};
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
