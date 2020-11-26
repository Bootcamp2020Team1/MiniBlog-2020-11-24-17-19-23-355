using MiniBlog.Stores;
using MiniBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Service
{
    public interface IArticleService
    {
        void AddArticle(Article article);
        Article GetArticleById(Guid id);
        List<Article> GetAllArticles();
        void DeleteArticleByName(string name);
    }
}
