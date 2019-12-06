using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();
        public decimal storeProfit = 0;

        public ConsignmentShop() // Constructor -- Has no return type
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListBox.DataSource = itemsBinding;

            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            vendorsBinding.DataSource = store.Vendors;
            vendorsListBox.DataSource = vendorsBinding;

            vendorsListBox.DisplayMember = "Display";
            vendorsListBox.ValueMember = "Display";

        }

        private void SetupData()
        {
            Vendor demoVendor = new Vendor();

            //One way of initialising an object
            demoVendor.FirstName = "Bill";
            demoVendor.LastName = "Gates";


            store.Vendors.Add(demoVendor);

            //Another way of initialising object
            store.Vendors.Add(new Vendor { FirstName = "Sue", LastName = "Evans" });
            store.Items.Add(new Item
            {
                Title = "Hard Drive",
                Description = "A Book about Microsoft",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "Shoe Dog",
                Description = "A Book about Nike",
                Price = 3.50M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Mind Maps ",
                Description = "A Book about the mind",
                Price = 2.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "Homo Deus",
                Description = "A Book about Humanity",
                Price = 6.50M,
                Owner = store.Vendors[1]
            });

            store.Name = "Amazonia";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ConsignmentShop_Load(object sender, EventArgs e)
        {

        }

        private void purchaseItemButton_Click(object sender, EventArgs e)
        {   // Figure out what is selected from the items list
            // Copy the item to the shopping cart
            // Do we remove the item from the items list? - no
            Item selectedItem = (Item)itemsListBox.SelectedItem;

            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);
        }

        private void purchaseButton_Click(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;
            }

            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            storeProfitValue.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}
