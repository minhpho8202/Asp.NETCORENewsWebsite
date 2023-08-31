using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebsiteTinTuc03.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    [JsonIgnore]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}
