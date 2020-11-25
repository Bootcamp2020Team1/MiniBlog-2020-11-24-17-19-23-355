using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Service;
namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IArticleService articleService;
        public ArticleController(IUserService userService, IArticleService articleService)
        {
            this.userService = userService;
            this.articleService = articleService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return this.articleService.GetAllArticles();
        }

        [HttpPost(Name = "Create")]
        public ActionResult<Article> Create(Article newArticle)
        {
            if (newArticle.UserName != null)
            {
                userService.AddUserByName(newArticle.UserName);

                articleService.AddArticle(newArticle);
            }

            return CreatedAtAction(nameof(Create), new { id = newArticle.Id.ToString() });
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {     
            return articleService.GetArticleById(id);
        }
    }
}