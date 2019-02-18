using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NluQuestionnaire.Data;
using NluQuestionnaire.Service;

namespace NluQuestionnaire.Controllers
{
    [Produces("application/json")]
    [Route("api/Questionnaire")]
    public class QuestionnaireController : Controller
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody]Questionnaire value)
        {
            QuestionnaireService questionnaireService = new QuestionnaireService();
            questionnaireService.Create(value);
        }
        [HttpGet]
        public string Get() {
            return "Service is ok.";
        }
    }
}