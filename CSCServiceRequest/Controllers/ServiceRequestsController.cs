using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSCServiceRequest.Models;
using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CSCServiceRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public ServiceRequestsController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/ServiceRequests
        /// <summary>
        /// Retorna lista de chamados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequest()
        {
            return await _context.ServiceRequest.ToListAsync();
        }

        // GET: api/ServiceRequests/5
        /// <summary>
        /// Retorna chamado pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest(long id)
        {
            var serviceRequest = await _context.ServiceRequest.FindAsync(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return serviceRequest;
        }

        // PUT: api/ServiceRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Edita chamado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequest(long id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.ServiceRequestId)
            {
                return BadRequest();
            }

            _context.Entry(serviceRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ServiceRequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Cadastra chamado e adiciona na fila do kafka
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest serviceRequest)
        {
            _context.ServiceRequest.Add(serviceRequest);
            await _context.SaveChangesAsync();

            await PutOnKafkaQueue(serviceRequest);

            return CreatedAtAction("GetServiceRequest", new { id = serviceRequest.ServiceRequestId }, serviceRequest);
        }

        // DELETE: api/ServiceRequests/5
        /// <summary>
        /// Deleta chamado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRequest>> DeleteServiceRequest(long id)
        {
            var serviceRequest = await _context.ServiceRequest.FindAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            _context.ServiceRequest.Remove(serviceRequest);
            await _context.SaveChangesAsync();

            return serviceRequest;
        }

        private bool ServiceRequestExists(long id)
        {
            return _context.ServiceRequest.Any(e => e.ServiceRequestId == id);
        }


        /// <summary>
        /// Adiciona o item na fila do Kafka
        /// </summary>
        /// <returns></returns>
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PutOnKafkaQueue(ServiceRequest item)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _configuration["KafkaServer"]
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();

            using (producer)
            {
                try
                {
                    var result = await producer.ProduceAsync("csc_new_support", new Message<Null, string> { Value = JsonSerializer.Serialize(item) });

                    return true;
                }
                catch (ProduceException<Null, string> ex)
                {
                    throw ex;
                }
            }
        }
    }
}
