using Microsoft.AspNetCore.Mvc;
using TestRMQ.BL;
using TestRMQ.DAO;

namespace TestRMQ.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private readonly IRabbitMQBusinessLogic _rabbitMQBL;


        public RabbitMQController(IRabbitMQBusinessLogic rabbitMQBL)
        {
            _rabbitMQBL = rabbitMQBL;
        }

        [HttpPost("createQueue", Name = "crtQueue")]
        public IActionResult createQueue(string queueName)
        {
            return _rabbitMQBL.createQueue(queueName) == true ? Ok("RabbitMQ: Queue aggiunta con successo.") : BadRequest("RabbitMQ: Errore durante la creazione della Queue.");
        }

        [HttpDelete("deleteQueue", Name = "delQueue")]
        public IActionResult deleteQueue(string queueName)
        {
            return _rabbitMQBL.deleteQueue(queueName) == true ? Ok("RabbitMQ: Queue eliminata con successo.") : BadRequest("RabbitMQ: Errore durante la creazione della Queue.");
        }

        [HttpPost("sendMessage", Name = "QueueMessage")]
        public IActionResult publish(string msg)
        {
            return _rabbitMQBL.publish(msg) == true ? Ok("RabbitMQ: Messaggio aggiunto alla coda.") : BadRequest("RabbitMQ: Errore durante l'invio in coda.");
        }

        [HttpGet("getMessages", Name = "getMessages")]
        public IActionResult getMessages(string queueName, int maxMessages)
        {
            var messages = _rabbitMQBL.getMessages(queueName, maxMessages);

            if (messages == null || !messages.Any())
            {
                return NotFound("Nessun messaggio trovato.");
            }

            return Ok(messages);
        }



    }
}
