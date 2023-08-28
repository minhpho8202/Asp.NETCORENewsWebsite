using System;
using System.Collections.Generic;

namespace WebsiteTinTuc03.DAL.Models;

public partial class Like
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ArticleId { get; set; }

    public virtual Article? Article { get; set; }

    public virtual User? User { get; set; }
}
