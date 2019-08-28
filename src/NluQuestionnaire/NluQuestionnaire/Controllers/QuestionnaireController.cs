using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
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
        public IActionResult Post([FromBody]EvaluateEntity value)
        {
            QuestionnaireService questionnaireService = new QuestionnaireService();
            int result= questionnaireService.EvaluateAdd(value);
            if (result > 0)
            {
                return Ok(new { result = "Success." });
            }
            else {
                return Ok(new { result = "Fail." });
            }
        }
        [HttpGet]
        public string Get() {
            QuestionnaireService questionnaireService = new QuestionnaireService();
            return questionnaireService.GetCourseName();
        }
        [HttpGet("{code}")]
        public string Get(string code)
        {
            var appid = "wx72c87578b8d8fea7";
            var secret = "deb2089f6f12e579a57441024b8c5f5c";
            var url = @"https://api.weixin.qq.com/sns/jscode2session?appid="
                + appid + "&secret=" + secret + "&js_code=" + code + "&grant_type=authorization_code";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ReadWriteTimeout = 5000;
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            string key = "\"openid\":\"";
            int startIndex = retString.IndexOf(key);

            int endIndex = retString.IndexOf("\"}", startIndex);
            string openid = retString.Substring(startIndex + key.Length, endIndex - startIndex - key.Length);
            return openid;

        }
    }
}