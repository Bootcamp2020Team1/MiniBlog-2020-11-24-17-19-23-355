using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Interfaces;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly UserService userService;
        private readonly ArticleService articleService;
        public ArticleController(UserService userService, ArticleService articleService)
        {
            this.userService = userService;
            this.articleService = articleService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleService.GetAllArticles();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            if (article.UserName != null)
            {
                userService.AddNewUser(article.UserName);

                articleService.AddNewArticle(article);
            }

            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            Article foundArticle = articleService.FindArticleById(id);
            return foundArticle;
        }
    }
}