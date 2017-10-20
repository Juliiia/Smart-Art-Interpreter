using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMLHelper = SmartArtInterpreter.ShapeInterpreter.SmartArtDescription.XMLHelper;
using DescFormManager = SmartArtInterpreter.MessageBoxDesc.DescriptionFormManager;

namespace SmartArtInterpreter.MessageBoxDesc
{
    public partial class ListAllSmartArts : Form
    {
        // Singelton ////////////////////////////////////////
        private static ListAllSmartArts _inst = null;

        private ListAllSmartArts()
        {
            InitializeComponent();
        }

        public static ListAllSmartArts GetInstanz
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new ListAllSmartArts();
                }
                return _inst;
            }
        }
        ///////////////////////////////////////////////////////

        public void ReloadTreeView()
        {
            /*
             * Importent to reload changes in the xml if this Form is open
             */
            treeView1.Nodes.Clear();
            LoadTreeView();
        }
        
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            /*
             * if the user select a SmartArt
             * source: https://msdn.microsoft.com/de-de/library/system.windows.forms.treeview.nodemousedoubleclick(v=vs.110).aspx
             */
            if (e.Node.Level == 1)
            {
                DescFormManager formManager = DescFormManager.GetInstanz;
                formManager.GetDescForm(e.Node.Text);
            }
        }

        private void ListAllSmartArts_Load(object sender, EventArgs e)
        {
            LoadTreeView();
        }

        private void LoadTreeView()
        {
            /*
             * Load all SmartArts into the TreeViewSmartArt
             */
            XMLHelper xmlHelper = XMLHelper.getInstance;
            List<string> allCategories = xmlHelper.ListAllCategries();
            foreach (string category in allCategories)
            {
                List<string> subCategories = new List<string>();
                subCategories = xmlHelper.ListAllSubCategries(category);
                TreeNode[] array = new TreeNode[subCategories.Count];
                int count = 0;
                foreach (string element in subCategories)
                {
                    TreeNode node = new TreeNode(element);
                    array[count] = node;
                    count++;
                }

                TreeNode treeNode = new TreeNode(xmlHelper.TranslateCategory(category), array);
                treeView1.Nodes.Add(treeNode);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            treeView1.Nodes.Clear();
            this.Hide();
        }
    }
}
//System.Diagnostics.Debug.WriteLine(child2node.Name);