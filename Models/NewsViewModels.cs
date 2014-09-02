using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace test2.Models
{
    public class ListNewsViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "标题")]
        public string Tittle { get; set; }
        [Display(Name = "作者")]
        public string Author { get; set; }
        [Display(Name = "发布时间")]
        public DateTime Time { get; set; }
        [Display(Name = "摘要")]
        public string Summery { get; set; }
        [Display(Name = "图片")]
        public string Picture { get; set; }
    }
    public class NewsDetailModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "标题")]
        public string Tittle { get; set; }
        [Display(Name = "作者")]
        public string Author { get; set; }
        [Display(Name = "正文")]
        public string Body { get; set; }
        [Display(Name = "点击量")]
        public int VisiteCount { get; set; }
        [Display(Name = "发布时间")]
        public DateTime Time { get; set; }
    }
    public class InsertNewsModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Tittle { get; set; }
        [Required]
        [Display(Name = "作者")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "正文")]
        [AllowHtml]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}