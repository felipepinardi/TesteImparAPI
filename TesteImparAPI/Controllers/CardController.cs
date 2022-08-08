using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteImparAPI.Controllers
{
    [Route("cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpGet]
        [Route("getAll")]
        [Route("")]
        public List<Card> GetAll()
        {
            return Card.GetAll();
        }

        [HttpPost]
        [Route("create")]
        [Route("")]
        public Card AddCard([FromBody] Card item)
        {
            return item.Create();
        }

        [HttpPut]
        [Route("{id}")]
        public Card UpdateCard(int id, [FromBody] Card item)
        {
            item.CardId = id;
            return item.Update();
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteCard(int id)
        {
            Card.Delete(id);
        }
    }
}
