using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class SolidPrinciples
    {
        #region Single Responsibility Principle

        internal class SingleResponsibilty
        {
            internal enum OrderType
            {
                TakeAway,
                Delivery,
                Inline
            }

            internal enum QuantityType
            {
                Full,
                Half,
                NotApplicable
            }


            internal class FoodItem
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public QuantityType Type { get; set; }
                public float Price { get; set; }
                public bool Available { get; set; }
                static readonly List<FoodItem> FoodItemsList;

                /// <summary>
                /// Get the food item based on id or name using indexing
                /// </summary>
                public readonly static FoodItem Get = new FoodItem();
                static FoodItem()
                {
                    FoodItemsList = new List<FoodItem>();
                    var fields = typeof(FoodItem).GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
                    foreach (var field in fields)
                    {
                        var foodItem = field.GetValue(null) as FoodItem;
                        if (foodItem != null)
                        {
                            FoodItemsList.Add(foodItem);
                        }
                    }
                }

                public FoodItem()
                {
                }

                public FoodItem(int id,string name, float price, bool available) : this(id, name, price, available, QuantityType.NotApplicable)
                {
                }
                public FoodItem(int id,string name, float price, bool available, QuantityType type)
                {
                    Id = id;
                    Name = name;
                    Price = price;
                    Available = available;
                    Type = type;
                }

                public static readonly FoodItem Burger = new FoodItem(1, "Burger", 50, true);
                public static readonly FoodItem Pizza = new FoodItem(2, "Pizza", 150, true);
                public static readonly FoodItem Coke = new FoodItem(3, "Coke", 40, true);

                public static List<FoodItem> GetFoodItems()
                {
                    return FoodItemsList;
                }

                public static FoodItem GetFoodItemFromId(int id)
                {                  
                    return FoodItemsList.Where(m => m.Id == id).FirstOrDefault();
                }

                /// <summary>
                /// Get the food item based on the item name
                /// </summary>
                /// <param name="name"> The food item name </param>
                /// <returns> The food item </returns>
                public FoodItem this[string name]
                {
                    get
                    {
                        return FoodItemsList.Where(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    }
                }

                /// <summary>
                /// Gets the food item based on the item id
                /// </summary>
                /// <param name="id"> The food item id </param>
                /// <returns> The food item </returns>
                public FoodItem this[int id]
                {
                    get
                    {
                        return GetFoodItemFromId(id);
                    }
                }
            }

            internal class OrderItem
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public int Quantity { get; set; }
                public float Price { get; set; }
            }

            internal class OrderDetails
            {
                public int FriendlyID { get; set; }
                public Guid ID { get; set; }
                public string Address { get; set; }
                public string UserName { get; set; }
                public OrderType Type { get; set; }
                public DateTime CreatedOn { get; set; }
                public List<OrderItem> Items { get; set; }
            }

            internal class OrderFood
            {
                public string Address { get; set; }
                public string UserName { get; set; }
                public OrderType Type { get; set; }
                public List<OrderItem> Items { get; set; }

                public OrderFood() { }

                public OrderFood(OrderType type, List<OrderItem> items, string userName, string address)
                {
                    Type = type;
                    Items = items;
                    UserName = userName;
                    Address = address;
                }

                public OrderDetails CreateOrder()
                {
                    var order = new OrderDetails() { ID = Guid.NewGuid(), CreatedOn = DateTime.Now };
                    switch(Type)
                    {
                        case OrderType.Delivery:
                            order.Type = OrderType.Delivery;
                            order.Address = Address;
                            order.UserName = UserName;
                            break;
                        case OrderType.Inline:
                            break;
                        case OrderType.TakeAway:
                            break;
                        default:
                            order = null;
                            break;
                    }

                    return order;
                }
            }

            internal class Restaurant
            {
                public static string Name { get; set; }
                public static string Address { get; set; }
                public static OrderDetails OrderFood(OrderType Type, List<OrderItem> items, string UserName = null, string Address = null)
                {
                    throw new NotImplementedException("Yet to be implemented");
                }
            }
        }

        #endregion


        #region Open Closed Principle

        class OpenClosedP
        {

        }

        #endregion

        class LiksovSubstituion
        {

        }

        class InterfaceSegregation
        {

        }

        class DependencyInversion
        {

        }
    }
}
