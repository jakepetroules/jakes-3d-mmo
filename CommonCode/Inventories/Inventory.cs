namespace MMO3D.CommonCode
{
    using System;

    /// <summary>
    /// Class for managing inventories.
    /// </summary>
    [Serializable]
    public abstract class Inventory
    {
        /// <summary>
        /// An array of the items contained in the inventory.
        /// </summary>
        private Item[] items;

        /// <summary>
        /// Initializes a new instance of the Inventory class.
        /// </summary>
        /// <param name="inventorySize">The size of the inventory.</param>
        protected Inventory(int inventorySize)
        {
            this.items = new Item[inventorySize];
        }

        /// <summary>
        /// Raised when items are added to the inventory.
        /// </summary>
        public event EventHandler ItemsAdded = delegate { };

        /// <summary>
        /// Raised when items are removed from the inventory.
        /// </summary>
        public event EventHandler ItemsRemoved = delegate { };

        /// <summary>
        /// Raised when the location of an item in the inventory changes.
        /// </summary>
        public event EventHandler InventoryOrderChanged = delegate { };

        /// <summary>
        /// Gets the number of items currently contained in the inventory.
        /// </summary>
        /// <value>See summary.</value>
        public int Count
        {
            get
            {
                int count = 0;
                for (int i = 0; i < this.items.Length; i++)
                {
                    if (this.items[i] != null)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        /// <summary>
        /// Gets the number of slots in the inventory that are currently empty.
        /// </summary>
        /// <value>See summary.</value>
        public int CountEmptySlots
        {
            get { return this.items.Length - this.Count; }
        }

        /// <summary>
        /// Gets the total number of slots in the inventory.
        /// </summary>
        /// <value>See summary.</value>
        public int CountTotalSlots
        {
            get { return this.items.Length; }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">The index in the inventory.</param>
        /// <returns>The item at the specified index.</returns>
        public Item this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        /// <summary>
        /// Gets an array of the items contained in the inventory.
        /// </summary>
        /// <returns>See summary.</returns>
        public Item[] GetItems()
        {
            return this.items;
        }

        /// <summary>
        /// Adds an item to the end of the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Whether the item was added.</returns>
        public bool Add(Item item)
        {
            if (item == null)
            {
                return false;
            }

            for (int i = 0; i < this.items.Length; i++)
            {
                if (this.items[i] == null)
                {
                    this.items[i] = item;
                    this.ItemsAdded(this, EventArgs.Empty);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the item at the specified index, from the inventory.
        /// </summary>
        /// <param name="index">The index, in the inventory, of the item to remove.</param>
        public void Remove(int index)
        {
            this.items[index] = null;
            this.ItemsRemoved(this, EventArgs.Empty);
        }

        /// <summary>
        /// Removes all items from the inventory.
        /// </summary>
        public void RemoveAll()
        {
            for (int i = 0; i < this.items.Length; i++)
            {
                this.items[i] = null;
            }

            this.ItemsRemoved(this, EventArgs.Empty);
        }

        /// <summary>
        /// Removes all items with the specified ID from the inventory.
        /// </summary>
        /// <param name="id">The ID of the items to remove.</param>
        public void RemoveAllOf(int id)
        {
            for (int i = 0; i < this.items.Length; i++)
            {
                if (this.items[i].ItemTypeId == id)
                {
                    this.items[i] = null;
                }
            }

            this.ItemsRemoved(this, EventArgs.Empty);
        }

        /// <summary>
        /// Swaps the location of two items in the inventory.
        /// </summary>
        /// <param name="indexFirst">The index, in the inventory, of the first item to swap locations.</param>
        /// <param name="indexSecond">The index, in the inventory, of the second item to swap locations.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Either argument is less than zero or greater than or equal to the inventory's size.</exception>
        public void SwapSlots(int indexFirst, int indexSecond)
        {
            try
            {
                Item intermediate = this.items[indexFirst];
                this.items[indexFirst] = this.items[indexSecond];
                this.items[indexSecond] = intermediate;

                this.InventoryOrderChanged(this, EventArgs.Empty);
            }
            catch (IndexOutOfRangeException e)
            {
                throw new ArgumentOutOfRangeException("Arguments must be greater than or equal to zero, and less than the size of the inventory.", e);
            }
        }
    }
}
