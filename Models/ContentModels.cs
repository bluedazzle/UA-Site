using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace test2.Models
{
    public class Content
    {
        [Key]
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Body { get; set; }
        public int VisiteCount { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime LastEditTime { get; set; }
        public bool Enable { get; set; }

        public virtual IdentityUser Publisher { get; set; }
        public virtual ICollection<ContentType> ContentTypes { get; set; }
    }

    public class News : Content
    {
        public string Author { get; set; }
    }
    public class Announcement : Content
    {
        public DateTime DeadLine { get; set; }
    }
    public class Vote : Content
    {
        public bool AllowAnonymous { get; set; }
        public virtual ICollection<VoteItem> VoteItems { get; set; }
    }

    public class VoteItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int VoteCount { get; set; }
        public bool Enable { get; set; }

        public virtual Vote Vote { get; set; }
    }
    public abstract class LogBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        public string IP { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual IdentityUser User { get; set; }
    }
    public class VoteLog : LogBase
    {
        public virtual VoteItem VoteItem { get; set; }
    }

    public class UserLog : LogBase
    {
    }
    public class UploadLog : LogBase
    {
        public virtual UploadedResource UploadedResource { get; set; }
    }

    public class UploadedResource
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Dictionary { get; set; }
        public long Size { get; set; }
        public string FileType { get; set; }
        public string HashCode { get; set; }
        public bool Enable { get; set; }

        public virtual IdentityUser Uploader { get; set; }
        public virtual ICollection<UploadLog> Logs { get; set; }
    }

    public partial class IdentityUser
    {
        public virtual ICollection<VoteLog> MyVotes { get; set; }
        public virtual ICollection<UserLog> UserLog { get; set; }
    }

    public class FriendLink
    {
        [Key]
        public int Id { get; set; }
        public string LinkTittle { get; set; }
        public string LinkAddress { get; set; }
        public bool Enable { get; set; }
    }

    [Table("contents_types")]
    public class ContentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Content> Contents { get; set; }
    }
}