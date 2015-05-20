using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using System.Web.UI;

/// <summary>
/// Cart Version 1.0.2.1
/// </summary>
namespace ChanceSpace.WebShop
{
    public class Cart
    {
        #region Fields
        private List<Product> _products; // The only thing that is either set by session or by new;
        private HttpContext _context = HttpContext.Current;
        private HttpSessionState _sesh;
        private string _seshName;
        #endregion _Fields_

        #region Constructors
        public Cart() : this("cart") { }
        public Cart(string session)
	    {
            _sesh = _context.Session;
            _seshName = session;

            if (_sesh[_seshName] != null)
            {
                Debug.WriteLine("Sesh exists");
                //_context.Response.Write((string)_sesh[_seshName]);
                Cart a = (Cart)_context.Session[_seshName];
                this._products = a._products;
            }
            else
            {
                _products = new List<Product>();
            }
	    }
        #endregion _Constructors_

        #region Properties
        public Product this[int index]
        {
            get { return this._products[index]; }
            set { this._products[index] = value; }
        }
        public int Count
        {
            get 
            { 
                if(this._products.Count > 0) return this._products.Count;
                else return 0;
            }
        }
        public List<Product> Products
        {
            get { return this._products; }
        }
        #endregion _Properties_

        #region Methods
        public int IndexOf(Product item)
        {
            return this._products.IndexOf(item);
        }
        /// <summary>
        /// Adds a new Product to the cart or adds the amount to an already existing Product
        /// </summary>
        /// <param name="item">The Product added to the Cart</param>
        public void Add(Product item)
        {
            bool isNew = true;
            if (this.Count > 0)
            {
                Product p = this._products.Find(x => x.Id == item.Id);
                if(p != null)
                {
                    p.Amount += item.Amount;
                    if (p.Amount <= 0) this.Remove(p);
                    isNew = false;
                }
            }
            if (isNew) if(item.Amount > 0) this._products.Add(item);
        }
        /// <summary>
        /// Removes all of this type of Product
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Product item)
        {
            this._products.Remove(item);
        }
        public void Remove(int id)
        { 
            Product p = this._products.Find(x => x.Id == id);
            this._products.Remove(p);
        }
        //?
        public void Remove(Product item, int amount)
        {
            if (this.Count > 0)
            {
                if (this.Count == amount) this.Remove(item);
                else
                {
                    foreach (Product p in this._products)
                    {
                        if (item.Id == p.Id)
                        {
                            p.Amount -= amount;
                        }
                    }
                }
                
            }
        }
        public void CopyTo(Product[] array, int index)
        {
            this._products.CopyTo(array, index);
        }
        public void AddRange(Product[] collection)
        {
            this.AddRange(collection);
        }
        public bool Contains(Product item)
        {
            return this._products.Contains(item);
        }
        public void Insert(int index, Product item)
        {
            this._products.Insert(index, item);
        }
        /// <summary>
        /// Updates the session that Cart uses, so i can be saved across page refreshes and postbacks
        /// </summary>
        public void UpdateSession()
        {
            _sesh[_seshName] = this;
        }

        #endregion _Methods_
    }
}