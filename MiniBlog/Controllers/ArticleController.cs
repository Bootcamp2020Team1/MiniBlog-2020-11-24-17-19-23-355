using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService articleService;
        private readonly UserService userService;

        public ArticleController(ArticleService articleService, UserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return ArticleStoreWillReplaceInFuture.Articles.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            if (article.UserName != null)
            {
                this.userService.RegisterUserByName(article.UserName);

                articleService.AddArticle(article);
            }

            var check = CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
            return check;
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            return articleService.GetArticleById(id);
        }
    }
}