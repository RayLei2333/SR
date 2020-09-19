using SR.DTO;
using SR.DTO.Req;
using SR.DTO.Resp;
using SR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Business
{
    public interface IArticleService
    {
        ResponseModel GetArticle(string id);

        ResponseModel GetArticleList(ArticleQuery model);

        ResponseModel AddOrUpdate(ArticleDTO model);

        ResponseModel ReleaseArticle(string articleId);

        ResponseModel DeleteArticle(string articleId);

        ResponseModel SignArticle(string articleId, string detailId);

        ResponseModel CancelSignArticle(string articleId, string detailId);

        ResponseModel SignedArticle(string articleId, string detailId);

        ResponseModel<List<T_Article_Signed>> BatchSignedArticle(List<string> userList, string articleId);

        ResponseModel GetUserArticleStatus(string articleId);

        ResponseModel AddComment(ArticleCommentDTO model, List<string> mediaUrl);

        ResponseModel GetCommentList(int pageIndex, int pageSize, string articleId);

        ResponseModel GetUserSignArticle(int pageIndex, int pageSize);

        ResponseModel GetUserSignedArticle(int pageIndex, int pageSize);
    }
}
