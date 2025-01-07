using System;
using System.Windows.Forms;
using Core;

namespace Client
{

    internal partial class frmEditor_Shop
    {
        public frmEditor_Shop()
        {
            InitializeComponent();
        }
        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Shop[GameState.EditorIndex].Name = txtName.Text;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Shop[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void ScrlBuy_Scroll(object sender, EventArgs e)
        {
            Core.Type.Shop[GameState.EditorIndex].BuyRate = (int)Math.Round(nudBuy.Value);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int index;

            index = lstTradeItem.SelectedIndex;

            {
                ref var withBlock = ref Core.Type.Shop[GameState.EditorIndex].TradeItem[index];
                withBlock.Item = cmbItem.SelectedIndex;
                withBlock.ItemValue = (int)Math.Round(nudItemValue.Value);
                withBlock.CostItem = cmbCostItem.SelectedIndex;
                withBlock.CostValue = (int)Math.Round(nudCostValue.Value);
            }
            Editors.UpdateShopTrade();
        }

        private void BtnDeleteTrade_Click(object sender, EventArgs e)
        {
            int index;

            index = lstTradeItem.SelectedIndex;
            {
                ref var withBlock = ref Core.Type.Shop[GameState.EditorIndex].TradeItem[index];
                withBlock.Item = 0;
                withBlock.ItemValue = 0;
                withBlock.CostItem = 0;
                withBlock.CostValue = 0;
            }
            Editors.UpdateShopTrade();
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.ShopEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.ShopEditorOK();
            Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.ShopEditorCancel();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Shop.ClearShop(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Shop[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.ShopEditorInit();
        }

        private void frmEditor_Shop_Load(object sender, EventArgs e)
        {
            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_SHOPS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Type.Shop[i].Name);

            cmbItem.Items.Clear();
            cmbCostItem.Items.Clear();
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
            {
                cmbItem.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);
                cmbCostItem.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);
            }

        }

        private void frmEditor_Shop_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.ShopEditorCancel();
        }
    }
}