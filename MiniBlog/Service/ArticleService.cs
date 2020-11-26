using MiniBlog.Model;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniBlog.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleStore articleStore;

        public ArticleService(IArticleStore articleStore)
        {
            this.articleStore = articleStore;
        }

        public void AddArticle(Article article)
        {
            articleStore.Articles.Add(article);
        }

        public Article GetArticleById(Guid id)
        {
            return articleStore.Articles.FirstOrDefault(article => article.Id == id);
        }

        public List<Article> GetAllArticles()
        {
            return articleStore.Articles;
        }

        public void DeleteArticleByName(string name)
        {
            articleStore.Articles.RemoveAll(a => a.UserName == name);
        }
    }
}
