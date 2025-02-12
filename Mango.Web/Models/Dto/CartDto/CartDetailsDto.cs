﻿using Mango.Web.Models.Dto.Product;

namespace Mango.Web.Models.Dto.CartDto;

public class CartDetailsDto
{
    public int CartDetailsId { get; set; }
    public int CardHeaderId { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}
