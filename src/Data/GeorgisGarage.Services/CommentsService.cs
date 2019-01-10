using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeorgisGarage.Data;
using GeorgisGarage.Services.Contracts;
using GeorigsGarage.Data.Models;

namespace GeorgisGarage.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext context;
        private readonly IServicesService servicesService;

        public CommentsService(IServicesService servicesService, ApplicationDbContext context)
        {
            this.servicesService = servicesService;
            this.context = context;
        }
        public bool AddCommentToService(string serviceId, User commentator, int rating, string content)
        {
            var service = this.servicesService.GetServiceById(serviceId);

            if (service == null)
            {
                return false;
            }

            var comment = new Comment
            {
                Content = content,
                Rating = rating,
                Service = service,
                ServiceId = service.Id,
                Replies = new List<Reply>(),
                User = commentator,
                PostedOn = DateTime.UtcNow
            };

            service.Comments.Add(comment);

            this.context.Comments.Add(comment);
            this.context.Update(service);
            this.context.SaveChanges();

            return true;
        }

        public List<Comment> GetCommentsByServiceId(string id)
        {
            var service = this.servicesService.GetServiceById(id);

            if (service == null)
            {
                return null;
            }

            var comments = service.Comments.ToList();

            return comments;
        }

        public bool AddReplyToComment(string commentId, string content, User user)
        {
            var comment = GetCommentById(commentId);

            if (comment == null)
            {
                return false;
            }

            var reply = new Reply { Comment = comment, Content = content, User = user, PostedOn = DateTime.UtcNow };

            comment.Replies.Add(reply);
            this.context.Replies.Add(reply);
            this.context.Update(comment);
            this.context.SaveChanges();

            return true;
        }

        public Comment GetCommentById(string commentId)
        {
            var comment = this.context.Comments.FirstOrDefault(x => x.Id == commentId);

            return comment;
        }
        

        private List<Reply> GetRepliesByCommentId(string commentId)
        {
            var comment = this.GetCommentById(commentId);

            if (comment == null)
            {
                return null;
            }

            var replies = this.context.Replies.Where(x => x.CommentId == commentId).ToList();

            return replies;
        }

        public bool DeleteCommentAndItsReplies(string commentId)
        {
            var comment = GetCommentById(commentId);

            if (comment == null)
            {
                return false;
            }

            var replies = GetRepliesByCommentId(commentId);

            if (replies.Count != 0)
            {
                foreach (var reply in replies)
                {
                    reply.IsDeleted = true;
                }

                this.context.UpdateRange(replies);
            }

            comment.IsDeleted = true;

            this.context.Update(comment);
            this.context.SaveChanges();
            return true;
        }

        public bool DeleteReply(string replyId)
        {
            var reply = GetReplyById(replyId);

            if (reply == null)
            {
                return false;
            }

            reply.IsDeleted = true;
            this.context.Update(reply);
            this.context.SaveChanges();
            return true;
        }

        private Reply GetReplyById(string replyId)
        {
            return this.context.Replies.FirstOrDefault(x => x.Id == replyId);
        }
    }
}