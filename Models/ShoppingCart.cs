namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }
		public int GetTotalQuantity()
		{
			return Items.Sum(item => item.Quantity);
		}

		public decimal GetTotalPrice()
		{
			return Items.Sum(item => item.Price * item.Quantity);
		}
		// Các phương thức khác...
	}
}
