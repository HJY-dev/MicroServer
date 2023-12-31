using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// 文章
/// </summary>
public class Article
{

    public long Id { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 摘要
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    [MaxLength(-1)]
    public string Content { get; set; } = null!;

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; } = null!;

    /// <summary>
    /// 来源
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// 来源地址
    /// </summary>
    public string? SourceUrl { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    public string? Cover { get; set; }

    /// <summary>
    /// 浏览量
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// 点赞量
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// 评论量
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 是否推荐
    /// </summary>
    public bool IsRecommend { get; set; }

    /// <summary>
    /// 是否发布
    /// </summary>
    public bool IsPublish { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    public Article(string title, string? summary, string content, string author, string? source, string? sourceUrl,
        string? cover)
    {
        Title = title;
        Summary = summary;
        Content = content;
        Author = author;
        Source = source;
        SourceUrl = sourceUrl;
        Cover = cover;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="title"></param>
    /// <param name="summary"></param>
    /// <param name="content"></param>
    /// <param name="author"></param>
    /// <param name="source"></param>
    /// <param name="sourceUrl"></param>
    /// <param name="cover"></param>
    /// <param name="modifiedUserId"></param>
    public void Update(string title, string? summary, string content, string author, string? source, string? sourceUrl,
        string? cover, string? modifiedUserId)
    {
        Title = title;
        Summary = summary;
        Content = content;
        Author = author;
        Source = source;
        SourceUrl = sourceUrl;
        Cover = cover;
    }

    /// <summary>
    /// 推荐
    /// </summary>
    /// <param name="modifiedUserId"></param>
    public void Recommend(string? modifiedUserId)
    {
        IsRecommend = true;
    }

    /// <summary>
    /// 取消推荐
    /// </summary>
    /// <param name="modifiedUserId"></param>
    public void UnRecommend(string? modifiedUserId)
    {
        IsRecommend = false;
    }

    /// <summary>
    /// 置顶
    /// </summary>
    /// <param name="modifiedUserId"></param>
    public void Top(string? modifiedUserId)
    {
        IsTop = true;
    }

    /// <summary>
    /// 取消置顶
    /// </summary>
    /// <param name="modifiedUserId"></param>
    public void UnTop(string? modifiedUserId)
    {
        IsTop = false;
    }

    /// <summary>
    /// 发布
    /// </summary>
    /// <param name="modifiedUserId"></param>
    public void Publish(string? modifiedUserId)
    {
        IsPublish = true;
        PublishTime = DateTime.Now;
    }

    /// <summary>
    /// 取消发布
    /// </summary>
    /// <param name="modifiedUserId"></param>
    public void UnPublish(string? modifiedUserId)
    {
        IsPublish = false;
        PublishTime = null;
    }
}