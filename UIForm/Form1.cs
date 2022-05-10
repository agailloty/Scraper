using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Persistence;
using Robot;

namespace UIForm
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private List<JobOffer> allJobs { get; set; }
        private void btnRechercher_Click(object sender, EventArgs e)
        {
            var jobsearch = new Indeed(txtRecherche.Text, (int)numPages.Value, txtLocation.Text);
            //allJobs.Clear();
            lbTitles.Items.Clear();
            allJobs = jobsearch.GetJobs();
            foreach (var job in allJobs)
            {
                lbTitles.Items.Add(job.Title);
            }
            lblJobs.Enabled = true;
            lblJobs.Text = "Emplois trouvés : " + lbTitles.Items.Count;
        }

        private void lbTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pos = lbTitles.SelectedIndex;
            JobOffer currJob = allJobs.ElementAt(pos);
            lblTitle.Text = currJob.Title;
            lblCompany.Text = currJob.Company;
            lblLocation.Text = currJob.City;
            lblDescription.Text = currJob.Description;

        }
    }
}
