using APIPruebas.DTOs;
using APIPruebas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using APIPruebas.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace APIPruebas.Controllers
{


    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductoController : ControllerBase
    {
        private readonly DBAPIContext dbcontext;
        private readonly IMapper mapper;

        public ProductoController(DBAPIContext dbcontext,IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
        }


        [HttpGet]
        [Route("Lista")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductoDTO>>> Lista()
        {
            


            try
            {

                var productos = await dbcontext.Productos.Include(o => o.oCategoria).ToListAsync();
                

                if(productos.Count == 0)
                {
                    return NotFound("La lista esta vacia , Agrega un producto");
                }

                return Ok(productos); 

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public async Task<IActionResult> Obtener(int idProducto)
        {
            var oProducto = await dbcontext.Productos.FindAsync(idProducto);

           if(oProducto == null)
            {
                return BadRequest("Producto no encontrado");

            }

            

            try
            {
                oProducto = await dbcontext.Productos.Include(c => c.oCategoria).
                      Where(p => p.IdProducto == idProducto).FirstOrDefaultAsync();


                return Ok(oProducto);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("NuevoProducto")]
        public async Task <IActionResult> Nuevo(ProductoCreacionDTO productoCreacionDTO)
        {


            try
            {

              

                var producto = mapper.Map<Producto>(productoCreacionDTO);
           



                await dbcontext.Productos.AddAsync(producto);
               await  dbcontext.SaveChangesAsync();


                return Ok();


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("Editar/{idProducto:int}")]
        public async Task<IActionResult> Editar(ProductoCreacionDTO productoDTO, int idProducto)
        {

            var oProducto = await dbcontext.Productos.AnyAsync(x => x.IdProducto == idProducto);

            if (!oProducto)
            {
                return NotFound("Producto no encontrado");

            }


            var productoCategoria = await dbcontext.Productos.AnyAsync(x => x.IdCategoria
            == productoDTO.IdCategoria);

            if (!productoCategoria)
            {
                return NotFound("Categoria no encontrada");
            }

            var ProductoEncontrado = mapper.Map<Producto>(productoDTO);
            ProductoEncontrado.IdProducto = idProducto;
         

            try
            {
               


                dbcontext.Productos.Update(ProductoEncontrado);
               await dbcontext.SaveChangesAsync();

                return Ok();

                

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public async Task<IActionResult> Eliminar(int idProducto)
        {

            var oProducto = await dbcontext.Productos.AnyAsync(x => x.IdProducto == idProducto);

            if (!oProducto)
            {
                return NotFound("Producto no encontrado");

            }

            var ProductoConvertido = await dbcontext.Productos.FindAsync(idProducto);

            if(ProductoConvertido == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {



                dbcontext.Productos.Remove(ProductoConvertido);
                await dbcontext.SaveChangesAsync();

                return Ok();



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


    }
}
