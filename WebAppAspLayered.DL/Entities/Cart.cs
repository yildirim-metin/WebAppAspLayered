﻿namespace WebAppAspLayered.DL.Entities;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public List<CartItem> Items { get; set; } = [];
}
