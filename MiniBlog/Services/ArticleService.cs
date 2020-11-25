using MiniBlog.Interfaces;
using MiniBlog.Model;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private readonly IArticleStore articleStore;
        public ArticleService(IArticleStore articleStore)
        {
            this.articleStore = articleStore;
        }

        internal List<Article> GetAllArticles()
        {
            return articleStore.Articles.ToList();
        }

        internal void AddNewArticle(Article article)
        {
            articleStore.Articles.Add(article);
        }

        internal Article FindArticleById(Guid id)
        {
            return articleStore.Articles.FirstOrDefault(article => article.Id == id);
        }

        internal void RemoveAllArticlesOfUser(User user)
        {
            articleStore.Articles.RemoveAll(a => a.UserName == user.Name);
        }
    }
}
