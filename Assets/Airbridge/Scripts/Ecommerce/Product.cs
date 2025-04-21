using System.Collections.Generic;

namespace Airbridge.Ecommerce
{
    public class Product
    {
        private string idKey        = "productID";
        private string nameKey      = "name";
        private string currencyKey  = "currency";
        private string priceKey     = "price";
        private string quantityKey  = "quantity";
        private string positionKey  = "position";

        private string categoryIdKey    = "categoryID";
        private string categoryNameKey  = "categoryName";
        private string brandIdKey       = "brandID";
        private string brandNameKey     = "brandName";

        private Dictionary<string, object> data = new Dictionary<string, object>();

        public void SetId(string id)
        {
            AddData(idKey, id);
        }

        public void SetName(string name)
        {
            AddData(nameKey, name);
        }

        public void SetCurrency(string currency)
        {
            AddData(currencyKey, currency);
        }

        public void SetPrice(double price)
        {
            AddData(priceKey, price);
        }

        public void SetQuantity(int quantity)
        {
            AddData(quantityKey, quantity);
        }

        public void SetPosition(int position)
        {
            AddData(positionKey, position);
        }
        
        public void SetCategoryId(string categoryId)
        {
            AddData(categoryIdKey, categoryId);
        }
        
        public void SetCategoryName(string categoryName)
        {
            AddData(categoryNameKey, categoryName);
        }
        
        public void SetBrandId(string brandId)
        {
            AddData(brandIdKey, brandId);
        }
        
        public void SetBrandName(string brandName)
        {
            AddData(brandNameKey, brandName);
        }
        
        public Dictionary<string, object> ToDictionary()
        {
            return data;
        }

        public void AddData(string key, object value)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, value);
            }
            else
            {
                data[key] = value;
            }
        }
    }
}