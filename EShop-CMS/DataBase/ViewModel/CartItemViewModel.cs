namespace EShop_CMS.DataBase.ViewModel
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImgName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsPresent { get; set; }

        public int Subtotal => Price * Quantity;
    }

    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public int TotalItems => Items.Sum(i => i.Quantity);
        public int Subtotal => Items.Sum(i => i.Subtotal);
        public int ShippingCost => Subtotal >= 2000000 || Subtotal == 0 ? 0 : 150000;
        public int Total => Subtotal + ShippingCost;
    }
}
