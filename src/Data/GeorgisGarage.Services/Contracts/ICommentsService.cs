using GeorigsGarage.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeorgisGarage.Services.Contracts
{
    public interface ICommentsService
    {
        bool AddCommentToService(string serviceId, User commentator, int rating, string content);

        List<Comment> GetCommentsByServiceId(string id);
        Comment GetCommentById(string commentId);

        bool AddReplyToComment(string commentId, string content, User user);
        bool DeleteCommentAndItsReplies(string commentId);
        bool DeleteReply(string replyId);
    }
}
