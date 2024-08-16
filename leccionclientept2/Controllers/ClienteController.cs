using leccionclientept2.Modelos;
using Microsoft.AspNetCore.Mvc;
using static leccionclientept2.Comunes.ConexionDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace leccionclientept2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // GET: api/<ClienteController>
        [HttpGet]
        public List<Cliente> Get()
        {
            return ConexionBD.GetClientes();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public Cliente Get(int id)
        {
            return ConexionBD.GetCliente(id);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public void Post([FromBody] Cliente objCliente)
        {
            ConexionBD.PostCliente(objCliente);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cliente objCliente)
        {
            ConexionBD.PutCliente(id, objCliente);
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{idCliente}/{idUsuarioModificacion}")]
        public void Delete(int idCliente, int idUsuarioModificacion)
        {
            ConexionBD.DeleteCliente(idCliente, idUsuarioModificacion);
        }
    }
}
