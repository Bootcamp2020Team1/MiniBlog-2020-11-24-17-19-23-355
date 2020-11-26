using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private readonly IArticleStore articleStore;

        public ArticleService(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
        }

        public void AddArticle(Article article)
        {
            articleStore.Articles.Add(article);
        }

        public List<Article> GetAllArticles()
        {
            return articleStore.Articles;
        }

        public Article GetArticleById(Guid id)
        {
            return articleStore.Articles.FirstOrDefault(article => article.Id == id);
        }

        public void DeleteArticleByName(string name)
        {
            articleStore.Articles.RemoveAll(article => article.UserName == name);
        }
    }
}
