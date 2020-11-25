using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleStore articleStore;
        private readonly IUserStore userStore;
        public ArticleController(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleStore.Articles.ToList();
        }

        [HttpPost(Name = "Create")]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!userStore.Users.Exists(_ => article.UserName == _.Name))
                {
                    userStore.Users.Add(new User(article.UserName));
                }

                articleStore.Articles.Add(article);
            }

            var name = nameof(GetById);
            return CreatedAtAction("Create", new { id = article.Id.ToString() });
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            var foundArticle = articleStore.Articles.FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}