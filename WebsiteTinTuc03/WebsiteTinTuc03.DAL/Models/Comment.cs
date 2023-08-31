using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebsiteTinTuc03.DAL.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ArticleId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDate { get; set; }

    [JsonIgnore]
    public virtual Article? Article { get; set; }

    public virtual User? User { get; set; }
}
