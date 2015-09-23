using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A Product object
/// </summary>
namespace ChanceSpace.WebShop
{
    public class Product
    {
        #region Fields
        private int _id;
        private string _name;
        private float _price;
        private int _amount;
        private float _total;
        #endregion _Fields_

        #region Constructors
        
        public Product() {
          
        }
        /// <summary>
        /// Default cart constructor
        /// </summary>
        /// <param name="id">The product's id</param>
        /// <param name="name">The product's name</param>
        /// <param name="price">The product's price</param>
        /// <param name="amount">The amount of this product</param>
        public Product(int id, string name, float price, int amount)
        {
            this._id = id;
            this._name = name;
            this.Price = price;
            this.Amount = amount;
            
            //this._total = price * amount;
        }
        #endregion _Constructors_

        #region Properties
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Name { get { return this._name; } set { this._name = value; } }
        public float Price { get { return this._price; } set { this._price = value; SetTotal(); } }
        public int Amount { get { return this._amount; } set { this._amount = value; SetTotal(); } }
        public float Total { get { return this._total; } }
        #endregion _Properties_

        #region Methods
        private void SetTotal()
        {
            _total = _price * _amount;
        }
        #endregion _Methods_
    }
}

